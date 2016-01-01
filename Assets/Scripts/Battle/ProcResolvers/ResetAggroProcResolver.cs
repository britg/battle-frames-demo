using UnityEngine;
using System.Collections;

public class ResetAggroProcResolver : IProcResolver {

	public void Resolve (ProcContext procContext) {
        Debug.Log("Reset aggro proc resolver " + procContext);   
    }
}
