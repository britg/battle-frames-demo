﻿using UnityEngine;
using System.Collections.Generic;


public class Simulation : MonoBehaviour {
     
    public static string scenarioKey;
	
	public GameObject battlePrefab;
	
	public JSONResourceLoader resourceLoader;

	public Config config;
	
	void Awake () {
		resourceLoader.OnDoneLoading += OnDoneResourceLoading;
		resourceLoader.Load(Application.streamingAssetsPath);
		Debug.Log("Application persistent data path: " + Application.persistentDataPath);
	}
	
	void OnDoneResourceLoading () {
        LoadScenario();
	}
    
    /*
     * In the final implementation we are loading a scenario from
     * the API based on the tile chosen by the player.
     * For now, we will stub out scenarios
     */
    void LoadScenario () {
        Debug.Log("Scenario key is " + scenarioKey);
        Demo();
    }
	
	void Demo () {
		
		
		var rootMonster = new Character("rootMonster");
		rootMonster.aiControlled = true;
		
		var monster2 = new Character("rootMonster");
		monster2.name = "Second Mob";
		monster2.aiControlled = true;
		
		var warrior = new Character("warrior");
		warrior.aiControlled = false;
		
		var healer = new Character("healer");
		healer.aiControlled = false;
		var staff = new Item("acolytesStaff");
        healer.equipment.ReplaceSlot(Item.Slot.MainHand, staff);
		
		var battle = new Battle("demo");

		battle.AddCharacterToSide(warrior, Battle.Side.Adventurers);
		battle.AddCharacterToSide(healer, Battle.Side.Adventurers);
		
		battle.AddCharacterToSide(rootMonster, Battle.Side.Mobs);
		battle.AddCharacterToSide(monster2, Battle.Side.Mobs);
		
		var battleObj = GameObject.Find("Battle");
		if (battleObj == null) {
			battleObj = Instantiate(battlePrefab);
		}
		
		var battleController = battleObj.GetComponent<BattleController>();
		battleController.battle = battle;
		battleController.topSide = Battle.Side.Mobs;
		battleController.bottomSide = Battle.Side.Adventurers;
		
		battleController.Setup();
        
        // var healAll = new Special("healAll");
        // battleController.specialsController.AddSpecial(healAll);
	}
	
}
