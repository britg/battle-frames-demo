using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class LogInController : MonoBehaviour {
    
    public InputField emailField;
    public InputField passField;
    
    public MainApiController mainApiController;

	public void AttemptLogIn () {
        mainApiController.LogIn(
            emailField.text,
            passField.text,
            HandleLogIn    
        );
    }
    
    void HandleLogIn (WWW response) {
        var json = JSON.Parse(response.text);
        if (json["errors"] != null) {
            Debug.Log("Errors! " + response.text);
            // TODO: Handle errors
            return;
        }
        
        var accessToken = response.responseHeaders["ACCESS-TOKEN"];
        var client = response.responseHeaders["CLIENT"];
        var uid = response.responseHeaders["UID"];
        
        mainApiController.SetAccessToken(accessToken, client, uid);
        
        var handle = json["data"].AsObject["nickname"].Value;
        PlayerPrefs.SetString("handle", handle);
        
        SceneManager.LoadScene(0);
    }
}
