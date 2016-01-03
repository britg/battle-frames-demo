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
		var attackerDamage = attackerController.character.stats.statForKey(Stat.Damage);
		result.delta = -tpd.Roll(attackerDamage.minValue, attackerDamage.maxValue);
		result.fromController = attackerController;
		result.targetController = targetController;
		
		results.Add(result);
		
		return results; 
	}
}
