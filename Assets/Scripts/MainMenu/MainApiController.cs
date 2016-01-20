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
        //TestAuth(HandleTestAuth);
        TestUser((successResponse) => {    
            gameCanvas.SetActive(true);
            loginCanvas.SetActive(false);
            loggedInAsText.text = "Logged in as: " + PlayerPrefs.GetString("handle");
        }, (errorResponse) => {
            gameCanvas.SetActive(false);
            loginCanvas.SetActive(true);
        });
	}
	
	// Update is called once per frame
	void Update () {
	
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
    
    public void SignUp (string email, string handle, string password, APIResponseHandler handler) {
        var path = "/auth.json";
        var body = new Dictionary<string, string>() {
            { "nickname", handle },
            { "email", email },
            { "password", password },
            { "password_confirmation", password }
        };
        Debug.Log("Posting email: " + email + " and password: " + password);
        Post(path, body, handler);   
    }
}
