using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AbilitiesController : BattleFrameBehaviour {
	
    public GameObject abilitiesContainer;
    public GameObject abilityFramePrefab;
    Dictionary<string, AbilityFrameController> abilityFrames = new Dictionary<string, AbilityFrameController>();
    
	public Ability currentCastingAbility;
	public BattleFrameController targetController;
	public float currentCastingTime = 0f;
    
    void Start () {
        NotificationCenter.AddObserver(this, Notifications.OnBattleFrameFocusSelect);
        NotificationCenter.AddObserver(this, Notifications.OnAbilityResolved);
        NotificationCenter.AddObserver(this, Notifications.OnAbilityBeginCasting);
        UpdateAbilityFrames();
        abilitiesContainer.SetActive(false);
    }
    
    void UpdateAbilityFrames () {
        
        var abilityKeys = new List<string>();
        
        // create any missing ability frames
        foreach (Ability ability in character.abilities) {
            abilityKeys.Add(ability.key);
            
            if (abilityFrames.ContainsKey(ability.key)) {
                continue;
            }
            
            CreateAbilityFrame(ability);
        }
        
        // remove any extra ability frames
        var existingKeys = abilityFrames.Keys.ToList();
        foreach (string existingKey in existingKeys) {
            if (!abilityKeys.Contains(existingKey)) {
                var extra = abilityFrames[existingKey];
                abilityFrames.Remove(existingKey);
                Destroy(extra.gameObject);
            }
        }
        
        // Position ability frames
        PositionAbilityFrames();
        
    }
    
    void CreateAbilityFrame (Ability ability) {
        var abilityFrame = Instantiate(abilityFramePrefab) as GameObject;
        abilityFrame.transform.SetParent(abilitiesContainer.transform, false);
        abilityFrame.name = ability.name;
        var abilityFrameController = abilityFrame.GetComponent<AbilityFrameController>();
        abilityFrameController.parentAbilitiesController = this;
        abilityFrameController.ability = ability;
        abilityFrames[ability.key] = abilityFrameController;
    }
    
    void PositionAbilityFrames () {
        foreach (KeyValuePair<string, AbilityFrameController> kv in abilityFrames) {
            var abilityFrameController = kv.Value;
            var localPos = abilityFrameController.transform.localPosition;
            localPos = Vector3.zero;
            localPos.z = -2;
            abilityFrameController.transform.localPosition = localPos;
        }
    }
    
    void OnAbilityInputDown (GameObject targetObj) {
        var abilityFrameController = targetObj.GetComponent<AbilityFrameController>();
        
        if (abilityFrameController == null) {
            HideAbilities();
        }
        
        if (abilityFrameController.parentAbilitiesController == this) {
            Debug.Log("On ability input down (" + gameObject.name + " -> " + abilityFrameController.ability + ")");
        }
        
    }
    
    void OnNonAbilityInputDown (GameObject targetObj) {
        if (abilitiesContainer.activeSelf) {
            // Debug.Log("Hiding abilities from " + gameObject.name);
            HideAbilities();    
        }
    }
    
    void OnAbilityResolved () {
        if (abilitiesContainer.activeSelf) {
            HideAbilities();
        }
    }
    
    void OnAbilityBeginCasting () {
        if (abilitiesContainer.activeSelf) {
            HideAbilities();
        }
    }
    
    void OnBattleFrameFocusSelect (Notification n) {
        if (NotificationIsFromSelf(n) && !aiControlled) {
            PresentAbilities();
        }
    }
	
	public void PresentAbilities () {
		Debug.Log("Presenting abilities from " + gameObject.name);
        NotificationCenter.PostNotification(Notifications.OnBattleFramePresentedAbilities);
        abilitiesContainer.SetActive(true);
        UpdateAbilityFrames();
	}
    
    public void HideAbilities () {
        NotificationCenter.PostNotification(Notifications.OnBattleFrameHidAbilities);
        abilitiesContainer.SetActive(false);
    }
	
	public void PromptAbility (Ability ability) {
		if (ability.abilityPointCost > character.stats.CurrentValue(Stat.AbilityPoints)) {
			LogNotEnoughAbilityPoints(ability);
			return;
		}
		
		currentCastingAbility = ability;
		if (currentCastingAbility.requiresTargetSelection) {
			// TODO Enter target selection mode
		} else {
			StartAbility(ability);
		}
	}
	
	void LogNotEnoughAbilityPoints (Ability ability) {
		Debug.Log(
			string.Format("{0}: Not enough ability points to cast {1}!", 
				character.name, 
				ability.name
			)
		);
	}
	
	public bool StartDefaultFriendlyAbility (BattleFrameController _targetController) {
		if (character.defaultFriendlyAbility == null) {
			return false;
		}
        return StartAbility(character.defaultFriendlyAbility, _targetController);
	}
	
	public bool StartAbility (Ability ability) {
		return StartAbility(ability, null);
	}
	
	public bool StartAbility (Ability ability, BattleFrameController _targetController) {
		if (ability.abilityPointCost > character.stats.CurrentValue(Stat.AbilityPoints)) {
			LogNotEnoughAbilityPoints(ability);
			return false;
		}
        
        NotificationCenter.PostNotification(Notifications.OnAbilityBeginCasting);
        currentCastingAbility = ability;
		targetController = _targetController;
        return true;
	}
	
	void PurchaseAbility (Ability ability) {
		character.stats.ChangeStat(Stat.AbilityPoints, -ability.abilityPointCost);
	}
	
	void Update () {
		UpdateAbility();
	}
	
	void UpdateAbility () {
		if (currentCastingAbility == null) {
			return;
		}
		
		currentCastingTime += gameDeltaTime;
		if (currentCastingTime >= currentCastingAbility.castingTime) {
			FinishCasting();
		}
	}
	
	void FinishCasting () {
		PurchaseAbility(currentCastingAbility);
		battleController.ExecuteAbility(currentCastingAbility, battleFrameController, targetController);
		currentCastingAbility = null;
		currentCastingTime = 0f;		
	}
	
}
