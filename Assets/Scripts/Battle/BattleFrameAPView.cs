using UnityEngine;
using System.Collections;

public class BattleFrameAPView : View {
	
	public BattleFrameController controller;
	
	Character character {
		get {
			return controller.character;
		}
	}
	
	TextMesh textMesh;

	// Use this for initialization
	void Start () {
		textMesh = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		textMesh.text = string.Format("{0}/{1}", character.stats.CurrentValue(Stat.AbilityPoints), character.stats.MaxValue(Stat.AbilityPoints));
	}
}
