using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.IO;

public class Simulation : MonoBehaviour {

	public Config config;
	public Battle battle;
	
	void Awake () {
		Load();
	}
	
	void Bootstrap () {
		Test();
	}
	
	void Test () {
		var spell = new Spell("flash_heal");
		Debug.Log(spell.name);
		var item = new Item("healing_potion");
		Debug.Log(item.name);
	}
	
	/*
		JSON loading
	
	*/
	public class JSONSourceData : Dictionary<string, Dictionary<string, JSONNode>> {}
	public static JSONSourceData jsonCache = new JSONSourceData();
	
  	const string EXT = ".json";
	
	public bool doneLoading = false;
	
	public void Load () {
		LoadDirectory(Application.streamingAssetsPath);
		StartCoroutine("CheckDoneLoading");
	}
	
	void LoadDirectory (string dir) {

		foreach (string subdir in Directory.GetDirectories(dir)) {
			LoadDirectory(subdir);
		}
	
		foreach (string filename in Directory.GetFiles(dir)) {
			StartCoroutine("LoadFile", filename);
			// LoadFile(filename);
		}
	}

	IEnumerator LoadFile (string filename) {
		var fileInfo = new FileInfo(filename);
		var ext = fileInfo.Extension;
	
		if (ext == EXT) {
			doneLoading = false;
			var url = "file://" + filename;
			WWW www = new WWW(url);
			yield return www;
			//string contents = File.ReadAllText(filename);
			string contents = www.text;
			ParseContents(contents);
			doneLoading = true;
		} else {
		}
	}
	
	void ParseContents (string json) {
		var parsed = JSON.Parse(json);
		var type = parsed["type"].Value;
		var key = parsed["key"].Value;
		if (!Simulation.jsonCache.ContainsKey(type)) {
			Simulation.jsonCache[type] = new Dictionary<string, JSONNode>();
		}
		Simulation.jsonCache[type][key] = parsed;
	}
	
	IEnumerator CheckDoneLoading () {
		while (!doneLoading) {
			yield return new WaitForSeconds(0.1f);	
		}
		
		Bootstrap();
	}
}
