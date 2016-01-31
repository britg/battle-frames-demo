
class StartBattleRequestGenerator {
    public static ClientRequest Generate () {
        var clientRequest = new ClientRequest(ClientRequest.ClientReady);
        
        var data = clientRequest.dataNode();
        clientRequest.SetData(data);
        
        return clientRequest;
    }
}