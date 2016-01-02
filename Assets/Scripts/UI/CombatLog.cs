using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CombatLog : SimulationBehaviour {
	
	public int historyLength = 100;
	
	public GameObject msgPrefab;
	public Transform scrollContentTransform;
	
	private static CombatLog instance;
	
	public static CombatLog Instance {
		get {
			if (instance == null) {
				instance = GameObject.Find("CombatLog").GetComponent<CombatLog>();
			}
			return instance;
		}
	}
	
	public void OnApplicationQuit () {
		instance = null;
	}
	
	public static void Add (string msg) {
		// var newMsg = Instantiate(Instance.msgPrefab) as GameObject;
		// newMsg.transform.SetParent(Instance.scrollContentTransform, false);
		// var msgText = newMsg.GetComponent<Text>();
		// msgText.text = string.Format("{0:0.00} {1}", Instance.gameTime.CurrentSeconds, msg);
		// TruncateMessages();
		// Instance.Invoke("ScrollToBottom", 0.1f);
	}
	
	static void TruncateMessages () {
		var overage = Instance.scrollContentTransform.childCount - Instance.historyLength;
		
		if (overage <= 0) {
			return;
		}
		
		for (int i = 0; i < overage; i++) {
			var toRemove = Instance.scrollContentTransform.GetChild(i);
			Destroy(toRemove);
		}
	}
	
	void ScrollToBottom () {
		var scrollRect = Instance.GetComponent<ScrollRect>();
		scrollRect.verticalNormalizedPosition = 0;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
