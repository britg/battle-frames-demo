using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class ServerCommandHandler : MonoBehaviour {
    
    public delegate void Callback();
    public SpawnCharacterProcessor spawnCharacterProcessor;
    public ClientRequestHandler clientRequestHandler;
    public BattleStateSyncer battleStateSyncer;
    
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
        
        switch (currentWorkingCommand.commandName) {
            case ServerCommand.SpawnCharacter:
                spawnCharacterProcessor.Process(currentWorkingCommand, OnCommandDone);
            break;
            case ServerCommand.EnableClientActions:
                clientRequestHandler.EnableClientRequests(OnCommandDone);
            break;
            case ServerCommand.ClientRequestOutcome:
                clientRequestHandler.FinishRequest(currentWorkingCommand, OnCommandDone);                
            break;
            case ServerCommand.SyncBattleState:
                battleStateSyncer.Sync(currentWorkingCommand, OnCommandDone);    
            break;
        }
        
    }
    
    void OnCommandDone () {
        Debug.Log("Done processing command " + currentWorkingCommand);
        currentWorkingCommand = null;
    }
}
