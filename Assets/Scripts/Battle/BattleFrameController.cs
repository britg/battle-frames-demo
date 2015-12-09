using UnityEngine;
using System.Collections;

public class BattleFrameController : SimulationBehaviour {
	
	public Character character;
	public BattleFrameController currentTarget;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetCharacter (Character _character) {
		character = _character;		
	}
}
