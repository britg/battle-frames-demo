using UnityEngine;
using System.Collections;

public class WinView : MonoBehaviour {
    
    public GameObject winTextObj;

	// Use this for initialization
	void Start () {
	   NotificationCenter.AddObserver(this, Notifications.OnPlayerWin);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnPlayerWin () {
        winTextObj.SetActive(true);        
    }
}
