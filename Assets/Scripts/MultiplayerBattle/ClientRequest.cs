using SimpleJSON;
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
    
    public string ForApi() {
        return forApi;
    }
    
    public override string ToString () {
        return requestName;
    }
}