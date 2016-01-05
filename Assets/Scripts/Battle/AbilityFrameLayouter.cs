using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AbilityFrameLayouter : SimulationBehaviour {
    
    public Vector3 initialScale = new Vector3(0.3f, 0.3f, 0.3f);
    public Vector3 initialOffset = new Vector3(0f, -1f, 0f);
    public Vector3 initialSpacing = new Vector3(0.5f, 0f, 0f);
    public float initialCountShift = 0.2f;
    
    public float activeAnimationTime = 0.2f;
    public float resetAnimationTime = 0.2f;
    public Vector3 activeScale = new Vector3(1f, 1f, 1f);
    public Vector3 activeOffset = new Vector3(0f, 1.5f, 0f);
    public Vector3 activeSpacing = new Vector3(1.5f, 0f, 0f);
    public float activeCountShift = 0.5f;
    
    List<Vector3> initialPositions;
    List<Vector3> finalPositions;
    
    public Dictionary<string, AbilityFrameController> abilityFrames;
    List<AbilityFrameController> abilityList;
    
    bool isActive = false;

	// Use this for initialization
	void Start () {
        NotificationCenter.AddObserver(this, Notifications.OnBattleFrameFocusSelect);
        NotificationCenter.AddObserver(this, Notifications.OnAbilityResolved);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void PositionAbilityFrames () {
        
        abilityList = abilityFrames.Values.ToList();
        var abilityCount = abilityList.Count;
        var abilityCountShift = -abilityCount * initialCountShift;
        initialOffset.x += abilityCountShift;
        
        initialPositions = new List<Vector3>();
        finalPositions = new List<Vector3>();
        activeCountShift = -abilityCount * activeCountShift;
        activeOffset.x += activeCountShift;
        
        
        for (int i = 0; i < abilityFrames.Count; i++) {
            
            var abilityFrameController = abilityList[i];
            
            // var localPos = abilityFrameController.transform.localPosition;
            var localPos = initialOffset + initialSpacing * i;
            var finalPos = activeOffset + activeSpacing * i;
            
            initialPositions.Add(localPos);
            finalPositions.Add(finalPos);
            
            abilityFrameController.initialPosition = localPos;
            abilityFrameController.activePosition = finalPos;
            
            abilityFrameController.transform.localPosition = localPos;    
            abilityFrameController.transform.localScale = initialScale;
        }
    }
    
    public void Activate () {
        if (isActive) {
            return;
        }
        
        Debug.Log("Activating abilities");
        isActive = true;
        for (int i = 0; i < abilityList.Count; i++) {
            var abilityFrameController = abilityList[i];
            ActivateController(abilityFrameController);
        }
    }
    
   
    
    public void Deactivate () {
        if (!isActive) {
            return;
        }
        Debug.Log("Deactivating abilities");
        isActive = false;
        for (int i = 0; i < abilityList.Count; i++) {
            var abilityFrameController = abilityList[i];
            DeactivateController(abilityFrameController);
        }
    }
    
    void ActivateController (AbilityFrameController controller) {
        var activeAnimationFSM = controller.stateMachine("Active Animation");
        var resetAnimationFSM = controller.stateMachine("Reset Animation");
        resetAnimationFSM.enabled = false;
        activeAnimationFSM.enabled = true;
        // activeAnimationFSM.Fsm.Stop();
        // activeAnimationFSM.Fsm.Start();
    }
    
    public void DeactivateController (AbilityFrameController controller) {
        var activeAnimationFSM = controller.stateMachine("Active Animation");
        var resetAnimationFSM = controller.stateMachine("Reset Animation");
        activeAnimationFSM.enabled = false;
        resetAnimationFSM.enabled = true;
        // resetAnimationFSM.Fsm.Stop();
        // resetAnimationFSM.Fsm.Start();
    }
}
