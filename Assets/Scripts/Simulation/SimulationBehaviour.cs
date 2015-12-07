using UnityEngine;
using System.Collections;

public class SimulationBehaviour : MonoBehaviour {

	Simulation _simulation;
	public Simulation simulation {
		get {
			if (_simulation == null) {
				_simulation = GameObject.Find("Simulation").GetComponent<Simulation>();
			}
			return _simulation;
		}
	}
	
	Battle _battle;
	public Battle battle {
		get {
			if (_battle == null) {
				_battle = simulation.battle;
			}
			return _battle;
		}
	}
	
	Character _playerCharacter;
	public Character playerCharacter {
		get {
			if (_playerCharacter == null) {
				_playerCharacter = battle.playerCharacter;
			}
			return _playerCharacter;
		}
	}
}
