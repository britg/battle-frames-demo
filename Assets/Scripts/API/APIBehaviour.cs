using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public abstract class APIBehaviour : MonoBehaviour {
    
    public delegate void AuthTestHandler (bool valid);
    public delegate void APIResponseHandler (WWW response); 

    public string RootUrl = "http://tavernlight.dev";
    
    string accessToken;
    string client;
    string uid;
    Dictionary<string, string> authHeaders;
    
    void Awake () {
        SetAuthHeaders();
    }
    
    void SetAuthHeaders () {
        accessToken = PlayerPrefs.GetString("access-token");
        client = PlayerPrefs.GetString("client");
        uid = PlayerPrefs.GetString("uid");
        authHeaders = new Dictionary<string, string>(){
            {"access-token", accessToken},
            {"token-type", "Bearer"},
            {"client", client},
            {"expiry", ""},
            {"uid", uid}
        };
    }
    
    protected void TestAuth (APIResponseHandler handler) {
        var path = string.Format("/auth/validate_token?uid={0}&client={1}&access-token={2}", accessToken, client, uid);
        StartCoroutine(Get(path, handler));
    }
    
    protected void TestAuthWithReturnToMain () {
        TestAuth(HandleTestAuthWithReturnToMain);
    }
    
    void HandleTestAuthWithReturnToMain (WWW www) {
        Debug.Log("Handle Test Auth: " + www.error);
        var status = www.responseHeaders["STATUS"];
        
        if (!status.Contains("200")) {
            SceneManager.LoadScene(0);
        }
        
        // if(www.responseHeaders.Count > 0) {
        //     foreach(KeyValuePair<string, string> entry in www.responseHeaders) {
        //         Debug.Log(entry.Value + "=" + entry.Key);
        //     }
        // }     
    }
    
    protected IEnumerator Get (string path, APIResponseHandler handler) {
        var url = string.Format("{0}{1}", RootUrl, path);
        Debug.Log("GET " + url);
        var www = new WWW(url);
        
        yield return www;
        Debug.Log("After yield");
        handler(www);
    }
    
}
