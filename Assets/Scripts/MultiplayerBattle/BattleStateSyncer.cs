using UnityEngine;
using System.Collections;

public class BattleStateSyncer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void Sync (ServerCommand serverCommand, ServerCommandHandler.Callback callback) {
        Debug.Log("Battle state syncer is syncing battle state");
        new tpd.Wait(this, 3, () => {
            callback.Invoke();
        });
    }
}
