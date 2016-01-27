using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using SimpleJSON;

public class MapInfoPanelController : MonoBehaviour {
    
    public GameObject infoPanel;
    public MapApiController mapApiController;
    
    MapTileController currentTileController;

	// Use this for initialization
	void Start () {
	   Hide();
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    
    public void SetMapPiece (MapTileController mapTileController) {
        currentTileController = mapTileController;
        mapApiController.RequestTile(currentTileController.tileId, (response) => {
            Show();
        });
    }
    
    public void RequestBattle () {
        mapApiController.RequestBattle(currentTileController.tileId, (response) => {
           Debug.Log("Response is " + response.text);
           var parsed = JSON.Parse(response.text);
           var battleId = parsed["battle"].AsObject["id"].Value;
           PlayerPrefs.SetString("battleId", battleId);
           Debug.Log("Battle id is " + battleId);
           SceneManager.LoadScene("MultiplayerBattle");
        });
    }
    
    public void Hide () {
       infoPanel.SetActive(false); 
    }
    
    public void Show () {
        infoPanel.SetActive(true);
    }
}
