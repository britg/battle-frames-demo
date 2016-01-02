using UnityEngine;
using System.Collections;

public class Notifications {
	
	public const string OnBattleFrameFocusDown = "OnBattleFrameFocusDown";
    public const string OnBattleFrameFocusSelect = "OnBattleFrameFocusSelect";
    public const string OnBattleFrameLostFocus = "OnBattleFrameLostFocus";
    
    
    // Abilities
    
    public const string OnBattleFramePresentedAbilities = "OnBattleFramePresentedAbilities";
    public const string OnBattleFrameHidAbilities = "OnBattleFrameHidAbilities";
    
    public const string OnAbilityResolved = "OnAbilityResolved";
    
    public const string OnAbilityBeginCasting = "OnAbilityBeginCasting";
    
    public const string OnPlayerWin = "OnPlayerWin";
	
	public static class Keys {
		public const string Controller = "controller";
	}
}
