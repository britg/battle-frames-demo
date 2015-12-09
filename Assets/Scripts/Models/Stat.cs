using UnityEngine;
using System.Collections;

public class Stat : JSONResource {
	
	public const string Health = "health";
	public const string AbilityPoints = "abilityPoints";
	
	public Stat (string _key) : base(_key) {}
	
	public float currentValue;
	public float minValue;
	public float maxValue;
	
	public bool shouldRound;
}
