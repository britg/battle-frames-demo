using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class EquipmentProfile : Dictionary<string, Item> {
    
    public EquipmentProfile () : base() {
        
    }
    
    public EquipmentProfile (JSONResource jsonResource) {
        LoadFromJSON(jsonResource);
    }

    public Dictionary<float, Special> SpecialChances () {
        var chances = new Dictionary<float, Special>();
        
        return chances;
    }
    
    void LoadFromJSON (JSONResource jsonResource) {
        
    }
}
