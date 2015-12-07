using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.IO;

public class JSONResourceLoader : MonoBehaviour {
	public bool doneLoading = false;
	
	public delegate void DoneEventHandler ();
	public event DoneEventHandler OnDoneLoading;
	
	public void Load (string dir) {
		LoadDirectory(dir);
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
	
		if (ext == JSONResource.EXT) {
			//string contents = File.ReadAllText(filename);
			
			doneLoading = false;
			var url = "file://" + filename;
			WWW www = new WWW(url);
			yield return www;
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
		if (!JSONResource.jsonCache.ContainsKey(type)) {
			JSONResource.jsonCache[type] = new Dictionary<string, JSONNode>();
		}
		JSONResource.jsonCache[type][key] = parsed;
	}
	
	IEnumerator CheckDoneLoading () {
		while (!doneLoading) {
			yield return new WaitForSeconds(0.1f);	
		}
		
		if (OnDoneLoading != null) {
			OnDoneLoading();	
		}
	}
}