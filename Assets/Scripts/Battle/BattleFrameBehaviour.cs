using UnityEngine;
using System.Collections;

public class BattleFrameBehaviour : SimulationBehaviour {

	BattleFrameController _battleFrameControler;
	public BattleFrameController battleFrameController {
		get {
			if (_battleFrameControler == null) {
				_battleFrameControler = GetComponent<BattleFrameController>();
			}
			return _battleFrameControler;
		}
	}
	
	Character _character;
	public Character character {
		get {
			if (_character == null) {
				_character = battleFrameController.character;
			}
			return _character;
		}
	}
	
	BattleFrameController _currentTarget;
	public BattleFrameController currentTarget {
		get {
			return battleFrameController.currentTarget;
		}
	}
}
