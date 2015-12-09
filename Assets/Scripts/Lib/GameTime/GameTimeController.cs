using UnityEngine;

public class GameTimeController : MonoBehaviour {
	
	public bool paused = true;
	
	public GameTime.Config gameTimeConfig;
	
	public GameTime gameTime;
	
	public float gameDeltaTime = 0f;
	
	void Awake () {
		gameTime = new GameTime(gameTimeConfig);
		Invoke("Unpause", 1f);
	}
	
	void Start () {
			
	}
	
	void Update () {
		if (!paused) {
			gameDeltaTime = gameTime.ConvertDeltaTime(Time.deltaTime);
			gameTime.AddTime(gameDeltaTime);	
		}
	}
	
	void Unpause () {
		paused = false;
	}	
	
}