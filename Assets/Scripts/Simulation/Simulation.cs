using UnityEngine;


public class Simulation : MonoBehaviour {
	
	public JSONResourceLoader resourceLoader;

	public Config config;
	public Battle battle;
	
	void Awake () {
		resourceLoader.OnDoneLoading += OnDoneResourceLoading;
		resourceLoader.Load(Application.streamingAssetsPath);
		Debug.Log("Application persistent data path: " + Application.persistentDataPath);
	}
	
	void OnDoneResourceLoading () {
		var spell = new Spell("flash_heal");
		Debug.Log(spell.name);
		var item = new Item("healing_potion");
		Debug.Log(item);
		Debug.Log(item.testField);
		
		
		var other = new Item("stuff");
		Debug.Log(other);
	}
	
}
