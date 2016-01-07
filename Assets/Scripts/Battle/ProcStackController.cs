using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ProcStackController : BattleBehaviour {
    
    public ProcStack procStack = new ProcStack();
    public ProcStack removeThisPass = new ProcStack();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	   UpdateProcs();
	}
    
    void UpdateProcs () {
        if (procStack.Count < 1) {
            return;
        }
        
        procStack.ForEach(pc => {
            pc.currentTimeDelta += gameDeltaTime;
            if (pc.ShouldExecute()) {
                PerformProc(pc);
            }
        });
        
        RemoveCompleted();
    }
    
    void PerformProc (ProcContext procContext) {
        
        Debug.Log("Performing procs!!!");
        
        // TODO: If a one-time proc, perform then destory
        // else if recurring, check current delta time and
        // perform
        var proc = procContext.proc;
        var procResolver = Proc.Resolver(proc.key);
        procResolver.Resolve(procContext);
        if (procContext.ExecutionComplete()) {
            removeThisPass.Add(procContext);
        }
    }
    
    void RemoveCompleted () {
        
        if (removeThisPass.Count < 1) {
            return;
        }
        
        Debug.Log("Removing completed procs!");
        
        foreach (ProcContext pc in removeThisPass) {
            procStack.Remove(pc);
        }
        
        removeThisPass = new ProcStack();
    }
}
