using UnityEngine;
using System.Collections;

public class MapInfoPanelController : MonoBehaviour {
    
    public GameObject infoPanel;

	// Use this for initialization
	void Start () {
	   Hide();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void SetMapPiece (MapPieceController mapPieceController) {
        Show();
    }
    
    public void Hide () {
       infoPanel.SetActive(false); 
    }
    
    public void Show () {
        infoPanel.SetActive(true);
    }
}
