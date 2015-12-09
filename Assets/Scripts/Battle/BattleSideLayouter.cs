using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleSideLayouter : MonoBehaviour {
	
	public Battle.Side battleSide;
	
	public Dictionary<string, List<GameObject>> characterClasses
		= new Dictionary<string, List<GameObject>>();

	public void LayoutFrames () {
		foreach (Transform t in transform) {
			var frameController = t.gameObject.GetComponent<BattleFrameController>();
			if (frameController != null) {
				
				// TMP
				if (battleSide == Battle.Side.Friend) {
					t.position = new Vector3(-1, -1, -1);	
				} else {
					t.position = new Vector3(-1, 1, -1);
				}
				
				
				// TODO: Actual layout engine
			}
		}
	}
}
