using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;
using System.Linq;

public class SpecialProfile : Dictionary<Special, float> {
    
    public SpecialProfile () : base() {
        
    }
    
    public SpecialProfile (JSONResource jsonResource) : base() {
        LoadFromJSON(jsonResource);
    }
    
    public void Add (SpecialProfile otherProfile) {
        otherProfile.ToList().ForEach(x => {
            this[x.Key] = x.Value;
        });
    }
    
    public Special Roll (List<Special> excluding) {
        var minusExclusions = new Dictionary<Special, float>(this);
        excluding.ForEach(s => {
           minusExclusions.Remove(s); 
        });
        return tpd.RollMap<Special>(minusExclusions);
    }
    
    void LoadFromJSON (JSONResource jsonResource) {
        Debug.Log("Loading special profile from json");
        var sourceNode = jsonResource.sourceNode;
        var specialProfileNode = sourceNode["specialProfile"];
        if (specialProfileNode == null) {
            return;
        }       
        
        foreach (KeyValuePair<string, JSONNode> kv in specialProfileNode.AsObject) {
            var specialKey = kv.Key;
            var chance = kv.Value.AsFloat;
            var special = new Special(specialKey);
            this[special] = chance;
        }
    }
    
    
    
}