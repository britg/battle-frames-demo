using UnityEngine;
using System.Collections;

public class GenericAnimationController : BattleBehaviour, IAnimationController {
    
    public AbilityContext abilityContext { get; set; }
    
    public TextMesh text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void Execute () {
        text.gameObject.SetActive(true);
        text.text = abilityContext.ability.name;
    }
}
