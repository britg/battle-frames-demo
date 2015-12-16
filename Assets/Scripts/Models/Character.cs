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
	
	public List<Ability> abilities = new List<Ability>();
	public Ability defaultFriendlyAbility;
	
	public List<Item> equipment = new List<Item>();
	
	public Battle.Side currentBattleSide = Battle.Side.Neutral;
	
	public override string ToString () {
		return name;
	}
}
