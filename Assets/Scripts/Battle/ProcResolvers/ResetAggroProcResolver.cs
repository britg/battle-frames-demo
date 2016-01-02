using UnityEngine;
using System.Collections;

public class ResetAggroProcResolver : IProcResolver {

	public void Resolve (ProcContext procContext) {
        Debug.Log("Reset aggro proc resolver " + procContext);
        var proc = procContext.proc;
        if (proc.target == Proc.Target.EnemySide) {
            ResetEnemySide(procContext);
        }
    }
    
    void ResetEnemySide (ProcContext procContext) {
        var proc = procContext.proc;
        var caster = procContext.caster;
        var enemySide = caster.enemyBattleSide;
        
        var battleController = procContext.battleController;
        var enemyControllers = battleController.controllers[enemySide];
        
        enemyControllers.ForEach(c => {
            c.SeedAggro();
        });      
    }
}
