using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class Item : JSONResource {
	
	public enum Slot {
		Inventory,
		MainHand,
		OffHand,
		Head,
		Neck,
		Shoulders,
		Torso,
		Wrists,
		Hands,
		Waist,
		RightFinger,
		LeftFinger,
		Legs,
		Feet
	}

	public Item (string _key) : base(_key) {
        specialProfile = new SpecialProfile(this);
        LoadStatModifiersFromJSON();
        LoadProcs();
    }
	
	public List<Slot> slots = new List<Slot>();
    public List<CharacterClass> characterClasses = new List<CharacterClass>();
    
    // Empty class restriction list means no restriction
    public bool classRestricted {
        get {
            return characterClasses.Count > 0;
        }
    }
    
    public SpecialProfile specialProfile;
    
    public Dictionary<string, float> statModifiers;
    
    public int levelRequirement;
    
    public bool consumable;
    public List<Proc> procs;
    
    void LoadStatModifiersFromJSON () {
        
    }
    
    void LoadProcs () {
		procs = new List<Proc>();
		
		var procsNode = sourceNode["procs"];
		if (procsNode == null) {
			return;
		}
		
		foreach (KeyValuePair<string, JSONNode> kv in procsNode.AsObject) {
			var proc = new Proc(kv.Key, kv.Value);
			procs.Add(proc);
		}
		
	}
	
}
