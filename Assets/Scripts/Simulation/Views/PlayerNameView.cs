using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerNameView : View {
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		text.text = playerCharacter.name;
		
	}
}
