using UnityEngine;
using System.Collections;

public class BasicAttackController : BattleFrameBehaviour {
	
	public float attackSpeed;
	public bool hasTarget;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		// TODO get attack speed in game time.
		attackSpeed = character.stats.CurrentValue(Stat.AttackSpeed);
		hasTarget = (currentTarget != null);
	}
	
	public void FSM_Handler_PerformAttack () {
		Debug.Log(character.name + "Performing attack -> " + currentTarget.character.name);	
	}
}
