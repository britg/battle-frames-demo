﻿using UnityEngine;
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
		NotificationCenter.AddObserver(this, Notifications.OnBattleFrameSelected);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnBattleFrameSelected (Notification n) {
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
		Dictionary<string, string> data = new Dictionary<string, string>();
		foreach (KeyValuePair<BattleFrameController, float> kv in aggroProfile) {
			data.Add(kv.Key.character.name, kv.Value.ToString());
		}
		
		aggroTable.rowData = data;
	}
}