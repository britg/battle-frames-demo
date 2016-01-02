using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class Simulation : MonoBehaviour {
     
    public static string scenarioKey;
    
    public string defaultScenarioKey = "1_wh";
	
	public GameObject battlePrefab;
	
	public JSONResourceLoader resourceLoader;

	public Config config;
	
	void Awake () {
		resourceLoader.OnDoneLoading += OnDoneResourceLoading;
		resourceLoader.Load(Application.streamingAssetsPath);
		Debug.Log("Application persistent data path: " + Application.persistentDataPath);
	}
    
    void Start () {
        NotificationCenter.AddObserver(this, Notifications.OnPlayerWin);
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
        if (scenarioKey == null) {
            scenarioKey = defaultScenarioKey;
        }
        var scenario = new Scenario(scenarioKey);
        // Debug.Log("Scenario contents are " + scenario.Contents());
        var battleObj = GameObject.Find("Battle");
		if (battleObj == null) {
			battleObj = Instantiate(battlePrefab);
		}
        var battleController = battleObj.GetComponent<BattleController>();
        scenario.Start(battleController);
    }
    
    void OnPlayerWin () {
        Invoke("End", 3f);
    }
    
    void End () {
        SceneManager.LoadScene("Map");
    }
}
