using SimpleJSON;
public class ClientRequest {
    public const string EndTurn = "end_turn";
    public const string ClientReady = "client_ready";
    public const string CurrentState = "current_state";
    
    public string requestName;
    JSONClass sourceNode;
    
    public ClientRequest (JSONNode _sourceNode) {
        sourceNode = _sourceNode.AsObject;
        requestName = sourceNode["key"].Value;
    }
    
    public override string ToString () {
        return requestName;
    }
}