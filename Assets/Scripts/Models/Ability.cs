using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class Ability : JSONResource {
	
	public enum TargetType {
		Self,
		AnyCharacter,
		EnemyCharacter,
		FriendlyCharacter,
		AnySide,
		EnemySide,
		FriendlySide
	} 
	
	public Ability (string _key) : base(_key) {
		LoadProcs();
	}
	
	public string description;
	public TargetType targetType;
	public bool requiresTargetSelection;
	public float castingTime;
	public float cooldown;
	public float radius;
	public float abilityPointCost;
	public List<Proc> procs;
	
	void LoadProcs () {
		procs = new List<Proc>();
		
		var procsNode = sourceNode["procs"];
		if (procsNode == null) {
			return;
		}
		
		foreach (KeyValuePair<string, JSONNode> kv in procsNode.AsObject) {
			var proc = new Proc(kv.Key, kv.Value);
			procs.Add(proc);
		}
		
	}
}