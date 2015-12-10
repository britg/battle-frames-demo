using UnityEngine;
using System.Collections;

public class TargettingController : BattleFrameBehaviour {
	
	public bool isControllable = false;
	
	void Awake () {
		
	}

	// Use this for initialization
	void Start () {
		 
	}
	
	// Update is called once per frame
	void Update () {
		isControllable = (character.currentBattleSide == Battle.Side.Friend);
	}
	
	public void FSM_Handler_Target (GameObject targetObj) {
		Debug.Log("Targetted " + targetObj);
	}
}
