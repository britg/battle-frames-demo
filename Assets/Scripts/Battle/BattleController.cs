using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleController : SimulationBehaviour {
	
	public GameObject friendlyContainer;
	public GameObject enemyContainer;
	public GameObject battleFramePrefab;
	
	public new Battle battle;
	
	public void Setup () {
		CreateFrames();
		PositionFrames();		
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
	
}
