using UnityEngine;
using System.Collections;

public class MapTileController : MapBehaviour {
    
    public Vector3 coords;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void Select () {
        Debug.Log("Calling select on " + coords);
        mapManager.MapTileSelected(this);
        
    }
}
