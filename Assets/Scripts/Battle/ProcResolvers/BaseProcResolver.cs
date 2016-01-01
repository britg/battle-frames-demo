using UnityEngine;
using System.Collections;

public class BaseProcResolver : IProcResolver {
    
    static BaseProcResolver instance;
	public static BaseProcResolver Instance {
		get {
			if (instance == null) {
				instance = new BaseProcResolver();
			}
			return instance;
		}
	}

    public void Resolve (ProcContext procContext) {
        var proc = procContext.proc;
        var target = procContext.target;
        foreach (StatChange statChangeTemplate in proc.baseStatChanges) {
			var statChange = proc.GenerateStatChange(statChangeTemplate.key);
			ApplyStatChange(statChange, target);
		}
    }
    
    /*
	 *	TODO: We need to apply stat change modifiers
	 * 		  to the proc'd value using:
	 *			- character's level
	 *			- character's equipment
	 */
    
    void ApplyStatChange (StatChange statChange, BattleFrameController target) {
		Debug.Log("Applying stat change " + statChange);
		
		target.ChangeStat(statChange.key, statChange.finalValue);
	}
	
}
