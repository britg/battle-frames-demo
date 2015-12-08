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
	
	BattleController _battleController;
	public BattleController battleController {
		get {
			if (_battleController == null) {
				_battleController = GameObject.Find("Battle").GetComponent<BattleController>();
			}
			return _battleController;
		}
	}
	
	Battle _battle;
	public Battle battle {
		get {
			if (_battle == null) {
				_battle = battleController.battle;
			}
			return _battle;
		}
	}
	
}
