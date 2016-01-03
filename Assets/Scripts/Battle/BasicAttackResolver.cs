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
	
        var primaryResult = AttackResultAgainstTarget(targetController);
		results.Add(primaryResult);
		
		return results; 
	}
    
    AttackResult AttackResultAgainstTarget (BattleFrameController target) {
        var result = new AttackResult();
        
        bool crit = RollForCrit();
        float damage = DamageAgainstTarget(targetController);
        
        // TODO: Also determine if Miss, Glance, Parry, etc.
        
        if (crit) {
            result.type = AttackResult.Type.CriticalHit;
            var critMult = attackerController.character.stats.CurrentValue(Stat.CritMultiplier);
            damage *= critMult;
        } else {
            result.type = AttackResult.Type.Hit;    
        }
		
		result.statKey = Stat.Health;
		result.delta = damage;
		result.fromController = attackerController;
		result.targetController = targetController;
        return result;
    }
    
    float DamageAgainstTarget (BattleFrameController target) {
        var attackerDamage = attackerController.character.stats.statForKey(Stat.Damage);
		return -tpd.Roll(attackerDamage.minValue, attackerDamage.maxValue);
    }
    
    bool RollForCrit () {
        var critChance = attackerController.character.stats.CurrentValue(Stat.CritChance);
        return tpd.RollPercent(critChance);
    }
}
