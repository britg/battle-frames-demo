using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleController : SimulationBehaviour {
	
	public Battle.Side topSide;
	public Battle.Side bottomSide;
	public GameObject topContainer;
	public GameObject bottomContainer;
	
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
			layouter.LayoutFrames();
		}
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
				controller.SeedAggro(OpposingControllers(controller));
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
	
}
