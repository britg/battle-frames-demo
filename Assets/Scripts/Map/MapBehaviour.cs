using UnityEngine;
using System.Collections;

public abstract class MapBehaviour : MonoBehaviour {

	MapManager _mapManager;
    public MapManager mapManager {
        get {
            if (_mapManager == null) {
                _mapManager = GameObject.Find("MapManager").GetComponent<MapManager>();
            }
            return _mapManager;
        }
    }
}
