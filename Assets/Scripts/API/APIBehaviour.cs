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
        UpdateAuthHeaders();
    }
    
    void UpdateAuthHeaders () {
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
    
    public void SetAccessToken (string _accessToken, string _client, string _uid) {
        PlayerPrefs.SetString("access-token", _accessToken);
        PlayerPrefs.SetString("client", _client);
        PlayerPrefs.SetString("uid", _uid);
        UpdateAuthHeaders();
    }
    
    public void TestAuth (APIResponseHandler handler) {
        var path = string.Format("/auth/validate_token?uid={0}&client={1}&access-token={2}", uid, client, accessToken);
        Get(path, handler);
    }
    
    protected void TestAuthWithReturnToMain () {
        TestAuth((WWW www) => {
            Debug.Log("Handle Test Auth: " + www.error);
            var status = www.responseHeaders["STATUS"];
            
            if (!status.Contains("200")) {
                SceneManager.LoadScene(0);
            }    
        });
    }
    
    public void TestUser (APIResponseHandler successHandler, APIResponseHandler errorHandler) {
        var path = string.Format("/api/user");
        Get(path, (response) => {
            var status = response.responseHeaders["STATUS"];
            if (status.Contains("200")) {
                WriteUserInfo(JSON.Parse(response.text));
                successHandler.Invoke(response);
            } else {
                errorHandler.Invoke(response);
            }
        });
    }
    
    protected void TestUserWithReturnToMain () {
        TestUser((successResponse) => {
            Debug.Log("success: " + successResponse.text);
        }, (errorResponse) => {
            SceneManager.LoadScene(0);
        });
    }
    
    protected void WriteUserInfo (JSONNode userInfo) {
        PlayerPrefs.SetString("currentTileJSON", userInfo["current_tile"].AsObject.ToString());
        
        var handle = userInfo["nickname"].Value;
        PlayerPrefs.SetString("handle", handle);
    }
    
    Dictionary<string, string> Headers () {
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers["client"] = PlayerPrefs.GetString("client");
        headers["access-token"] = PlayerPrefs.GetString("access-token");
        headers["uid"] = PlayerPrefs.GetString("uid");
        return headers;
    }
    
    protected void Get (string path, APIResponseHandler handler) {
        StartCoroutine(GetRequest(path, handler));
    }
    
    protected IEnumerator GetRequest (string path, APIResponseHandler handler) {
        var url = string.Format("{0}{1}", RootUrl, path);
        Debug.Log(string.Format("Headers: client={0}, access-token={1}, uid={2}", PlayerPrefs.GetString("client"), PlayerPrefs.GetString("access-token"), PlayerPrefs.GetString("uid")));
        Debug.Log("GET " + url);
        var www = new WWW(url, null, Headers());
        
        yield return www;
        handler(www);
    }
    
    protected void Post (string path, Dictionary<string, string> body, APIResponseHandler handler) {
        StartCoroutine(PostRequest(path, body, handler));
    }
    
    protected IEnumerator PostRequest (string path, Dictionary<string, string> body, APIResponseHandler handler) {
        var url = string.Format("{0}{1}", RootUrl, path);
        Debug.Log("POST " + url);
        var form = new WWWForm();
        foreach (KeyValuePair<string, string> kv in body) {
            form.AddField(kv.Key, kv.Value);
        }
        var www = new WWW(url, form.data, Headers());
        
        
        yield return www;
        handler(www);
    }
    
}
