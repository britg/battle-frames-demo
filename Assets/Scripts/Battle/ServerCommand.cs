using SimpleJSON;

public class ServerCommand {
    
    public const string SpawnShip = "spawn_ship";
    public const string EnableClientActions = "enable_client_actions";
    public const string DisableClientActions = "disable_client_actions";
    public const string ClientRequestOutcome = "client_request_outcome";
    public const string SyncBattleState = "sync_battle_state";
    public const string NotfyTurn = "notify_turn";
    
    public ServerCommandHandler.Callback onProcessedCallback;
    
    public string commandName;
    public string clientRequestId;
    public JSONNode data;
    JSONClass sourceNode;
    
    public ServerCommand (JSONNode _sourceNode) {
        sourceNode = _sourceNode.AsObject;
        commandName = sourceNode["key"].Value;
        clientRequestId = sourceNode["request_id"].Value;
        data = sourceNode["data"];
    }   
    
    public override string ToString () {
        return commandName;
    }
    
    public bool requestSuccessful {
        get {
            return data.Value == "success";
        }
    }
}