using UnityEngine;
using System.Collections;

public class SpawnCharacterProcessor : MonoBehaviour, IServerCommandProcessor {
    
    public GameObject character;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void Process (ServerCommand command) {
        Debug.Log("Spawn character processor is processing command " + command);
        Instantiate(character);
        command.onProcessedCallback.Invoke();
    }
}
