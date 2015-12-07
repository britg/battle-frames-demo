using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Battle : Model {
	
	public Battle (string _key) : base("Battle", _key) {}
	
	public Character playerCharacter;
	public List<Character> friendlyCharacters;
	public List<Character> enemyCharacters;
}
