using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleFrameCastingView : View {
	
	public TextMesh label;
	public TextMesh val;
	
	public AbilitiesController abilityController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (abilityController.currentCastingAbility != null) {
			Enable();
			UpdateCastingPercent();
		} else {
			Disable();
		}
	}
	
	void Disable () {
		label.gameObject.SetActive(false);
		val.gameObject.SetActive(false);
	}
	
	void Enable () {
		label.gameObject.SetActive(true);
		val.gameObject.SetActive(true);
	}
	
	void UpdateCastingPercent () {
		var ratio = abilityController.currentCastingTime / abilityController.currentCastingAbility.castingTime;
		var percent = Mathf.Round(ratio * 100);
		val.text = string.Format("{0}", percent);
	}
}
