using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleSideLayouter : MonoBehaviour {
	
	public Battle.Side battleSide;
	
	public Dictionary<string, List<GameObject>> characterClasses
		= new Dictionary<string, List<GameObject>>();

	public void LayoutFrames () {
		
		var friendCount = 0;
		var enemyCount = 0;
		
		foreach (Transform t in transform) {
			var frameController = t.gameObject.GetComponent<BattleFrameController>();
			if (frameController != null) {
				
				// TMP
				if (battleSide == Battle.Side.Adventurers) {
					t.position = new Vector3(-1 + (friendCount*-3), -1, -1);
					friendCount++;	
				} else {
					t.position = new Vector3(-1 + (enemyCount*-3), 1, -1);
					enemyCount++;
				}
				
				
				// TODO: Actual layout engine
			}
		}
	}
}
