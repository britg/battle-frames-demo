using UnityEngine;
using System.Collections;

public class AbilityResolver {
    
    AbilityContext abilityContext;
	Ability ability;
    
    public AbilityResolver (AbilityContext _abilityContext) {
        abilityContext = _abilityContext;
        ability = abilityContext.ability;
    }
	
	public void AddProcsToStack (ProcStack procStack) {
		// Debug.Log(string.Format("Ability Resolver: {0} - {1} -> {2}",
			// ability, caster, target));
			
		foreach (Proc proc in ability.procs) {
            var procContext = new ProcContext(proc, abilityContext);
            procStack.Add(procContext);
		}
	}
	
}
