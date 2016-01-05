using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
	
	GameTimeController _gameTimeController;
	public GameTimeController gameTimeController {
		get {
			if (_gameTimeController == null) {
				_gameTimeController = GameObject.Find("Simulation").GetComponent<GameTimeController>();
			}
			return _gameTimeController;
		}
	}
	
	public GameTime gameTime {
		get {
			return gameTimeController.gameTime;
		}
	}
	
	public float gameTimeMultiplier {
		get {
			return gameTime.config.currentTimeMultiplier;
		}
	}
	
	public float gameDeltaTime {
		get {
			return gameTimeController.gameDeltaTime;
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
    
    public List<PlayMakerFSM> stateMachines {
        get {
            return GetComponents<PlayMakerFSM>().ToList();
        }
    }
    
    public PlayMakerFSM stateMachine (string name) {
        foreach (var fsmComponent in stateMachines) {
            if (fsmComponent.FsmName == name) {
                return fsmComponent;
            }
        }
        return null;
    }
	
}
