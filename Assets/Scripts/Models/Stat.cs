using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stat : JSONResource {
	
	public const string Health = "health";
	public const string AbilityPoints = "abilityPoints";
	public const string AttackSpeed = "attackSpeed";
	public const string DPS = "dps";
	
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
	
	public float currentValue;
	public float minValue;
	public float maxValue;
	
	public bool computed = false;
}
