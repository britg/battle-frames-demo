using System.Collections.Generic;
using UnityEngine;

public enum APIState { NotRunning, 
                    Running, 
                    Connected, 
                    Subscribing,
                    Subscribed,
                    Done 
                }

public delegate void Handler();

public class APIStateMachine : MonoBehaviour {
    private readonly object syncLock = new object();
    private readonly Queue<APIState> pendingTransitions = new Queue<APIState>();
    private readonly Dictionary<APIState, Handler> handlers
        = new Dictionary<APIState, Handler>();

    [SerializeField]
    private APIState currentState = APIState.NotRunning;

    public void Run() {
        Transition(APIState.Running);
    }

    public void AddHandler(APIState state, Handler handler) {
        handlers.Add(state, handler);
    }

    public void Transition(APIState state) {
        APIState cur;
        lock(syncLock) {
            cur = currentState;
            pendingTransitions.Enqueue(state);
        }

        Debug.Log("Queued transition from " + cur + " to " + state);
    }

    public void Update() {
        while (pendingTransitions.Count > 0) {
            currentState = pendingTransitions.Dequeue();
            Debug.Log("websocket Transitioned to state " + currentState);

            Handler handler;
            if (handlers.TryGetValue(currentState, out handler)) {
                handler();
            }
        }
    }
}