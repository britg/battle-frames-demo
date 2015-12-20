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
	
	AbilityController _abilityController;
	public AbilityController abilityController {
		get {
			if (_abilityController == null) {
				_abilityController = GetComponent<AbilityController>();
			}
			return _abilityController;
		}
	}
    
    public bool NotificationIsFromSelf (Notification n) {
        var fromController = n.data[Notifications.Keys.Controller] as BattleFrameController;
        return fromController == battleFrameController;
    }
    
    public bool aiControlled {
        get {
            return battleFrameController.aiControlled;
        }
    }
}
