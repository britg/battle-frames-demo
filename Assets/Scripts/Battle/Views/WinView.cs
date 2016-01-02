using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinView : MonoBehaviour {
    
    public GameObject winTextObj;

	// Use this for initialization
	void Start () {
	   NotificationCenter.AddObserver(this, Notifications.OnPlayerWin);
       NotificationCenter.AddObserver(this, Notifications.OnPlayerLost);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnPlayerWin () {
        winTextObj.SetActive(true);        
    }
    
    void OnPlayerLost () {
        var text = winTextObj.GetComponent<Text>();
        text.text = "Lost :(";
        winTextObj.SetActive(true);
    }
}
