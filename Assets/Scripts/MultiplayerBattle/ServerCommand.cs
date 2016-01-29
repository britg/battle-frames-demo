using SimpleJSON;

public class ServerCommand {
    
    public const string  SpawnCharacter = "spawn_character";
    
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