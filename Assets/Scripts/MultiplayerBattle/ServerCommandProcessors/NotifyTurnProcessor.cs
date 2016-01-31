using UnityEngine;
using System.Collections;

public class NotifyTurnProcessor : MonoBehaviour, IServerCommandProcessor {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void Process (ServerCommand command) {
        
        command.onProcessedCallback.Invoke();
    }
}
