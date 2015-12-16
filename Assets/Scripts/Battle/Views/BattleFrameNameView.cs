using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TextMesh))]
public class BattleFrameNameView : View {
	
	public BattleFrameController controller;
	
	Character character {
		get {
			return controller.character;
		}
	}
	
	TextMesh textMesh;
	
	void Start () {
		textMesh = GetComponent<TextMesh>();
	}
	
	void Update () {
		textMesh.text = character.name;
	}
}
