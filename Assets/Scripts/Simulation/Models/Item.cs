using UnityEngine;
using System.Collections;

public class Item : JSONResource {

	public Item () : base() {}
	public Item (string _key) : base(_key) {}
	
	public string testField;
}
