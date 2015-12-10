using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Character : JSONResource {
	
	public Character (string _key) : base(_key) {
		stats = new StatProfile(this);
	}
	
	public void Initialize () {
		AssignFields();
		stats = new StatProfile(this);
	}
	
	public StatProfile stats;
	
	public List<Ability> abilities = new List<Ability>();
	
	public List<Item> equipment = new List<Item>();
	
	public Battle.Side currentBattleSide = Battle.Side.Neutral;
	
	public override string ToString () {
		return name;
	}
}
