using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;
using SimpleJSON;

public class MapManager : MonoBehaviour {
    
    public string BattleSceneName = "Battle";
    public GameObject mapTilePrefab;
    
    public MapInfoPanelController mapInfoPanelController;

	// Use this for initialization
	void Start () {
       // This is a proxy for getting the map JSON from somewhere.
	//    Invoke("RenderMap", 1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void OnDemoButtonPressed (string scenarioKey) {
        Simulation.scenarioKey = scenarioKey;
        SceneManager.LoadScene(BattleSceneName);
    }
    
    public void AddTiles (JSONNode tilesJSON) {
        var tilesArr = tilesJSON.AsArray;
        foreach (JSONNode node in tilesArr) {
            var tileObj = node.AsObject;
            var x = tileObj["x"].AsFloat;
            var y = tileObj["y"].AsFloat;
            var z = tileObj["z"].AsFloat;
            var pos = new Vector3(x, y, z);
            PlacePiece(pos);
        }
    }
    
    void PlacePiece (Vector3 coords) {
        var piece = (GameObject)Instantiate(mapTilePrefab, coords, Quaternion.identity);
        piece.GetComponent<MapTileController>().coords = coords;
    }
     
    public void MapTileSelected (MapTileController mapTileController) {
        Debug.Log("Map manager getting map piece selected");
        mapInfoPanelController.SetMapPiece(mapTileController);
    }
    
    public void Unselect () {
        mapInfoPanelController.Hide();
    }
}
