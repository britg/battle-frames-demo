using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WebSocketSharp;
using WebSocketSharp.Net;
using SimpleJSON;

public class BattleApiController : APIBehaviour {
    
    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

    public ServerCommandHandler commandHandler;    
    public APIStateMachine stateMachine;
    public string websocketURL = "ws://127.0.0.1:28080";
    public float pingInterval = 10f;
    
    WebSocket ws;
    
    Queue<string> receivedMessages = new Queue<string>();

	// Use this for initialization
	void Start() {
        Connect();
    }
    
    void Update () {
        ProcessResponseQueue();
    }
    
    void Connect () {
        ws = new WebSocket(websocketURL);
        var cookie = new Cookie();
        cookie.Name = "access";
        cookie.Value = PlayerPrefs.GetString("uid");
        ws.SetCookie(cookie);

        ws.OnOpen += OnOpenHandler;
        ws.OnMessage += OnMessageHandler;
        ws.OnClose += OnCloseHandler;

        stateMachine.AddHandler(APIState.Running, () => {
            new tpd.Wait(this, 3, () => {
                Debug.Log("Connecting to websocket " + ws.Url);
                ws.ConnectAsync();
            });
        });

        stateMachine.AddHandler(APIState.Connected, () => {
            stateMachine.Transition(APIState.Subscribing);
        });

        stateMachine.AddHandler(APIState.Subscribing, () => {
            Subscribe();
        });
        
        stateMachine.AddHandler(APIState.Subscribed, () => {
            new tpd.Wait(this, 1, () => {
                StartBattle();
            });
        });
        
        stateMachine.Run();
    }
    
    void StartPing () {
        InvokeRepeating("Ping", 0f, pingInterval);
    }    
        
    void StartBattle () {
        RequestReady();
        // StartPing();    
    }
    
    public static JSONNode requestNode () {
        var node = JSON.Parse("{}");
        node.Add("command", "message");
        var channel = JSON.Parse("{}");
        channel.Add("channel", "BattleChannel");
        channel.Add("battle_id", PlayerPrefs.GetString("battleId"));
        node.Add("identifier", channel.AsObject.ToString());
        return node;
    }
    
    void Subscribe () {
        var node = JSON.Parse("{}");
        node.Add("command", "subscribe");
        var channel = JSON.Parse("{}");
        channel.Add("channel", "BattleChannel");
        channel.Add("battle_id", PlayerPrefs.GetString("battleId"));
        node.Add("identifier", channel.AsObject.ToString());
        var msg = node.AsObject.ToString();
        
        APIMessage(msg);
    }
    
    void Ping () {
        var node = requestNode();
        var data = JSON.Parse("{}");
        data.Add("request", "ping");
        node.Add("data", data.AsObject.ToString());
        var msg = node.ToString();
        
        APIMessage(msg);
    }
    
    void RequestReady () {
        var node = requestNode();
        
        var data = JSON.Parse("{}");
        data.Add("request", ClientRequest.ClientReady);
        node.Add("data", data.AsObject.ToString());
        var msg = node.ToString();
        APIMessage(msg); 
    }
    
    public void SendClientRequest (ClientRequest clientRequest) {
        APIMessage(clientRequest.ForApi());
    }
    
    void APIMessage (string msg) {
        sw.Start();
        Debug.Log("[API Message] " + msg);
        ws.SendAsync(msg, OnSendComplete);
    }

    private void OnOpenHandler(object sender, System.EventArgs e) {
        Debug.Log("WebSocket connected!");
        stateMachine.Transition(APIState.Connected);
    }

    private void OnMessageHandler(object sender, MessageEventArgs e) {
        sw.Stop();
        
        // Debug.Log("WebSocket server said: " + e.Data + " (" + sw.ElapsedMilliseconds + ")");
        sw.Reset();
        receivedMessages.Enqueue(e.Data);
    }

    private void OnCloseHandler(object sender, CloseEventArgs e) {
        Debug.Log("WebSocket closed with reason: " + e.Reason);
        stateMachine.Transition(APIState.Done);
    }

    private void OnSendComplete(bool success) {
        // Debug.Log("Message sent successfully? " + success);
    }
    
    void ProcessResponseQueue () {
        if (receivedMessages.Count < 1) {
            return;
        }
        
        var msg = receivedMessages.Dequeue();
        var msgObj = JSON.Parse(msg);
        
        if (msgObj["identifier"].Value == "_ping") {
            return;
        }
        
        if (msgObj["type"].Value == "confirm_subscription") {
            stateMachine.Transition(APIState.Subscribed);
        }
        
        var message = msgObj["message"].AsObject;
        if (message == null || message["commands"] == null) {
            Debug.Log("Did not know how to handle message " + message);
            return;
        }
        
        var serverCommands = message["commands"].AsArray;
        commandHandler.ParseAndQueueServerCommands(serverCommands);
        
        // Debug.Log("Message is " + msgObj["message"].Value + " " + (msgObj["message"].AsObject == null));
        // if () {
        //     var battleNode = msgObj["message"].AsObject["battle"];
        //     if (battleNode != null) {
        //         UpdateBattleState(battleNode);
        //     }    
        // }
    }
    
    void OnDestroy () {
        Debug.Log("closing ws");
        ws.CloseAsync();
    }
    
}
