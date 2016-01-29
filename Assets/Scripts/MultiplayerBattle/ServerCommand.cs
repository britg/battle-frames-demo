using SimpleJSON;

public class ServerCommand {
    
    public const string SpawnCharacter = "spawn_character";
    public const string EnableClientActions = "enable_client_actions";
    public const string DisableClientActions = "disable_client_actions";
    public const string ClientRequestOutcome = "client_request_outcome";
    public const string SyncBattleState = "sync_battle_state";
    
    public string commandName;
    public string clientRequestId;
    JSONClass sourceNode;
    
    public ServerCommand (JSONNode _sourceNode) {
        sourceNode = _sourceNode.AsObject;
        commandName = sourceNode["key"].Value;
        clientRequestId = sourceNode["request_id"].Value;
    }   
    
    public override string ToString () {
        return commandName;
    }
}