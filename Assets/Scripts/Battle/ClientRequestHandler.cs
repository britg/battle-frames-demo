using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class ClientRequestHandler : MonoBehaviour {
    
    public delegate void Callback();
    public BattleApiController battleApiController;
    
    Queue<ClientRequest> clientRequestQueue = new Queue<ClientRequest>();
    ClientRequest currentWorkingRequest;
    
    bool clientRequestsEnabled = true;

	// Use this for initialization
	void Start () {
	
	}
    
    public void EnableClientRequests (ServerCommand command) {
        EnableClientRequests();
        command.onProcessedCallback.Invoke();
    }
    
    public void EnableClientRequests () {
        Debug.Log("Enabling client requests");
        clientRequestsEnabled = true;        
    }
    
    public void DisableClientRequests (ServerCommand command) {
        DisableClientRequests();
        command.onProcessedCallback.Invoke();
    }
    
    public void DisableClientRequests () {
        Debug.Log("Disabling client requests and clearing the queue");
        clientRequestsEnabled = false;
        clientRequestQueue.Clear();
    }
    
    public void RequestEndTurn () {
        if (!clientRequestsEnabled) {
            Debug.Log("End turn requested but client actions disabled");
            return;
        }

        var request = EndTurnRequestGenerator.Generate();
        QueueRequest(request);
    }
    
    public void QueueRequest (ClientRequest request) {
        Debug.Log("Queuing request "+ request);
        clientRequestQueue.Enqueue(request);
    }
	
	// Update is called once per frame
	void Update () {
	   PopAndPerformRequest();
	}
    
    void PopAndPerformRequest () {
        
        if (!clientRequestsEnabled) {
            return;
        }
        
        if (currentWorkingRequest != null) {
            return;
        }
        
        if (clientRequestQueue.Count < 1) {
            return;
        }
        
        currentWorkingRequest = clientRequestQueue.Dequeue();
        battleApiController.SendClientRequest(currentWorkingRequest);
    }
    
    public void FinishRequest (ServerCommand serverCommand) {
        var clientRequestId = serverCommand.clientRequestId;
        if (clientRequestId != currentWorkingRequest.id) {
            Debug.Log(string.Format("Error: the server told us to finish a request the wasn't the current one! {0}, {1}", clientRequestId, currentWorkingRequest));
            throw new System.ApplicationException();
        }
        
        // TODO: Validate that the command was successful
        // then display some sort of error and play an
        // error sound
        if (!serverCommand.requestSuccessful) {
            Debug.Log("Warning: Request was unsuccessful " + currentWorkingRequest);
        }
        
        Debug.Log("Finishing client request " + currentWorkingRequest);
        currentWorkingRequest = null;
        
        serverCommand.onProcessedCallback.Invoke();
    }
}
