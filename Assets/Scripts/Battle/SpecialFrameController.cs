using UnityEngine;
using System.Collections;

public class SpecialFrameController : SimulationBehaviour {
    
    public SpecialsController parentSpecialsController;
    public Special special;

	 public Vector3 preAnimationPosition;
    
    public Vector3 focusAnimationDelta = new Vector3(0, 0, -0.15f);
    public float focusAnimationTime = 0.1f;
    bool focusAnimated = false;
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnFocusDown () {
        Debug.Log("On special focus down (" + gameObject.name + ")");
        AnimateFocus();
    }
    
    void OnFocusSelect () {
        Debug.Log("On special focus select " + gameObject.name);
        AttemptSpecialActivation();
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
    
    void AttemptSpecialActivation () {
        if (special.requiresTargetSelection) {
            Debug.Log("Special requires target!");
        } else {
            Debug.Log("Activating special immediately!");
            parentSpecialsController.StartSpecial(special);
        }
    }
    
    void SetSpecialTarget (GameObject targetObj) {
        
        if (!special.requiresTargetSelection) {
            return;
        }
        
        Debug.Log("Setting ability target! " + targetObj);
        ReverseFocusAnimation();
        var targetController = targetObj.GetComponent<BattleFrameController>();
        if (targetController == null) {
            Debug.Log("Target is not valid! ");
            return;
        }
        // parentSpecialsController.StartAbility(ability, targetController);
    }
}
