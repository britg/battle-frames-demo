using UnityEngine;
using System.Collections;

public class AbilityResolver {
	
	BattleController battleController;
	Ability ability;
	BattleFrameController caster;
	BattleFrameController target;

	public AbilityResolver (BattleController _battleController,
							Ability _ability,
							BattleFrameController _caster,
							BattleFrameController _target) {
		battleController = _battleController;
		ability = _ability;
		caster = _caster;
		target = _target;	
	}
	
	public void Resolve () {
		// Debug.Log(string.Format("Ability Resolver: {0} - {1} -> {2}",
			// ability, caster, target));
			
		foreach (Proc proc in ability.procs) {
            var procResolver = Proc.Resolver(proc.key);
            var procContext = new ProcContext();
            procContext.proc = proc;
            procContext.ability = ability;
            procContext.battleController = battleController;
            procContext.caster = caster;
            procContext.target = target;
            procResolver.Resolve(procContext);
		}
	}
	
}
