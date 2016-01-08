using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MapManager : MonoBehaviour {
    
    public string BattleSceneName = "Battle";
    public GameObject mapTilePrefab;
    
    public MapInfoPanelController mapInfoPanelController;

	// Use this for initialization
	void Start () {
       // This is a proxy for getting the map JSON from somewhere.
	   Invoke("RenderMap", 1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void OnDemoButtonPressed (string scenarioKey) {
        Simulation.scenarioKey = scenarioKey;
        SceneManager.LoadScene(BattleSceneName);
    }
    
    void RenderMap () {
        PlacePiece(new Vector3(1, 0, 1));
        PlacePiece(new Vector3(1, 0, 2));
        PlacePiece(new Vector3(2, 0, 2));
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
