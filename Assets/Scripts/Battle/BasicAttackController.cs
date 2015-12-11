using UnityEngine;
using System.Collections;

public class BasicAttackController : BattleFrameBehaviour {
	
	public float attackSpeed;
	public bool hasTarget;
	public bool isControllable = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		// TODO get attack speed in game time.
		attackSpeed = character.stats.CurrentValue(Stat.AttackSpeed);
		hasTarget = (currentTarget != null);
		isControllable = (character.currentBattleSide == Battle.Side.Friend);
	}
	
	public void PerformBasicAttack () {
		// Debug.Log(character.name + "Performing attack -> " + currentTarget.character.name);
		
		// Create an `AttackResult` object
		// perform animation here
		// perform animation on targets
		
		var attackResults = new BasicAttackResolver(
				battleFrameController, 
				currentTarget, 
				battleController
			).Resolve();
		
		battleController.ProcessAttackResults(attackResults);
	}
	
	public void SetBasicAttackTarget (GameObject targetObj) {
		Debug.Log("Targetted " + targetObj);
		var targetFrameController = targetObj.GetComponent<BattleFrameController>();
		if (targetFrameController == null) {
			return;
		}
		
		battleFrameController.currentTarget = targetFrameController;  
	}
}
