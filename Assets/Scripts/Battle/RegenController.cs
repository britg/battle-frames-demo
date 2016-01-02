using UnityEngine;
using System.Collections;

public class RegenController : BattleFrameBehaviour {

	void Start () {
        gameTime.SecondChange += OnSecondChange;
    }
    
    void OnSecondChange () {
        if (character.regensAbilityPoints) {
            Regen();    
        }
    }
    
    void Regen () {
        var abilityPointRegenAmount = character.stats.CurrentValue(Stat.AbilityRegen);
        battleFrameController.ChangeStat(Stat.AbilityPoints, abilityPointRegenAmount, false);
    }
    
    void OnDestroy () {
        gameTime.SecondChange -= OnSecondChange;
    }
}
