using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InfoPanelController : SimulationBehaviour {
	
	BattleFrameController _controller;
	public BattleFrameController controller {
		get {
			return _controller;
		}
		set {
			_controller = value;
			UpdateUI();
		}
	}	
	public Text nameText;
	public Text isAIControlled;
	
	public TableController aggroTable;
	
	void Awake () {
		NotificationCenter.AddObserver(this, Notifications.OnBattleFrameFocusDown);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnBattleFrameFocusDown (Notification n) {
		controller = n.data[Notifications.Keys.Controller] as BattleFrameController;
	}
	
	void UpdateUI () {
		nameText.text = controller.character.name;
		isAIControlled.text = string.Format("AI controlled? {0}", controller.aiControlled);
		UpdateAggroTable();
	}
	
	void UpdateAggroTable () {
		
		if (!controller.aiControlled) {
			aggroTable.gameObject.SetActive(false);
			return;
		} else {
			aggroTable.gameObject.SetActive(true);
		}
		
		var aggroProfile = controller.aggroProfile;
		var data = new Dictionary<RowDataKeyObject, string>();
		foreach (KeyValuePair<BattleFrameController, float> kv in aggroProfile) {
            var keyObj = new RowDataKeyObject(kv.Key.character.name);
			data.Add(keyObj, kv.Value.ToString());
		}
		
		aggroTable.rowData = data;
	}
}
