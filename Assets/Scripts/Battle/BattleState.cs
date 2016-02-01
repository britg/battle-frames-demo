using UnityEngine;
using SimpleJSON;

public class BattleState {
    
    JSONClass sourceJSON;
    
    public BattleState (JSONClass battleStateJSON) {
        sourceJSON = battleStateJSON;        
    }
    
    public string currentTurnSide {
        get {
            return sourceJSON["current_turn_side"].Value;
        }
    }
    
    public string mySide {
        get {
            return sourceJSON["my_side"].Value;
        }
    }
    
    public bool isClientTurn {
        get {
            // Debug.Log(string.Format("{0} {1} {2}", currentTurnSide, mySide, currentTurnSide == mySide));
            return currentTurnSide == mySide;
        }
    }
    
    public override string ToString () {
        return string.Format("{0}", sourceJSON.ToString());
    }
}