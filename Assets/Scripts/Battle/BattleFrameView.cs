using UnityEngine;
using System.Collections;

public class BattleFrameView : View {

	Character _character;
	public Character character {
		get {
			return _character;
		}
		set {
			_character = value;
			UpdateName();
		}
	}
	
	public GameObject characterName;
	void UpdateName () {
		var text = characterName.GetComponent<TextMesh>();
		text.text = character.name;
	}
}
