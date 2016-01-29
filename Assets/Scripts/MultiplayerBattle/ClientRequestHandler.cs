using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class ClientRequestHandler : MonoBehaviour {
    
    public delegate void Callback();
    public BattleApiController battleApiController;
    
    Queue<ClientRequest> clientRequestQueue = new Queue<ClientRequest>();
    ClientRequest currentWorkingRequest;
    
    bool clientRequestsEnabled = false;

	// Use this for initialization
	void Start () {
	
	}
    
    public void EnableClientRequests (ServerCommandHandler.Callback callback) {
        EnableClientRequests();
        callback.Invoke();
    }
    
    public void EnableClientRequests () {
        clientRequestsEnabled = true;        
    }
    
    public void DisableClientRequests () {
        clientRequestsEnabled = false;
    }
    
    public void RequestEndTurn () {
        if (!clientRequestsEnabled) {
            Debug.Log("End turn requested but client actions disabled");
            return;
        }

        var request = EndTurnRequestGenerator.Generate();
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
    
    public void FinishRequest (ServerCommand serverCommand, ServerCommandHandler.Callback callback) {
        var clientRequestId = ""; //TODO: extract the client request id from the server command
        if (clientRequestId != currentWorkingRequest.id) {
            Debug.Log("Error: the server told us to finish a request the wasn't the current one!");
            throw new System.ApplicationException();
        }
        
        Debug.Log("Finishing client request " + currentWorkingRequest);
        currentWorkingRequest = null;
        
        new tpd.Wait(this, 3, () => {
            callback.Invoke();    
        });
    }
}
