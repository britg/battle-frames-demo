using UnityEngine;
using System.Collections;

public interface IStatCalculator {

	// Use this for initialization
	int CalculatedStat (string statKey, StatProfile profile);
	int CalculatedStatMax (string statKey, StatProfile profile);
	int CalculatedStatMin (string statKey, StatProfile profile);
}
