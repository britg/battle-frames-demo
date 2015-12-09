using UnityEngine;
using System.Collections;

public class BaseStatCalculator : IStatCalculator {
	
	static BaseStatCalculator instance;
	public static BaseStatCalculator Instance {
		get {
			if (instance == null) {
				instance = new BaseStatCalculator();
			}
			return instance;
		}
	}

	public int CalculatedStat (string statKey, StatProfile profile) {
		return RoundStat(statKey, profile);
	}
	
	public int CalculatedStatMax (string statKey, StatProfile profile) {
		return RoundStatMax(statKey, profile);
	}
	
	public int CalculatedStatMin (string statKey, StatProfile profile) {
		return RoundStatMin(statKey, profile);
	}
	
	protected int RoundStat (string statKey, StatProfile profile) {
		return Mathf.RoundToInt(ModifiedStat(statKey, profile).currentValue);
	}
	
	protected int RoundStatMax (string statKey, StatProfile profile) {
		return Mathf.RoundToInt(ModifiedStat(statKey, profile).maxValue);
	}
	
	protected int RoundStatMin (string statKey, StatProfile profile) {
		return Mathf.RoundToInt(ModifiedStat(statKey, profile).minValue);
	}
	
	protected Stat ModifiedStat (string statKey, StatProfile profile) {
		// TODO: add in modifiers
		return profile.statForKey(statKey);
	}
}
