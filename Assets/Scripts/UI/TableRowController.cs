using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TableRowController : MonoBehaviour {
	
	public string key = "[key]";
	public string val = "[val]";
	
	public Text keyText;
	public Text valText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		keyText.text = key;
		valText.text = val;
	}
}
