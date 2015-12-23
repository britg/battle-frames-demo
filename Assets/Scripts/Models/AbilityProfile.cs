using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;
using System.Linq;

public class AbilityProfile : List<Ability> {
    
    string defaultFriendlyAbilityKey;
    
    public AbilityProfile () : base() {
        
    }
    
    public AbilityProfile (JSONResource jsonResource) : base() {
        LoadFromJSON(jsonResource);
    }
    
    public Ability DefaultFriendlyAbility () {
        if (defaultFriendlyAbilityKey == null) {
            return null;
        } else {
            return abilityForKey(defaultFriendlyAbilityKey);
        }
    }
    
    public Ability abilityForKey (string key) {
        var matching = this.Where(a => (a.key == key));
        var ability = matching.FirstOrDefault();
        return ability;
    }
    
    void LoadFromJSON (JSONResource jsonResource) {
        var sourceNode = jsonResource.sourceNode;
        if (sourceNode == null) {
            return;
        }
        
        var abilitiesNode = sourceNode["abilities"];
        if (abilitiesNode == null) {
            return;
        }
        
        foreach (JSONNode abilityKey in abilitiesNode.AsArray) {
            AssignAbility(abilityKey.Value);    
        }
        
        var defaultFriendlyNode = sourceNode["defaultFriendlyAbility"];
        defaultFriendlyAbilityKey = defaultFriendlyNode.Value;
    }
    
    void AssignAbility (string key) {
        Debug.Log("Assigning ability " + key);
        var ability = new Ability(key);
        this.Add(ability);
    }
    
} 