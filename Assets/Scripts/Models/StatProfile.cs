using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleJSON;

public class StatProfile {
	
	public StatProfile () {
	}
	
	public StatProfile (JSONResource jsonResource) {
		Preload(jsonResource);
	}
	
	public List<Stat> stats = new List<Stat>();
	
	public Stat ModifiedStat (string statKey) {
		return Stat.Mapping(statKey).ModifiedStat(statKey, this);
	}
	
	public float CurrentValue (string statKey) {
		return Stat.Mapping(statKey).CalculatedStat(statKey, this);
	}
	
	public int MaxValue (string statKey) {
		return Stat.Mapping(statKey).CalculatedStatMax(statKey, this);
	}
	
	public float MaxRawValue (string statKey) {
		return statForKey(statKey).maxValue;
	}
	
	public int MinValue (string statKey) {
		return Stat.Mapping(statKey).CalculatedStatMin(statKey, this);
	}
	
	public float MinRawValue (string statKey) {
		return statForKey(statKey).minValue;
	}
	
	public Stat statForKey (string statKey) {
		var matching = stats.Where(s => (s.key == statKey));
		var stat = matching.FirstOrDefault();
		return stat;
	}
	
	public void ChangeStat (string statKey, float delta) {
		var stat = statForKey(statKey);
		var modified = ModifiedStat(statKey);
		float allowableDelta = Mathf.Clamp(modified.currentValue + delta, modified.minValue, modified.maxValue) - modified.currentValue;
		
		stat.currentValue += allowableDelta;
	}
	
	
	void Preload (JSONResource jsonResource) {
		stats = new List<Stat>();
		
		var sourceNode = jsonResource.sourceNode;
		if (sourceNode == null) {
			return;
		}
		
		var statsNode = sourceNode["stats"];
		if (statsNode == null) {
            Debug.Log("stats node is null");
			return;
		}
		
		var statFields = typeof(Stat).GetFields(
			BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
		);
		
		foreach (KeyValuePair<string, JSONNode> kv in statsNode.AsObject) {
			var key = kv.Key;
			var statNode = kv.Value;
			var stat = statForKey(key);
			
			if (stat == null) {
				stat = new Stat(key);
				stats.Add(stat);
			}
			
			foreach (var statField in statFields) {
				// Debug.Log("Setting stat field " + statField.Name + " to " + statNode[statField.Name].AsFloat);
				
				if (statField.FieldType == typeof(float)) {
					statField.SetValue(stat, statNode[statField.Name].AsFloat);	
				}
				else if (statField.FieldType == typeof(bool)) {
					statField.SetValue(stat, statNode[statField.Name].AsBool);
				}
				
			}
		}
	}
}