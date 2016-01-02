using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class Stat : JSONResource {
	
	public const string Health = "health";
	public const string AbilityPoints = "abilityPoints";
	public const string AttackSpeed = "attackSpeed";
	public const string DPS = "dps";
    public const string SpecialChance = "specialChance";
    public const string CritChance = "critChance";
    public const string AbilityRegen = "abilityRegen";
	
	public static Dictionary<string, IStatCalculator> CalculatorMapping
		= new Dictionary<string, IStatCalculator>() {
			{ Health, BaseStatCalculator.Instance } 
		};
		
	public static IStatCalculator Mapping (string statKey) {
		if (CalculatorMapping.ContainsKey(statKey)) {
			return CalculatorMapping[statKey];
		}
		
		return BaseStatCalculator.Instance;
	}
	
	public Stat (string _key) : base(_key) {}
	public Stat (string _key, JSONNode __sourceNode) : base(_key, __sourceNode) {}
	
	public float currentValue;
	public float minValue;
	public float maxValue;
	
	public bool computed = false;
	
	public override string ToString () {
		return string.Format("{0}: {1}", name, currentValue);
	}
}
