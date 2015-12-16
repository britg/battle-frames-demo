using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AI_HighestAggroTargetter : BattleFrameBehaviour {

	// Use this for initialization
	void Start () {
		float randomStart = Random.Range(0f, 3f);
		Invoke("StartEvaluating", randomStart);
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
		battleFrameController.currentTarget = list.LastOrDefault().Key;
	}
}
