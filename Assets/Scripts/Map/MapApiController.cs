using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MapApiController : APIBehaviour {

	// Use this for initialization
	void Start () {
	   if (SceneManager.GetActiveScene().buildIndex != 0) {
            TestAuthWithReturnToMain();     
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
