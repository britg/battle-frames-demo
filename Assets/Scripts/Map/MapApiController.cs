using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MapApiController : APIBehaviour {
    
    public MapManager mapManager;

	// Use this for initialization
	void Start () {
        LoadMap();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void LoadMap () {
        TestUser((successResponse) => {
            Debug.Log("Player centerpoint is " + PlayerPrefs.GetString("currentTileJSON"));
            mapManager.RenderMap(successResponse);            
        }, (errorResponse) => {
            Debug.Log("Error loading map " + errorResponse.error);
        });
        
    }
}
