using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleController : SimulationBehaviour {
	
	public GameObject friendlyContainer;
	public List<BattleFrameController> friendlyControllers = new List<BattleFrameController>();
	public GameObject enemyContainer;
	public List<BattleFrameController> enemyControllers = new List<BattleFrameController>();
	
	public GameObject battleFramePrefab;
	
	public new Battle battle;
	
	public void Setup () {
		CreateFrames();
		PositionFrames();
		SeedAggro();
	}
	
	void CreateFrames () {
		foreach (Character c in battle.friendlyCharacters) {
			c.currentBattleSide = Battle.Side.Friend;
			CreateFrame(c);
		}
		
		foreach (Character c in battle.enemyCharacters) {
			c.currentBattleSide = Battle.Side.Enemy;
			CreateFrame(c);
		}
	}
	
	void CreateFrame (Character character) {
		var frame = Instantiate(battleFramePrefab) as GameObject;
		
		if (character.currentBattleSide == Battle.Side.Friend) {
			frame.transform.SetParent(friendlyContainer.transform);
		} else {
			frame.transform.SetParent(enemyContainer.transform);
		}
		
		frame.name = character.name;
		
		var frameController = frame.GetComponent<BattleFrameController>();
		frameController.SetCharacter(character);
		
		if (character.currentBattleSide == Battle.Side.Friend) {
			friendlyControllers.Add(frameController);
		} else {
			enemyControllers.Add(frameController);
		}
		
		 // TODO: Set the frame position, etc.
	}
	
	void PositionFrames () {
		var layouters = new List<BattleSideLayouter>();
		layouters.Add(friendlyContainer.GetComponent<BattleSideLayouter>());
		layouters.Add(enemyContainer.GetComponent<BattleSideLayouter>());
		
		foreach (var layouter in layouters) {
			layouter.LayoutFrames();
		}
	}
	
	public void ProcessAttackResults (List<AttackResult> attackResults) {
		foreach (AttackResult result in attackResults) {
			ProcessAttackResult(result);	
		}
	}
	
	public void ProcessAttackResult (AttackResult attackResult) {
		var stat = attackResult.targetController.character.stats.statForKey(attackResult.statKey);
		stat.currentValue += attackResult.delta;
		Debug.Log(attackResult);
	}
	
	public void SeedAggro () {
		foreach (BattleFrameController enemyController in enemyControllers) {
			enemyController.SeedAggro(friendlyControllers);
		}
	}
	
}
