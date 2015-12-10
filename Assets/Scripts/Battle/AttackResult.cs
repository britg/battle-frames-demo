using UnityEngine;
using System.Collections;

public class AttackResult {
	
	public enum Type {
		Hit,
		Miss,
		CriticalHit,
		Glance,
		Parry
		
	}

	public string statKey;
	public float delta;
	public Type type;
	public BattleFrameController fromController;
	public BattleFrameController targetController;
	
	public override string ToString () {
		return string.Format("Attack Result: {0} -> {1} {2} to {3}",
			fromController.character,
			delta,
			statKey,
			targetController.character);
	}
	
}
