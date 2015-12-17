using UnityEngine;
using System.Collections;
using SimpleJSON;

public class StatChange : JSONResource {

	public StatChange (string _key, JSONNode __sourceNode) : base(_key, __sourceNode) {}
	
	bool valueCalculated = false;
	float _finalValue;
	public float finalValue {
		get {
			if (!valueCalculated) {
				_finalValue = Random.Range(minValue, maxValue);
				valueCalculated = true;
			}
			return _finalValue;
		}
	}
	
	public float minValue;
	public float maxValue;
	
	public override string ToString () {
		return string.Format("{0} by {1} ({2} - {3})", key, finalValue, minValue, maxValue);
	}
}
