using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.Linq;

public class Proc : JSONResource {

	public Proc (string _key) : base(_key) {}
	public Proc (string _key, JSONNode __sourceNode) : base(_key, __sourceNode) {
		LoadBaseStatChanges();
	}
	
	public int occurances;
	public float occuranceRate;
	
	public List<StatChange> baseStatChanges;
	
	void LoadBaseStatChanges () {
		baseStatChanges = new List<StatChange>();
		var baseStatChangesNode = sourceNode["baseStatChanges"];
		if (baseStatChangesNode == null) {
			return;
		}
		
		foreach (KeyValuePair<string, JSONNode> kv in baseStatChangesNode.AsObject) {
			var stat = new StatChange(kv.Key, kv.Value);
			baseStatChanges.Add(stat);
		}
	}
	
	public StatChange GenerateStatChange (string statKey) {
		var baseStatChange = baseStatChanges.Where(s => (s.key == statKey)).FirstOrDefault();
		return new StatChange(statKey, baseStatChange.sourceNode);
	}
	
}
