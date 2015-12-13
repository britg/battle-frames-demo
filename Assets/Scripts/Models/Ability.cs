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
	
	public enum TargetGrouping {
		None,
		Single,
		AllSide,
		Everyone
	}
	
	public Ability (string _key) : base(_key) {}
	
	public string description;
	public TargetType targetType;
	public TargetGrouping targetGrouping;
	public float castingTime;
	public float cooldown;
	public float radius;
	public float abilityPointCost;
	public List<Proc> procs;
}