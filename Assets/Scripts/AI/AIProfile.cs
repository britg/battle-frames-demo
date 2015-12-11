using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class AIProfile {

	public List<string> AIList;
	
	public AIProfile (JSONResource jsonResource) {
		Preload(jsonResource);
	}
	
	void Preload (JSONResource jsonResource) {
		AIList = new List<string>();
		
		var sourceNode = jsonResource.sourceNode;
		if (sourceNode == null) {
			return;
		}
		
		var aiNode = sourceNode["ai"];
		if (aiNode == null) {
			return;
		}
		
		foreach (JSONNode node in aiNode.AsArray) {
			AIList.Add(node.Value);			
		}
	}
	
}
