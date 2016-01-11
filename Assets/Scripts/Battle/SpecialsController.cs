using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpecialsController : BattleBehaviour {
    
    public GameObject specialFramePrefab;

    // TODO convert this to a dictionary, one for each side.    
    public List<Special> specials = new List<Special>();
    Dictionary<string, SpecialFrameController> specialFrames = new Dictionary<string, SpecialFrameController>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void AddSpecial (Special special) {
        specials.Add(special);
        UpdateFrames();
    }
    
    void UpdateFrames () {
        
        var specialKeys = new List<string>();
        
        // create any missing ability frames
        foreach (Special special in specials) {
            specialKeys.Add(special.key);
            
            if (specialFrames.ContainsKey(special.key)) {
                continue;
            }
            
            CreateSpecialFrame(special);
        }
        
        // remove any extra ability frames
        var existingKeys = specialFrames.Keys.ToList();
        foreach (string existingKey in existingKeys) {
            if (!specialKeys.Contains(existingKey)) {
                var extra = specialFrames[existingKey];
                specialFrames.Remove(existingKey);
                Destroy(extra.gameObject);
            }
        }
        
        // Position ability frames
        PositionSpecialFrame();
        
    }
    
    void CreateSpecialFrame (Special special) {
        var specialFrame = Instantiate(specialFramePrefab) as GameObject;
        specialFrame.transform.SetParent(transform, false);
        specialFrame.name = special.name;
        var specialFrameController = specialFrame.GetComponent<SpecialFrameController>();
        specialFrameController.parentSpecialsController = this;
        specialFrameController.special = special;
        specialFrames[special.key] = specialFrameController;
    }
    
    void PositionSpecialFrame () {
        foreach (KeyValuePair<string, SpecialFrameController> kv in specialFrames) {
            var specialFrameController = kv.Value;
            var localPos = specialFrameController.transform.localPosition;
            localPos = Vector3.zero;
            localPos.z = -2;
            specialFrameController.transform.localPosition = localPos;
        }
    }
    
    public void StartSpecial (Special special) {
        StartSpecial(special, null);
    }
    
    public void StartSpecial (Special special, BattleFrameController targetController) {
        Debug.Log("Special controller starting special " + special.name + " with target " + targetController);
    }
}
