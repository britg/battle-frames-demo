using UnityEngine;
using System.Collections;
using WebSocketSharp;
using WebSocketSharp.Net;

public class BattleApiController : APIBehaviour {
    
    public WebsocketsStateMachine stateMachine;
    public string websocketURL = "ws://127.0.0.1:28080";
    
    WebSocket ws;

	// Use this for initialization
	void Start() {
        ws = new WebSocket(websocketURL);
        var cookie = new Cookie();
        cookie.Name = "access";
        cookie.Value = PlayerPrefs.GetString("uid");
        ws.SetCookie(cookie);

        ws.OnOpen += OnOpenHandler;
        ws.OnMessage += OnMessageHandler;
        ws.OnClose += OnCloseHandler;

        stateMachine.AddHandler(State.Running, () => {
            new tpd.Wait(this, 3, () => {
                Debug.Log("Connecting to websocket " + ws.Url);
                ws.ConnectAsync();
            });
        });

        stateMachine.AddHandler(State.Connected, () => {
            stateMachine.Transition(State.Subscribe);
        });

        stateMachine.AddHandler(State.Subscribe, () => {
            // ws.SendAsync("{\"command\": \"subscribe\", \"identifier\": {\"channel\": \"Battle\"}}", OnSendComplete);
            ws.SendAsync("{\"command\": \"subscribe\", \"identifier\": \"{\\\"channel\\\": \\\"BattleChannel\\\"}\"}", OnSendComplete);
        });

        // stateMachine.AddHandler(State.Pong, () => {
        //     new tpd.Wait(this, 3, () => {
        //         ws.CloseAsync();
        //     });
        // });

        stateMachine.Run();
    }

    private void OnOpenHandler(object sender, System.EventArgs e) {
        Debug.Log("WebSocket connected!");
        stateMachine.Transition(State.Connected);
    }

    private void OnMessageHandler(object sender, MessageEventArgs e) {
        Debug.Log("WebSocket server said: " + e.Data);
    }

    private void OnCloseHandler(object sender, CloseEventArgs e) {
        Debug.Log("WebSocket closed with reason: " + e.Reason);
        stateMachine.Transition(State.Done);
    }

    private void OnSendComplete(bool success) {
        Debug.Log("Message sent successfully? " + success);
    }
    
    void OnDestroy () {
        Debug.Log("closing ws");
        ws.CloseAsync();
    }
}
