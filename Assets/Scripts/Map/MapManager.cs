using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MapManager : MonoBehaviour {
    
    public string BattleSceneName = "Battle";
    public GameObject mapPiecePrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void OnDemoButtonPressed (string scenarioKey) {
        Simulation.scenarioKey = scenarioKey;
        SceneManager.LoadScene(BattleSceneName);
    }
}
