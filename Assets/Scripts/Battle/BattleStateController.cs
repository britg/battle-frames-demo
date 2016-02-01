using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SimpleJSON;

public class BattleStateController : MonoBehaviour {
    
    public BattleApiController apiController;
    public ClientRequestHandler clientRequestHandler;
    
    public Text turnText; 
    
    BattleState battleState;

	// Use this for initialization
	void Start () {
	   apiController.Connect(OnConnect);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnConnect () {
        SendClientReadyRequest();
    }
    
    void SendClientReadyRequest () {
        var request = StartBattleRequestGenerator.Generate();
        clientRequestHandler.QueueRequest(request);
    }
    
    public void Sync (ServerCommand serverCommand) {
        var battleStateNode = serverCommand.data;
        battleState = new BattleState(battleStateNode.AsObject);
        
        ApplyTurn();
        
        serverCommand.onProcessedCallback.Invoke();
    }
    
    void ApplyTurn () {
        if (battleState.isClientTurn) {
            turnText.text = "My Turn";
        } else {
            turnText.text = "Enemy Turn";
        }
    }
}
