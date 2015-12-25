using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class EquipmentProfile : Dictionary<Item.Slot, Item> {
    
    SpecialProfile completeSpecialProfile;
    
    public EquipmentProfile () : base() {
        
    }
    
    public EquipmentProfile (JSONResource jsonResource) {
        LoadFromJSON(jsonResource);
    }

    public SpecialProfile SpecialChances () {
        return completeSpecialProfile;
    }
    
    public void RecalculateSpecialProfile () {
        completeSpecialProfile = new SpecialProfile();
        foreach (KeyValuePair<Item.Slot, Item> kv in this) {
            var itemSpecialProfile = kv.Value.specialProfile;
            completeSpecialProfile.Add(itemSpecialProfile);
        }
    }
    
    public void ReplaceSlot (Item.Slot slot, Item item) {
        this[slot] = item;
        RecalculateSpecialProfile();
    }
    
    void LoadFromJSON (JSONResource jsonResource) {
        
    }
}
