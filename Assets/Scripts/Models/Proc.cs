using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class Proc : JSONResource {

	public Proc (string _key) : base(_key) {}
	public Proc (string _key, JSONNode __sourceNode) : base(_key, __sourceNode) {
		LoadBaseStatChanges();
	}
	
	public int occurances;
	public float occuranceRate;
	
	public List<Stat> baseStatChanges;
	
	void LoadBaseStatChanges () {
		baseStatChanges = new List<Stat>();
		var baseStatChangesNode = sourceNode["baseStatChanges"];
		if (baseStatChangesNode == null) {
			return;
		}
		
		foreach (KeyValuePair<string, JSONNode> kv in baseStatChangesNode.AsObject) {
			var stat = new Stat(kv.Key, kv.Value);
			baseStatChanges.Add(stat);
		}
	}
	
}
