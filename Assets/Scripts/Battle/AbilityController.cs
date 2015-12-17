using UnityEngine;
using System.Collections;

public class AbilityController : BattleFrameBehaviour {
	
	public Ability currentCastingAbility;
	public BattleFrameController targetController;
	public float currentCastingTime = 0f;
	
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
		CombatLog.Add(
			string.Format("{0}: Not enough ability points to cast {1}!", 
				character.name, 
				ability.name
			)
		);
	}
	
	public void StartDefaultFriendlyAbility (BattleFrameController _targetController) {
		if (character.defaultFriendlyAbility == null) {
			return;
		}
		
		if (character.defaultFriendlyAbility.abilityPointCost > character.stats.CurrentValue(Stat.AbilityPoints)) {
			LogNotEnoughAbilityPoints(character.defaultFriendlyAbility);
			return;
		}
		
		currentCastingAbility = character.defaultFriendlyAbility;
		targetController = _targetController;
	}
	
	public void StartAbility (Ability ability) {
		
	}
	
	public void StartAbility (Ability ability, BattleFrameController _targetController) {
		
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
