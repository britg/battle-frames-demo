using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class View : SimulationBehaviour {
	
	Text _text;
	protected Text text {
		get {
			if (_text == null) {
				_text = GetComponent<Text>();
			}
			return _text;
		}
	}

}
