using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleController : SimulationBehaviour {
	
	public Battle.Side topSide;
	public Battle.Side bottomSide;
	public GameObject topContainer;
	public GameObject bottomContainer;
    public GameObject specialsContainer;
    public SpecialsController specialsController;
	
	public Dictionary<Battle.Side, List<BattleFrameController>> controllers
		= new Dictionary<Battle.Side, List<BattleFrameController>>();
		
	public List<BattleFrameController> allControllers {
		get {
			var _all = new List<BattleFrameController>();
			foreach (KeyValuePair<Battle.Side, List<BattleFrameController>> side in controllers) {
				_all.AddRange(side.Value);
			}
			return _all;
		}
	}
	
	public List<BattleFrameController> aiControlledControllers {
		get {
			var _aiControlled = new List<BattleFrameController>();
			foreach (var controller in allControllers) {
				if (controller.aiControlled) {
					_aiControlled.Add(controller);
				}
			}
			return _aiControlled;
		}
	}
	
	public GameObject battleFramePrefab;
	
	public new Battle battle;
	
	public void Setup () {
		CreateFrames();
		PositionFrames();
		SeedAggro();
	}
	
	void CreateFrames () {
		foreach (KeyValuePair<Battle.Side, List<Character>> kv in battle.sides) {
			foreach (var character in kv.Value) {
				CreateFrame(character);
			}
		}
	}
	
	void CreateFrame (Character character) {
		var frame = Instantiate(battleFramePrefab) as GameObject;
		
		if (character.currentBattleSide == topSide) {
			frame.transform.SetParent(topContainer.transform);
		} else {
			frame.transform.SetParent(bottomContainer.transform);
		}
		
		frame.name = character.name;
		
		var frameController = frame.GetComponent<BattleFrameController>();
		frameController.SetCharacter(character);
		
		if (!controllers.ContainsKey(character.currentBattleSide)) {
			controllers[character.currentBattleSide] = new List<BattleFrameController>();
		}
		
		var sideControllers = controllers[character.currentBattleSide];
		sideControllers.Add(frameController);
	}
	
	void PositionFrames () {
		var layouters = new List<BattleSideLayouter>();
		layouters.Add(bottomContainer.GetComponent<BattleSideLayouter>());
		layouters.Add(topContainer.GetComponent<BattleSideLayouter>());
		
		foreach (var layouter in layouters) {
			layouter.LayoutFrames(this);
		}
	}
    
    /*
    * Roll for special:
    * Specials are rolled for every time a character attacks.
    * Can't have more than one of the same special available
    */
    public void RollForSpecial (BattleFrameController battleFrameController) {
        var character = battleFrameController.character;
        var chance = character.stats.CurrentValue(Stat.SpecialChance);
        var procd = tpd.RollPercent(chance);
        Debug.Log(string.Format("{0} rolled for special: {1}", character, procd));
        if (procd) {
            ProcSpecial(character.specialChances);
        }
    }
    
    void ProcSpecial(SpecialProfile specialProfile) {
        var exclude = specialsController.specials;
        var special = specialProfile.Roll(exclude);
        if (special == null) {
            return;
        }

        specialsController.AddSpecial(special);
    }
	
	/* 
	* Every battle frame controller gets notified
	* of every attack and chooses what to do with it.
	* e.g. for aggro propagation. 
	*/
	public void DelegateAttackResults (List<AttackResult> attackResults) {
		foreach (AttackResult result in attackResults) {
			DelegateAttackResult(result);
		}
	}
	
	public void DelegateAttackResult (AttackResult attackResult) {
		foreach (var controller in allControllers) {
			controller.ProcessAttackResult(attackResult);
		}
	}
	
	public void SeedAggro () {
		foreach (var controller in allControllers) {
			if (controller.aiControlled) { 
				controller.SeedAggro();
			}
		}
	}
	
	public List<BattleFrameController> OpposingControllers (BattleFrameController controller) {
		var opposing = new List<BattleFrameController>();
		foreach (var other in allControllers) {
			if (other.currentBattleSide != controller.currentBattleSide) {
				opposing.Add(other);
			}
		}
		return opposing;
	}
	
	/*
	 * 	Execute an ability
	 */
	public void ExecuteAbility (Ability ability, BattleFrameController caster, BattleFrameController target) {
		var abilityResolver = new AbilityResolver(
			_ability: ability,
			_battleController: this,
			_caster: caster,
			_target: target
		);
		abilityResolver.Resolve();
        NotificationCenter.PostNotification(Notifications.OnAbilityResolved);
	}
    
    public void ExecuteSpecial (Special special, BattleFrameController targetController) {
        // new SpecialResolver resolver.Resolve()
        //
        // Notify special resolved
    }
    
    public void HandleBattleFrameDeath (BattleFrameController battleFrameController) {
        var battleSide = battleFrameController.currentBattleSide;
        controllers[battleSide].Remove(battleFrameController);
        Destroy(battleFrameController.gameObject);
        
        if (controllers[battleSide].Count < 1) {
            if (battleSide == Battle.Side.Mobs) {
                AnnouncePlayerWin();
            } else {
                AnnouncePlayerLost();
            }
        }
    }
    
    void AnnouncePlayerWin () {
        NotificationCenter.PostNotification(Notifications.OnPlayerWin);   
    }
    
    void AnnouncePlayerLost () {
        NotificationCenter.PostNotification(Notifications.OnPlayerLost);
    }
	
}
