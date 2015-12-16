using UnityEngine;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class JSONSourceData : Dictionary<string, Dictionary<string, JSONNode>> {}

public abstract class JSONResource {
  public const string EXT = ".json";
  public static JSONSourceData jsonCache = new JSONSourceData();
  
  public string type;
  
  public JSONResource () {
    guid = System.Guid.NewGuid().ToString();
    type = this.GetType().ToString();
    
    if (key == null) {
      throw new System.Exception("key must be present");  
    }
    
    
    AssignFields();
  }
  
  public JSONResource (string _key) {
    guid = System.Guid.NewGuid().ToString();
    type = this.GetType().ToString();
    key = _key;
    
    AssignFields();
  }
  
  public JSONResource (string _key, JSONNode __sourceNode) {
    guid = System.Guid.NewGuid().ToString();
    type = this.GetType().ToString();
    key = _key;
    sourceNode = __sourceNode;
  }
    
  JSONNode _sourceNode;
  public JSONNode sourceNode {
    get {
      if (_sourceNode == null) {
        LoadTopLevelSourceNode();
      }
      return _sourceNode; 
    }
    set {
      LoadTopLevelSourceNode();
      AssignFields();
      _sourceNode = value;
      AssignFields();
    }
  }
  
  public string guid;
  public string key;
  public string name;
  
  void LoadTopLevelSourceNode () {
    if (!JSONResource.jsonCache.ContainsKey(type)) {
      _sourceNode = new JSONNode();
    } else {
        var typeDict = JSONResource.jsonCache[type];
        if (typeDict.ContainsKey(key)) {
          _sourceNode = typeDict[key];  
        } else {
          _sourceNode = new JSONNode();
        }  
      }
  }

  protected void AssignFields () {
    
    var fields = this.GetType().GetFields(
      BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
     );

    foreach (var field in fields) {
      var node = sourceNode[field.Name];
      
      // Debug.Log(type + ": " + field.Name + " : " + field.FieldType.ToString() + " : " + node.Value);
      if (node != null && node.Value != "") {
        
        if (field.FieldType == typeof(string)) {
          field.SetValue(this, node.Value);
        }
        
        if (field.FieldType == typeof(float)) {
          field.SetValue(this, node.AsFloat);
        }
        
        if (field.FieldType == typeof(int)) {
          field.SetValue(this, node.AsInt);
        }
        
        if (field.FieldType == typeof(bool)) {
          field.SetValue(this, node.AsBool);
        }
      }
      
      // field.SetValue(this, node);
    }
  }
  
  public override string ToString () {
    return string.Format("{0} : {1} : {2} : {3}", type, guid, key, name);
  }
}
