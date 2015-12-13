using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Battle : JSONResource {
	
	
	public enum Side {
		Adventurers,
		Mobs,
		Neutral
	}
		
	public Battle (string _key) : base(_key) {}
	
	public Dictionary<Side, List<Character>> sides = new Dictionary<Side, List<Character>>();
	public List<Character> allCharacters {
		get {
			var _all = new List<Character>();
			foreach (KeyValuePair<Side, List<Character>> kv in sides) {
				_all.AddRange(kv.Value);
			}
			return _all;
		}
	}
	
	public List<Character> aiControlledCharacters {
		get {
			var aiControlled = new List<Character>();
			foreach (Character c in allCharacters) {
				if (c.aiControlled) {
					aiControlled.Add(c);
				}
			}
			return aiControlled;
		}
	}
	
	public void AddCharacterToSide (Character character, Side side) {
		if (!sides.ContainsKey(side)) {
			sides[side] = new List<Character>();
		}
		character.currentBattleSide = side;
		sides[side].Add(character);
	}
}
