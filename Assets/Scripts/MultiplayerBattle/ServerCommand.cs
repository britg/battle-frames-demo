using SimpleJSON;

public class ServerCommand {
    
    public const string SpawnCharacter = "spawn_character";
    public const string EnableClientActions = "enable_client_actions";
    public const string ClientRequestOutcome = "client_request_outcome";
    
    public string commandName;
    JSONClass sourceNode;
    
    public ServerCommand (JSONNode _sourceNode) {
        sourceNode = _sourceNode.AsObject;
        commandName = sourceNode["key"].Value;
    }   
    
    public override string ToString () {
        return commandName;
    }
}