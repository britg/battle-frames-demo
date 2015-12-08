using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Battle : JSONResource {
	
	public enum Side {
		Friend,
		Enemy,
		Neutral
	}
		
	public Battle (string _key) : base(_key) {}
	
	public List<Character> friendlyCharacters = new List<Character>();
	public List<Character> enemyCharacters = new List<Character>();
}
