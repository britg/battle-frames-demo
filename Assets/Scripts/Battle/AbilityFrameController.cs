using UnityEngine;
using System.Collections;

public class AbilityFrameController : SimulationBehaviour {
    
    public AbilitiesController parentAbilitiesController;
    public Ability ability;
    
    public Vector3 preAnimationPosition;
    
    public Vector3 focusAnimationDelta = new Vector3(0, 0, -0.15f);
    public float focusAnimationTime = 0.1f;
    bool focusAnimated = false;
    
    bool coolingDown = false;
    float currentCoolDown = 0f;
    
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (coolingDown) {
            UpdateCooldown();
        }
	}
    
    void OnFocusDown () {
        Debug.Log("On ability focus down (" + gameObject.name + ")");
        AnimateFocus();
    }
    
    void OnFocusSelect () {
        Debug.Log("On ability focus select " + gameObject.name);
        AttemptAbilityActivation();
        ReverseFocusAnimation();
    }
    
    void AnimateFocus () {
        focusAnimated = true;
        preAnimationPosition = transform.position;
        iTween.MoveBy(gameObject, focusAnimationDelta, focusAnimationTime);
    }
    
    void ReverseFocusAnimation () {
        if (focusAnimated) {
            focusAnimated = false;
            iTween.MoveTo(gameObject, preAnimationPosition, focusAnimationTime);    
        }
    }
    
    void AttemptAbilityActivation () {
        if (ability.requiresTargetSelection) {
            Debug.Log("Ability requires target!");
            return;
        }
        
        if (coolingDown) {
            Debug.Log("Ability on cooldown!");
            return;
        }     
        
        Debug.Log("Activating ability immediately!");
        coolingDown = parentAbilitiesController.StartAbility(ability);
    }
    
    void SetAbilityTarget (GameObject targetObj) {
        Debug.Log("Setting ability target! " + targetObj);
        ReverseFocusAnimation();
        var targetController = targetObj.GetComponent<BattleFrameController>();
        if (targetController == null) {
            Debug.Log("Target is not valid! ");
            return;
        }
        
        if (coolingDown) {
            Debug.Log("Ability on cooldown!");
            return;
        }
        
        coolingDown = parentAbilitiesController.StartAbility(ability, targetController);
    }
    
    void UpdateCooldown () {
        currentCoolDown += gameDeltaTime;
        if (currentCoolDown >= ability.cooldown) {
            coolingDown = false;
            currentCoolDown = 0f;
        }
    }
}
