using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
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
            SceneManager.LoadScene(0); 
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
    
    public void RequestTile (string id, APIResponseHandler handler) {
        var path = string.Format("/api/tiles/{0}", id);
        Get(path, handler);
        
    }
    
    public void RequestBattle (string tileId, APIResponseHandler handler) {
        var path = string.Format("/api/battles");
        Post(path, new Dictionary<string, string>(){ {"tile_id", tileId }}, handler);
    }
}
