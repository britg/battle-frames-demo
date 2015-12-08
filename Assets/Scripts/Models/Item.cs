using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public Item (string _key) : base(_key) {}
	
	public List<Slot> slots = new List<Slot>();
	
}
