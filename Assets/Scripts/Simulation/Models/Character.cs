using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Character : Model {
	
	public Character (string _key) : base("Character", _key) {}
	
	public int level;
	public int experiencePoints;
	public int nextLevelExperiencePoints;

	public int currentHealth;
	public int maxHealth;
	
	public int currentMana;
	public int maxMana;
	
	public List<Spell> spells = new List<Spell>();
	
	
	
}
