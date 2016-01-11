using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainApiController : APIBehaviour {
    
    public GameObject gameCanvas;
    public GameObject loginCanvas;

	// Use this for initialization
	void Start () {
        TestAuth(HandleTestAuth);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void HandleTestAuth (WWW response) {
        var status = response.responseHeaders["STATUS"];
        
        if (!status.Contains("200")) {
            gameCanvas.SetActive(false);
            loginCanvas.SetActive(true);                   
        }
    }
}
