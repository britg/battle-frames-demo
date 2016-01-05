﻿using UnityEngine;
using System.Collections;

public class ProcContext {
    public BattleController battleController;
    public Ability ability;
    public BattleFrameController caster;
    public BattleFrameController target;
    
    public Proc proc;
    
    public int currentCount = 0;
    public float currentTimeDelta = 0f;
    
    public bool ShouldExecute () {
        if (proc.occurances > currentCount) {
            if (currentTimeDelta >= proc.occuranceRate) {
                currentTimeDelta = 0f;
                currentCount++;
                return true;
            }
        }
        
        return false;
    }
    
    public bool ExecutionComplete () {
        return currentCount >= proc.occurances;
    }
    
}
