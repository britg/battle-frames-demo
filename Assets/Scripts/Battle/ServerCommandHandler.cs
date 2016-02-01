using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class ServerCommandHandler : MonoBehaviour {
    
    public delegate void Callback();
    public SpawnShipProcessor spawnShipProcessor;
    public ClientRequestHandler clientRequestHandler;
    public BattleStateController battleStateController;
    public NotifyTurnProcessor notifyTurnProcessor;
    
    Queue<ServerCommand> serverCommandQueue = new Queue<ServerCommand>();
    ServerCommand currentWorkingCommand;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	   PopAndPerformCommand();
	}
    
    public void ParseAndQueueServerCommands (JSONArray commands) {
        foreach (JSONNode commandNode in commands) {
            var sc = new ServerCommand(commandNode);
            serverCommandQueue.Enqueue(sc);
        }
    }
    
    public void PopAndPerformCommand () {
        if (currentWorkingCommand != null) {
            return;
        }        
        
        if (serverCommandQueue.Count < 1) {
            return;
        }
        
        currentWorkingCommand = serverCommandQueue.Dequeue();
        Debug.Log("Popping and performing command " + currentWorkingCommand);
        currentWorkingCommand.onProcessedCallback = OnCommandDone;
        
        switch (currentWorkingCommand.commandName) {
            
            case ServerCommand.SpawnShip:
                spawnShipProcessor.Process(currentWorkingCommand);
            break;
            
            case ServerCommand.EnableClientActions:
                clientRequestHandler.EnableClientRequests(currentWorkingCommand);
            break;
            
            case ServerCommand.DisableClientActions:
                clientRequestHandler.DisableClientRequests(currentWorkingCommand);
            break;
            
            case ServerCommand.ClientRequestOutcome:
                clientRequestHandler.FinishRequest(currentWorkingCommand);                
            break;
            
            case ServerCommand.SyncBattleState:
                battleStateController.Sync(currentWorkingCommand);    
            break;
            
            case ServerCommand.NotfyTurn:
                notifyTurnProcessor.Process(currentWorkingCommand);
            break;
        }
        
    }
    
    void OnCommandDone () {
        Debug.Log("Done processing command " + currentWorkingCommand);
        currentWorkingCommand = null;
    }
}
