using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BattleFrameController : SimulationBehaviour {
    
    public List<string> toDisableOnAbilitySelect;
    
    [HideInInspector]
    public Vector3 preAnimationPosition;
    public Vector3 focusAnimationDelta = new Vector3(0, 0, -0.15f);
    public float focusAnimationTime = 0.1f;
    bool focusAnimated = false;
    
    
	[HideInInspector]
	public Character character;
    [HideInInspector]
	public BattleFrameController currentTarget;
	
	public Dictionary<BattleFrameController, float> aggroProfile;
	
	public bool aiControlled {
		get {
			return character.aiControlled;
		}
	}
	
	public Battle.Side currentBattleSide {
		get {
			return character.currentBattleSide;
		}
	}
    
    
	// Use this for initialization
	void Start () {
        NotificationCenter.AddObserver(this, Notifications.OnBattleFramePresentedAbilities);
        NotificationCenter.AddObserver(this, Notifications.OnBattleFrameHidAbilities);
        NotificationCenter.AddObserver(this, Notifications.OnBattleFrameLostFocus);
	}
	
	public void OnFocusDown () {
		var d = iTween.Hash(Notifications.Keys.Controller, this);
		NotificationCenter.PostNotification(Notifications.OnBattleFrameFocusDown, d);
        AnimateFocus();
	}
	
	public void OnFocusSelect () {
		var d = iTween.Hash(Notifications.Keys.Controller, this);
		NotificationCenter.PostNotification(Notifications.OnBattleFrameFocusSelect, d);
	}
    
    void OnBattleFrameLostFocus () {
        ReverseFocusAnimation();
    }
    
    void AnimateFocus () {
        focusAnimated = true;
        preAnimationPosition = transform.position;
        iTween.MoveBy(gameObject, focusAnimationDelta, focusAnimationTime);
    }
    
    void ReverseFocusAnimation () {
        if (focusAnimated) {
            focusAnimated = false;
            iTween.MoveTo(gameObject, preAnimationPosition, focusAnimationTime);    
        }
    }
    
    void OnBattleFramePresentedAbilities () {
        DisableInputStateMachines();    
    }
    
    void OnBattleFrameHidAbilities () {
        EnableInputStateMachines();
        NotificationCenter.PostNotification(Notifications.OnBattleFrameLostFocus);
    }
    
    void DisableInputStateMachines () {
        foreach (string fsmName in toDisableOnAbilitySelect) {
            var fsm = statMachine(fsmName);
            fsm.enabled = false;
        }
    }
    
    void EnableInputStateMachines () {
        foreach (string fsmName in toDisableOnAbilitySelect) {
            var fsm = statMachine(fsmName);
            fsm.enabled = true;
        }
    }
	
	public void SetCharacter (Character _character) {
		character = _character;
		EnableAI();
	}
	
	void EnableAI () {
		foreach (string aiName in character.ai) {
			EnableAIBehaviour(aiName);
		}
	}
	
	void EnableAIBehaviour (string name) {
		var componentName = "AI_" + name;
		var component = gameObject.AddComponent(System.Type.GetType(componentName));
	}
	
	public void SeedAggro (List<BattleFrameController> controllers) {
		aggroProfile = new Dictionary<BattleFrameController, float>();
		foreach (var controller in controllers) {
			aggroProfile[controller] = 0f;
		}
	}
	
	void ChangeAggro (BattleFrameController toEnemy, float amount) {
		if (!aggroProfile.ContainsKey(toEnemy)) {
			aggroProfile[toEnemy] = amount;
		} else {
			aggroProfile[toEnemy] += amount;
		}
	}
	
	#region Targetting
	
	public void SetTarget (BattleFrameController target) {
		var previousTarget = currentTarget;
		
		if (IsEnemy(target)) {
			currentTarget = target;	
		} else {
			var abilityController = GetComponent<AbilitiesController>();
			abilityController.StartDefaultFriendlyAbility(target);
		}
        
        NotificationCenter.PostNotification(Notifications.OnBattleFrameLostFocus);
	}
		
	#endregion
	
	public void ProcessAttackResult (AttackResult attackResult) {
		
		if (attackResult.targetController == this) {
			ReceiveAttack(attackResult);
		}
		
		if (attackResult.fromController == this) {
			DeliverAttack(attackResult);
		}
		
		if (aiControlled && IsEnemy(attackResult.fromController)) {
			// TODO: // BasicAttackAggroChangeResolver(attackResult, forController) aggroChange()
			float amount = 1f; 
			ChangeAggro(attackResult.fromController, amount);
		}
		
	}
	
	public bool IsEnemy (BattleFrameController other) {
		var opposingControllers = battleController.OpposingControllers(this);
		return opposingControllers.Contains(other);
	}
	
	void ReceiveAttack (AttackResult attackResult) {
		character.stats.ChangeStat(attackResult.statKey, attackResult.delta);
		// Debug.Log("Receiving attack result: " + attackResult);
	}
	
	void DeliverAttack (AttackResult attackResult) {
		// Debug.Log("Delivering attack result: " + attackResult);
	}
	
	
}
