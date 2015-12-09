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
  }
  
  public JSONResource (string _key) {
    guid = System.Guid.NewGuid().ToString();
    type = this.GetType().ToString();
    key = _key;
    
    AssignFields();
  }
    
  JSONNode _sourceNode;
  public JSONNode sourceNode {
    get {
      if (_sourceNode == null) {
        
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
      return _sourceNode;
    }
  }
  
  public string guid;
  public string key;
  public string name;

  void AssignFields () {
    
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
      }
      
      // field.SetValue(this, node);
    }
  }
  
  public override string ToString () {
    return string.Format("{0} : {1} : {2} : {3}", type, guid, key, name);
  }
}
