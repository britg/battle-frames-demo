using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AbilityPointType {
	Fury,
	Mana,
	Preparation
}

public enum CharacterClass {
	Warrior,
	Cleric,
	Thief,
	Mage,
	Hunter
}

[System.Serializable]
public class Character : JSONResource {
	
	public Character (string _key) : base(_key) {
		stats = new StatProfile(this);
		aiProfile = new AIProfile(this);
        abilities = new AbilityProfile(this);
        equipment = new EquipmentProfile(this);
	}
	
	public CharacterClass characterClass;
	public AbilityPointType abilityPointType;
	
	
	public StatProfile stats;
	public bool aiControlled = true;
	public AIProfile aiProfile;
	
	public List<string> ai {
		get {
			return aiProfile.AIList;
		}
	}
	
	public AbilityProfile abilities = new AbilityProfile();
	public Ability defaultFriendlyAbility {
        get {
            return abilities.DefaultFriendlyAbility();
        }
    }
    
    public EquipmentProfile equipment = new EquipmentProfile();
    public Dictionary<float, Special> specialChances {
        get {
            return equipment.SpecialChances();
        }
    }
	
	public Battle.Side currentBattleSide = Battle.Side.Neutral;
	
	public override string ToString () {
		return name;
	}
}
