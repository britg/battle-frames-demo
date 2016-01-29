using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClientRequestHandler : MonoBehaviour {
    
    public delegate void Callback();
    public BattleApiController battleApiController;
    
    Queue<ClientRequest> clientRequestQueue = new Queue<ClientRequest>();
    ClientRequest currentWorkingRequest;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	   PopAndPerformRequest();
	}
    
    void PopAndPerformRequest () {
        if (currentWorkingRequest != null) {
            return;
        }
        
        if (clientRequestQueue.Count < 1) {
            return;
        }
        
        currentWorkingRequest = clientRequestQueue.Dequeue();
        
        
    }
}
