using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BattleFrameController : SimulationBehaviour {
    
    public List<string> toDisableOnAbilitySelect;
    
    public GameObject damageTextPrefab;
    
    [HideInInspector]
    public Vector3 preAnimationPosition;
    public Vector3 focusAnimationDelta = new Vector3(0, 0, -0.15f);
    public float focusAnimationTime = 0.1f;
    bool focusAnimated = false;
    bool hasFocus = false;
    bool announcedDeath = false;
    bool abilitiesPresented = false;
    
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
    
    public Battle.Side enemyBattleSide {
        get {
            return (currentBattleSide == Battle.Side.Adventurers 
                       ? Battle.Side.Mobs 
                       : Battle.Side.Adventurers);
        }
    }
    
    
	// Use this for initialization
	void Start () {
        NotificationCenter.AddObserver(this, Notifications.OnBattleFramePresentedAbilities);
        NotificationCenter.AddObserver(this, Notifications.OnBattleFrameHidAbilities);
        NotificationCenter.AddObserver(this, Notifications.OnBattleFrameLostFocus);
        NotificationCenter.AddObserver(this, Notifications.OnAbilityBeginCasting);
        NotificationCenter.AddObserver(this, Notifications.OnAbilityResolved);
	}
	
	public void OnFocusDown () {
		var d = iTween.Hash(Notifications.Keys.Controller, this);
		NotificationCenter.PostNotification(Notifications.OnBattleFrameFocusDown, d);
        hasFocus = true;
        AnimateFocus();
	}
	
	public void OnFocusSelect () {
		var d = iTween.Hash(Notifications.Keys.Controller, this);
		NotificationCenter.PostNotification(Notifications.OnBattleFrameFocusSelect, d);
	}
    
    void OnBattleFrameLostFocus () {
        if (hasFocus) {
            Debug.Log("Battle frame lost focus " + gameObject.name);
            hasFocus = false;
            ReverseFocusAnimation();    
        }
    }
    
    void AnimateFocus () {
        Debug.Log("Animating focus");
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
    
    void OnBattleFramePresentedAbilities (Notification n) {
        DisableInputStateMachines();
        var target = n.data[Notifications.Keys.Controller] as BattleFrameController;
        if (target == this) {
            Debug.Log("On abilities shown " + character);
            abilitiesPresented = true;    
        }
    }
    
    void OnBattleFrameHidAbilities () {
        EnableInputStateMachines();
        
        if (!abilitiesPresented) {
            return;
        }
        
        NotificationCenter.PostNotification(Notifications.OnBattleFrameLostFocus);
        abilitiesPresented = false;
    }
    
    void OnAbilityBeginCasting () {
        Debug.Log("On ability begin casting");
        NotificationCenter.PostNotification(Notifications.OnBattleFrameLostFocus);
    }
    
    void OnAbilityResolved () {
        Debug.Log("On ability resolved");
        NotificationCenter.PostNotification(Notifications.OnBattleFrameLostFocus);
    }
    
    void DisableInputStateMachines () {
        foreach (string fsmName in toDisableOnAbilitySelect) {
            var fsm = stateMachine(fsmName);
            fsm.enabled = false;
        }
    }
    
    void EnableInputStateMachines () {
        foreach (string fsmName in toDisableOnAbilitySelect) {
            var fsm = stateMachine(fsmName);
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
	
	public void SeedAggro () {
		aggroProfile = new Dictionary<BattleFrameController, float>();
	}
	
	public void ChangeAggro (BattleFrameController toEnemy, float amount) {
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
	}
    
    void OnFinishedTargetting () {
        Debug.Log("On finished targetting");
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
		// Debug.Log("Receiving attack result: " + attackResult);
        ChangeStat(attackResult.statKey, attackResult.delta);
        DisplayAttackResult(attackResult);
	}
	
	void DeliverAttack (AttackResult attackResult) {
		// Debug.Log("Delivering attack result: " + attackResult);
        if (character.generatesAbilityPointsFromBasicAttack) {
            // TMP implmenetation
            character.stats.ChangeStat(Stat.AbilityPoints, character.stats.CurrentValue(Stat.AbilityProduction));
        }
	}
    
    public void ChangeStat (string statKey, float amount) {
        character.stats.ChangeStat(statKey, amount);
        CheckDeath();
    }
    
    void DisplayAttackResult (AttackResult result) {
        var damageTextObj = Instantiate(damageTextPrefab) as GameObject;
        damageTextObj.transform.position = transform.position;
        var damageTextView = damageTextObj.GetComponent<DamageTextView>();
        damageTextView.SetResult(result);
    }
    
    void CheckDeath () {
        var currentHealth = character.stats.CurrentValue(Stat.Health);
        if (currentHealth < 1 && !announcedDeath) {
            AnnounceDeath();
        }
    }
    
    void AnnounceDeath () {
        announcedDeath = true;
        battleController.HandleBattleFrameDeath(this);
    }
	
	
}
