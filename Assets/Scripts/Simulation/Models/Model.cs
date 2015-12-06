using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class Model {

	public string guid;
	public string name;
	
	public Model () {
		guid = System.Guid.NewGuid().ToString();
	}
}
