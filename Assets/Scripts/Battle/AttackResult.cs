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

	public int damageAmount;
	public Type type;
	public BattleFrameController targetController;
	
}
