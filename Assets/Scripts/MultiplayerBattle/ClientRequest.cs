using SimpleJSON;
using UnityEngine;

public class ClientRequest {
    
    
    public const string EndTurn = "end_turn";
    public const string ClientReady = "client_ready";
    public const string CurrentState = "current_state";

    public string id = System.Guid.NewGuid().ToString();    
    public string requestName;
    JSONClass sourceNode;
    
    public string forApi;

    public ClientRequest () {
    }
    
    public ClientRequest (string name) {
        requestName = name;
    }
    
    public ClientRequest (JSONNode _sourceNode) {
        sourceNode = _sourceNode.AsObject;
        requestName = sourceNode["key"].Value;
    }
    
    public JSONClass dataNode () {
        var data = JSON.Parse("{}");
        data.Add("request", requestName);
        data.Add("id", id);
        
        return data.AsObject;
    }
    
    public JSONClass requestNodeWithData (JSONClass data) {
        var node = JSON.Parse("{}");
        node.Add("command", "message");
        var channel = JSON.Parse("{}");
        channel.Add("channel", "BattleChannel");
        channel.Add("battle_id", PlayerPrefs.GetString("battleId"));
        node.Add("identifier", channel.AsObject.ToString());
        node.Add("data", data.AsObject.ToString());
        return node.AsObject;
    }
    
    public void SetData (JSONClass data) {
        var node = requestNodeWithData(data);
        sourceNode = node;
    }
    
    public string ForApi() {
        if (!string.IsNullOrEmpty(forApi)) {
            return forApi;    
        }
        return sourceNode.ToString();
    }
    
    public override string ToString () {
        return string.Format("{0} {1}", requestName, id);
    }
}