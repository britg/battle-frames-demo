using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

[System.Serializable]
public abstract class Model {
	
	public string type;
	public string sourceKey;
	
	public Model () {
		guid = System.Guid.NewGuid().ToString();
	}
	
	public Model (string _type, string _key) {
		guid = System.Guid.NewGuid().ToString();
		type = _type;
		sourceKey = _key;
	}
	
	public string guid;
		
	JSONNode _sourceNode;
	public JSONNode sourceNode {
		get {
			if (_sourceNode == null) {
				_sourceNode = Simulation.jsonCache[type][sourceKey];
			}
			return _sourceNode;
		}
	}
	public string key { get { return sourceNode["key"].Value; } }
	public string name { get { return sourceNode["name"].Value; } }
}
