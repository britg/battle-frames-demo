using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;
using SimpleJSON;

public class MapManager : MonoBehaviour {
    
    public string BattleSceneName = "MultiplayerBattle";
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
    
    public void OnBackButtonPressed () {
        SceneManager.LoadScene("SuperMassive");
    }
    
    public void AddTiles (JSONNode tilesJSON) {
        var tilesArr = tilesJSON["tiles"].AsArray;
        foreach (JSONNode node in tilesArr) {
            var tileObj = node.AsObject;
            var id = tileObj["id"].Value;
            var x = tileObj["x"].AsFloat;
            var y = tileObj["y"].AsFloat;
            var z = tileObj["z"].AsFloat;
            var pos = new Vector3(x, y, z);
            PlaceTile(id, pos);
        }
    }
    
    void PlaceTile (string id, Vector3 coords) {
        var tile = (GameObject)Instantiate(mapTilePrefab, coords, Quaternion.identity);
        var mapTileController = tile.GetComponent<MapTileController>(); 
        mapTileController.coords = coords;
        mapTileController.tileId = id;
    }
     
    public void MapTileSelected (MapTileController mapTileController) {
        Debug.Log("Map manager getting map piece selected");
        mapInfoPanelController.SetMapPiece(mapTileController);
    }
    
    public void Unselect () {
        mapInfoPanelController.Hide();
    }
}
