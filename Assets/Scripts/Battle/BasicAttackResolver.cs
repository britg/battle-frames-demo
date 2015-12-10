using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicAttackResolver {
	
	BattleFrameController attackerController;
	BattleFrameController targetController;
	BattleController battleController;
	
	public BasicAttackResolver (
		BattleFrameController _attackerController, 
		BattleFrameController _targetController, 
		BattleController _battleController ) {
		attackerController = _attackerController;
		targetController = _targetController;
		battleController = _battleController;
	}
	
	public List<AttackResult> Resolve () {
		var results = new List<AttackResult>();
		
		// TODO use actual calculation to get an attack result;
		var result = new AttackResult();
		result.type = AttackResult.Type.Hit;
		result.statKey = Stat.Health;
		var attackerSpeed = attackerController.character.stats.CurrentValue(Stat.AttackSpeed);
		var attackerDPS = attackerController.character.stats.CurrentValue(Stat.DPS);
		result.delta = -attackerDPS/attackerSpeed;
		result.fromController = attackerController;
		result.targetController = targetController;
		
		results.Add(result);
		
		return results; 
	}
}
