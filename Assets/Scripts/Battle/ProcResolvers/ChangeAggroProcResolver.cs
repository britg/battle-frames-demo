using UnityEngine;
using System.Collections;
using System.Linq;

public class ChangeAggroProcResolver : IProcResolver {

	public void Resolve (ProcContext procContext) {
        Debug.Log("Change aggro proc resolver called");
        var proc = procContext.proc;
        if (proc.target == Proc.Target.EnemySide) {
            ChangeEnemySide(procContext);
        }
    }
    
    void ChangeEnemySide (ProcContext procContext) {
        var proc = procContext.proc;
        var caster = procContext.caster;
        var enemySide = caster.enemyBattleSide;
        
        var battleController = procContext.battleController;
        var enemyControllers = battleController.controllers[enemySide];
        
        enemyControllers.ForEach(c => {
           c.ChangeAggro(caster, proc.amount);
        });      
    }
}
