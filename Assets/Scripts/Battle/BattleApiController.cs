using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WebSocketSharp;
using WebSocketSharp.Net;
using SimpleJSON;

public class BattleApiController : APIBehaviour {
    
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
        ProcessQueue();
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
            RequestBattleState();
            StartPing();
        });
        
        stateMachine.Run();
    }
    
    void StartPing () {
        InvokeRepeating("Ping", 0f, pingInterval);
    }
    
    void Ping () {
        
        var node = JSON.Parse("{}");
        node.Add("command", "message");
        var channel = JSON.Parse("{}");
        channel.Add("channel", "BattleChannel");
        channel.Add("battle_id", PlayerPrefs.GetString("battleId"));
        node.Add("identifier", channel.AsObject.ToString());
        var data = JSON.Parse("{}");
        data.Add("command", "ping");
        node.Add("data", data.AsObject.ToString());
        var msg = node.ToString();
        
        APIMessage(msg);
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
    
    void RequestBattleState () {
        var node = JSON.Parse("{}");
        node.Add("command", "message");
    }
    
    void APIMessage (string msg) {
        Debug.Log("[API Message] " + msg);
        ws.SendAsync(msg, OnSendComplete);
    }

    private void OnOpenHandler(object sender, System.EventArgs e) {
        Debug.Log("WebSocket connected!");
        stateMachine.Transition(APIState.Connected);
    }

    private void OnMessageHandler(object sender, MessageEventArgs e) {
        Debug.Log("WebSocket server said: " + e.Data);
        receivedMessages.Enqueue(e.Data);
    }

    private void OnCloseHandler(object sender, CloseEventArgs e) {
        Debug.Log("WebSocket closed with reason: " + e.Reason);
        stateMachine.Transition(APIState.Done);
    }

    private void OnSendComplete(bool success) {
        // Debug.Log("Message sent successfully? " + success);
    }
    
    void ProcessQueue () {
        if (receivedMessages.Count < 1) {
            return;
        }
        
        var msg = receivedMessages.Dequeue();
        var msgObj = JSON.Parse(msg);
        if (msgObj["type"].Value == "confirm_subscription") {
            stateMachine.Transition(APIState.Subscribed);
        } else {
            iTween.MoveBy(Camera.main.gameObject, Vector3.left, 1f);
        }
    }
    
    void OnDestroy () {
        Debug.Log("closing ws");
        ws.CloseAsync();
    }
}
