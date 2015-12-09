using UnityEngine;


public class Simulation : MonoBehaviour {
	
	public GameObject battlePrefab;
	
	public JSONResourceLoader resourceLoader;

	public Config config;
	
	void Awake () {
		resourceLoader.OnDoneLoading += OnDoneResourceLoading;
		resourceLoader.Load(Application.streamingAssetsPath);
		Debug.Log("Application persistent data path: " + Application.persistentDataPath);
	}
	
	void OnDoneResourceLoading () {
		Demo();
	}
	
	void Demo () {
		
		var rootMonster = new Character("root_monster");
		var warrior = new Character("warrior");
		
		var battle = new Battle("demo");
		
		battle.friendlyCharacters.Add(warrior);
		battle.enemyCharacters.Add(rootMonster);
		
		var battleObj = GameObject.Find("Battle");
		if (battleObj == null) {
			battleObj = Instantiate(battlePrefab);
		}
		
		
		var battleController = battleObj.GetComponent<BattleController>();
		battleController.battle = battle;		
		
		battleController.Setup();
	}
	
}
