using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.Linq;

public class Scenario : JSONResource {
    
    static Battle.Side defaultTopSide = Battle.Side.Mobs;
    static Battle.Side defaultBottomSide = Battle.Side.Adventurers;
    
    Battle battle;
    BattleController battleController;

	public Scenario (string _key) : base(_key) {}
    
    public void Start (BattleController _battleController) {
        battle = new Battle(key);
        battleController = _battleController;
        Configure();
        battleController.Setup();
    }
    
    void Configure () {
        // Load each character and set them in the appropriate side
        var sidesNode = sourceNode["sides"];
        foreach (KeyValuePair<string, JSONNode> kv in sidesNode.AsObject) {
            var side = tpd.ParseEnum<Battle.Side>(kv.Key);
            var charList = kv.Value.AsArray.Childs.ToList();
            charList.ForEach(k => {
               LoadCharacter(k.Value, side); 
            });
        }
        
        battleController.battle = battle;
        
        // Default positions for now
        // TODO: Load dynamically
        battleController.topSide = defaultTopSide;
        battleController.bottomSide = defaultBottomSide;
    }
    
    void LoadCharacter (string charKey, Battle.Side side) {
        var character = new Character(charKey);
        // Debug.Log("character's ability point type is " + character.abilityPointType);
        battle.AddCharacterToSide(character, side);
    }
}


// void Demo () {
		
		
// 		var rootMonster = new Character("rootMonster");
// 		rootMonster.aiControlled = true;
		
// 		var monster2 = new Character("rootMonster");
// 		monster2.name = "Second Mob";
// 		monster2.aiControlled = true;
		
// 		var warrior = new Character("warrior");
// 		warrior.aiControlled = false;
		
// 		var healer = new Character("healer");
// 		healer.aiControlled = false;
// 		var staff = new Item("acolytesStaff");
//         healer.equipment.ReplaceSlot(Item.Slot.MainHand, staff);
		
// 		var battle = new Battle("demo");

// 		battle.AddCharacterToSide(warrior, Battle.Side.Adventurers);
// 		battle.AddCharacterToSide(healer, Battle.Side.Adventurers);
		
// 		battle.AddCharacterToSide(rootMonster, Battle.Side.Mobs);
// 		battle.AddCharacterToSide(monster2, Battle.Side.Mobs);
		
// 		var battleObj = GameObject.Find("Battle");
// 		if (battleObj == null) {
// 			battleObj = Instantiate(battlePrefab);
// 		}
		
// 		var battleController = battleObj.GetComponent<BattleController>();
// 		battleController.battle = battle;
// 		battleController.topSide = Battle.Side.Mobs;
// 		battleController.bottomSide = Battle.Side.Adventurers;
		
// 		battleController.Setup();
        
//         // var healAll = new Special("healAll");
//         // battleController.specialsController.AddSpecial(healAll);
// 	}
