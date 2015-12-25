using UnityEngine;
using System.Collections;

public class BasicAttackController : BattleFrameBehaviour {
	
	public float attackSpeed;
	public bool hasTarget;
	public bool isControllable = false;
    
    public Vector3 normalizedDirectionToTarget;
    
    public string attackAnimationFSMName = "Basic Attack Animation FSM";
    PlayMakerFSM attackAnimationFSM;

	// Use this for initialization
	void Start () {
       attackAnimationFSM = statMachine(attackAnimationFSMName); 
	   attackAnimationFSM.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		// TODO get attack speed in game time.
		attackSpeed = character.stats.CurrentValue(Stat.AttackSpeed);
		hasTarget = (currentTarget != null);
		isControllable = (character.currentBattleSide == Battle.Side.Adventurers);
	}
	
	public void PerformBasicAttack () {
		// perform animation here
		// perform animation on targets
		
		CombatLog.Add(string.Format("{0} attacking {1}", battleFrameController.character.name, currentTarget.character.name));
		
		var attackResults = new BasicAttackResolver(
				battleFrameController, 
				currentTarget, 
				battleController
			).Resolve();
		
        if (!battleFrameController.aiControlled) {
            battleController.RollForSpecial(battleFrameController);    
        }
        
        normalizedDirectionToTarget = Vector3.Normalize(currentTarget.transform.position - transform.position);
        
		battleController.DelegateAttackResults(attackResults);
        attackAnimationFSM.enabled = true;
        attackAnimationFSM.Fsm.Stop();
        attackAnimationFSM.Fsm.Start();
	}
	
	public void SetBasicAttackTarget (GameObject targetObj) {
		var targetFrameController = targetObj.GetComponent<BattleFrameController>();
		if (targetFrameController == null) {
			return;
		}
		
		battleFrameController.SetTarget(targetFrameController);  
	}
}
