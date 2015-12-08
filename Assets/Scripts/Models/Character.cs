using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Character : JSONResource {
	
	public Character (string _key) : base(_key) {}
	
	public StatProfile stats = new StatProfile();
	
	public List<Spell> spells = new List<Spell>();
	
	public List<Item> equipment = new List<Item>();
	
	public Battle.Side currentBattleSide = Battle.Side.Neutral;
}
