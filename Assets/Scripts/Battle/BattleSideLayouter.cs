using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleSideLayouter : MonoBehaviour {
    
    public Vector3 totalDimensions = new Vector3(15.33f, 11.48f, 0f);
    public Vector3 sidebarDimensions = new Vector3(3.5f, 11.48f, 0f);
    
    public Vector3 frameDimensions = new Vector3(3.0f, 1.5f, 0.8f);
    
    public float smallerScale;
	
	public Battle.Side battleSide;
	
	public Dictionary<string, List<GameObject>> characterClasses
		= new Dictionary<string, List<GameObject>>();
        
    BattleController battleController;

	public void LayoutFrames (BattleController _battleController) {
        battleController = _battleController;
        
        var frameSpace = totalDimensions - sidebarDimensions;
        var frameCenter = frameSpace/2f;
        // frameCenter.x = -(totalDimensions.x/2f - frameSpace.x);
        frameCenter.x = -sidebarDimensions.x/2f;
        
        
        if (battleSide == battleController.topSide) {
            frameCenter.y = totalDimensions.y/4f;
        } else {
            frameCenter.y = -totalDimensions.y/4f;
        }
        
        // Debug.Log("Frame center for " + battleSide + " is " + frameCenter);
        
        var frameControllers = battleController.controllers[battleSide];
        var frameCenterPoints = new List<Vector3>();
        
        var width = frameSpace.x;
        var refCount = Mathf.Clamp(frameControllers.Count, 3, 5) + 1;
        var spacing = width / refCount;
        
        // Debug.Log("Spacing is " + spacing);
        
        var sign = -1;
        int j = 0;
        for (int i = 0; i < frameControllers.Count; i++) {
            var controller = frameControllers[i];
            var offset = sign * j * spacing;
            var pos = frameCenter;
            pos.x += offset;
            pos.z = controller.transform.position.z;
            // Debug.Log("Placing frame at " + pos);
            controller.transform.position = pos;
            sign *= -1;
            if (sign == 1) {
                j++;
            }
            
            if (refCount > 4) {
                var scale = controller.transform.localScale;
                controller.transform.localScale = scale * smallerScale;
            }
        }
        
        
		
		// var friendCount = 0;
		// var enemyCount = 0;
		
		// foreach (Transform t in transform) {
		// 	var frameController = t.gameObject.GetComponent<BattleFrameController>();
		// 	if (frameController != null) {
				
		// 		// TMP
		// 		if (battleSide == Battle.Side.Adventurers) {
		// 			t.position = new Vector3(-1 + (friendCount*-3), -1, -1);
		// 			friendCount++;	
		// 		} else {
		// 			t.position = new Vector3(-1 + (enemyCount*-3), 1, -1);
		// 			enemyCount++;
		// 		}
				
				
		// 		// TODO: Actual layout engine
		// 	}
		// }
	}
}
