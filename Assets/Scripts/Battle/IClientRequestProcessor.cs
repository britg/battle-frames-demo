using UnityEngine;
using System.Collections;

public interface IClientRequestProcessor {

	void Process (ClientRequest request, ClientRequestHandler.Callback callback);
}
