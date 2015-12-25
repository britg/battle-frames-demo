using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AI_HighestAggroTargetter : BattleFrameBehaviour {

	// Use this for initialization
	void Start () {
		float randomStart = Random.Range(0f, 3f);
        StartEvaluating();
	}
	
	void StartEvaluating () {
		gameTime.SecondChange += SetTarget;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void SetTarget () {
		List<KeyValuePair<BattleFrameController, float>> list = battleFrameController.aggroProfile.ToList();
		
        list.Sort((firstPair, nextPair) => {
			return firstPair.Value.CompareTo(nextPair.Value);
		});
        
        var highestAggro = list.LastOrDefault();
        if (highestAggro.Key != null) {
            //Debug.Log("Highest target with aggro is " + highestAggro.Key);
            battleFrameController.currentTarget = highestAggro.Key;    
        }
        // } else {
        //     Debug.Log("No targets with aggro, doing nothing");    
        // }
	}
}
