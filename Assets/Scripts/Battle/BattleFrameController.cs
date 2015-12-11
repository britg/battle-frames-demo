using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BattleFrameController : SimulationBehaviour {
	
	public Character character;
	public BattleFrameController currentTarget;
	
	public Dictionary<BattleFrameController, float> aggroProfile;
	

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetCharacter (Character _character) {
		character = _character;
		EnableAI();
	}
	
	void EnableAI () {
		foreach (string aiName in character.ai) {
			EnableAIBehaviour(aiName);
		}
	}
	
	void EnableAIBehaviour (string name) {
		var componentName = "AI_" + name;
		var component = gameObject.AddComponent(System.Type.GetType(componentName));
	}
	
	public void SeedAggro (List<BattleFrameController> controllers) {
		aggroProfile = new Dictionary<BattleFrameController, float>();
		foreach (var controller in controllers) {
			aggroProfile[controller] = 0f;
		}
	}
}
