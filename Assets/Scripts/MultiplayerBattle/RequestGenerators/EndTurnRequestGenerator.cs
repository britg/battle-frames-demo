using UnityEngine;
using System.Collections;
using SimpleJSON;

public class EndTurnRequestGenerator {

	public static ClientRequest Generate () {
        var clientRequest = new ClientRequest(ClientRequest.EndTurn);
        
        var data = clientRequest.dataNode();
        clientRequest.SetData(data);
        
        return clientRequest;
    }
}
