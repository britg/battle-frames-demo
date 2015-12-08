using UnityEngine;
using System.Collections;

public class BattleFrameController : SimulationBehaviour {
	
	public Character character;
	public BattleFrameView view;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetCharacter (Character _character) {
		character = _character;
		// TODO: Do setup stuff like name, etc.
		view.character = character;		
	}
}
