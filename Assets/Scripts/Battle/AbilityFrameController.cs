using UnityEngine;
using System.Collections;

public class AbilityFrameController : SimulationBehaviour {
    
    public AbilitiesController parentAbilitiesController;
    public Ability ability;
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnAbilityInputDown () {
        Debug.Log("On ability input down (" + gameObject.name + ")");
    }
}
