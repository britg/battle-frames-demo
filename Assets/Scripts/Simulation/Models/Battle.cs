using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Battle : JSONResource {
	
	public Battle (string _key) : base(_key) {}
	
	public Character playerCharacter;
	public List<Character> friendlyCharacters;
	public List<Character> enemyCharacters;
}
