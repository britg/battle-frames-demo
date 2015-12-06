using UnityEngine;
using System.Collections;

public class SimulationBehaviour : MonoBehaviour {

	Simulation _simulation;
	public Simulation simulation {
		get {
			if (_simulation == null) {
				_simulation = GameObject.Find("Simulation").GetComponent<Simulation>();
			}
			return _simulation;
		}
	}
}
