using UnityEngine;
using System.Collections;

public class SpawnCharacterProcessor : MonoBehaviour, IServerCommandProcessor {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void Process (ServerCommand command, ServerCommandHandler.Callback callback) {
        Debug.Log("Spawn character processor is processing command " + command);
        new tpd.Wait(this, 3, () => {
            callback.Invoke();    
        });
    }
}
