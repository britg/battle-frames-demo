using UnityEngine;
using System.Collections.Generic;


public class Simulation : MonoBehaviour {
     
    public static string scenarioKey;
	
	public GameObject battlePrefab;
	
	public JSONResourceLoader resourceLoader;

	public Config config;
	
	void Awake () {
		resourceLoader.OnDoneLoading += OnDoneResourceLoading;
		resourceLoader.Load(Application.streamingAssetsPath);
		Debug.Log("Application persistent data path: " + Application.persistentDataPath);
	}
	
	void OnDoneResourceLoading () {
        LoadScenario();
	}
    
    /*
     * In the final implementation we are loading a scenario from
     * the API based on the tile chosen by the player.
     * For now, we will stub out scenarios
     */
    void LoadScenario () {
        // Debug.Log("Scenario key is " + scenarioKey);
        var scenario = new Scenario(scenarioKey);
        // Debug.Log("Scenario contents are " + scenario.Contents());
        var battleObj = GameObject.Find("Battle");
		if (battleObj == null) {
			battleObj = Instantiate(battlePrefab);
		}
        var battleController = battleObj.GetComponent<BattleController>();
        scenario.Start(battleController);
    }
}
