using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class MainApiController : APIBehaviour {
    
    public GameObject gameCanvas;
    public GameObject loginCanvas;
    
    public Text loggedInAsText;

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
        } else {
            gameCanvas.SetActive(true);
            loginCanvas.SetActive(false);
            
            var json = JSON.Parse(response.text);
            SetIdentity(json);
        }
    }
    
    void SetIdentity (JSONNode json) {
        var handle = json["data"].AsObject["nickname"].Value;
        PlayerPrefs.SetString("handle", handle);
        loggedInAsText.text = "Logged in as: " + PlayerPrefs.GetString("handle");
    }
    
    public void LogIn (string email, string password, APIResponseHandler handler) {
        var path = "/auth/sign_in";
        var body = new Dictionary<string, string>() {
            { "email", email },
            { "password", password }
        };
        Debug.Log("Posting email: " + email + " and password: " + password);
        Post(path, body, handler);   
    }
}
