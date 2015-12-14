using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TableController : MonoBehaviour {
	
	public string label = "[Label]";
	
	Dictionary<string, string> _rowData;
	public Dictionary<string, string> rowData {
		get {
			return _rowData;
		}
		set {
			_rowData = value;
			UpdateRows();
		}
	}
	
	public Text labelElement;
	public GameObject tableRowPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void ClearRows () {
		foreach (Transform t in transform) {
			if (t.gameObject.name == "TableRow") {
				Destroy(t.gameObject);
			}
		}
	}
	
	void UpdateRows () {
		ClearRows();
		foreach (KeyValuePair<string, string> kv in rowData) {
			var rowObj = Instantiate(tableRowPrefab);
			rowObj.name = "TableRow";
			rowObj.transform.SetParent(transform, false);
			var controller = rowObj.GetComponent<TableRowController>();
			controller.key = kv.Key;
			controller.val = kv.Value;
		}		
	}
}
