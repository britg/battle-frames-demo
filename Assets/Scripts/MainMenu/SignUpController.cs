using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using SimpleJSON;

public class SignUpController : MonoBehaviour {
    
    public GameObject logInPanel;

    public InputField nameField;
	public InputField emailField;
    public InputField passField;
    
    public MainApiController mainApiController;
    

	public void AttemptSignUp () {
        mainApiController.SignUp(
            emailField.text,
            nameField.text,
            passField.text,
            HandleSignUp    
        );
    }
    
    void HandleSignUp (WWW response) {
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
    
    public void ShowLogIn () {
        logInPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
