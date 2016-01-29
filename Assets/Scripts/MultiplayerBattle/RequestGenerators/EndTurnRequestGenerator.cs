using UnityEngine;
using System.Collections;
using SimpleJSON;

public class EndTurnRequestGenerator {

	public static ClientRequest Generate () {
        var clientRequest = new ClientRequest();
        
        var node = BattleApiController.requestNode();
        
        var data = JSON.Parse("{}");
        data.Add("request", ClientRequest.EndTurn);
        node.Add("data", data.AsObject.ToString());
        
        clientRequest.forApi = node.ToString();
        
        return clientRequest;
    }
}
