using UnityEngine;
using System.Collections;

public class SpawnShipProcessor : MonoBehaviour, IServerCommandProcessor {
    
    public GameObject shipPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void Process (ServerCommand command) {
        var characterObj = Instantiate(shipPrefab) as GameObject;
        var character = characterObj.GetComponent<Ship>();
        
        
        
        command.onProcessedCallback.Invoke();
    }
}
