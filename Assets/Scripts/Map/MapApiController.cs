using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using SimpleJSON;

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
            RequestMap();            
        }, (errorResponse) => {
            Debug.Log("Error loading map " + errorResponse.error);
        });
    }
    
    public void RequestMap () {
        var currentTileJSON = JSON.Parse(PlayerPrefs.GetString("currentTileJSON"));
        var x = currentTileJSON["x"].AsFloat;
        var y = currentTileJSON["y"].AsFloat;
        var z = currentTileJSON["z"].AsFloat;
        var path = string.Format("/api/tiles?x={0}&y={1}&z={2}", x, y, z);
        
        Get(path, (tilesResponse) => {
            Debug.Log("Tiles response is " + tilesResponse.text);
            var tilesJSON = JSON.Parse(tilesResponse.text);
            mapManager.AddTiles(tilesJSON);
        });
    }
}
