using UnityEngine;
using System.Collections;

public class AccountManagementController : MonoBehaviour {
    
    public GameObject buttonsContainer;
    public GameObject loginContainer;
    public GameObject signupContainer;

	// Use this for initialization
	void Start () {
	   ActivateButtons();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void ActivateButtons () {
        buttonsContainer.SetActive(true);
        loginContainer.SetActive(false);
        signupContainer.SetActive(false);
    }
    
    public void ActivateLogin () {
        buttonsContainer.SetActive(false);
        loginContainer.SetActive(true);
        signupContainer.SetActive(false);
    }
    
    public void ActivateSignup () {
        buttonsContainer.SetActive(false);
        loginContainer.SetActive(false);
        signupContainer.SetActive(true);
    }
}
