using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleAnimationController : BattleBehaviour {
    
    public GameObject genericPrefab;
    public GameObject tauntPrefab;
    
    Dictionary<string, GameObject> mapping;
    
    void Start () {
        CreateMapping();
    }
    
    void CreateMapping () {
        mapping = new Dictionary<string, GameObject>() {
            { "taunt", genericPrefab }
        };
    }
    
    GameObject Mapping (string abilityKey) {
        if (mapping.ContainsKey(abilityKey)) {
            return mapping[abilityKey];
        }
        return genericPrefab;
    }

    public void QueueAbilityAnimation (AbilityContext abilityContext) {
        var key = abilityContext.ability.key;
        var animPrefab = Mapping(key);
        
        var animObj = (GameObject)Instantiate(animPrefab);
        var animController = animObj.GetComponent<IAnimationController>();
        animController.abilityContext = abilityContext;
        animController.Execute();
    }
}
