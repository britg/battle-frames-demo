using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Character : JSONResource {
	
	public Character (string _key) : base(_key) {
		stats = new StatProfile(this);
		aiProfile = new AIProfile(this);
	}
	
	public StatProfile stats;
	public AIProfile aiProfile;
	
	public List<string> ai {
		get {
			return aiProfile.AIList;
		}
	}
	
	public List<Ability> abilities = new List<Ability>();
	
	public List<Item> equipment = new List<Item>();
	
	public Battle.Side currentBattleSide = Battle.Side.Neutral;
	
	public override string ToString () {
		return name;
	}
}
