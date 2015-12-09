using UnityEngine;

public class GameTime {
  
  [System.Serializable]
  public class Config {
    public float currentTimeMultiplier;
    public int startSeconds;
    public int dawnStartHour;
    public int nightStartHour;
  }
  
  public Config config;

  public float CurrentSeconds { get; set; }
  
  int DAY_SECONDS = 60 * 60 * 24;
  int HOUR_SECONDS = 60 * 60;
  int MINUTE_SECONDS = 60;
  
  float currentMinuteProgress = 0f;
  float currentHourProgress = 0f;
  
  public delegate void ChangeEventHandler ();
  
  // public event ChangeEventHandler SecondChange;
  public event ChangeEventHandler MinuteChange;
  public event ChangeEventHandler HourChange;
  public event ChangeEventHandler DawnStart;
  public event ChangeEventHandler NightStart;
  
  public static Config defaultConfig {
    get {
      var _defaultConfig = new Config();
      _defaultConfig.currentTimeMultiplier = 60f;
      _defaultConfig.startSeconds = 7 * 3600;
      _defaultConfig.dawnStartHour = 7;
      _defaultConfig.nightStartHour = 20;
      return _defaultConfig;
    }
  }
  
  public int Day {
    get {
      return Mathf.FloorToInt(CurrentSeconds / DAY_SECONDS);
    }
  }
  
  public int Hour {
    get {
      return Mathf.FloorToInt(CurrentSeconds % DAY_SECONDS / HOUR_SECONDS);
    }
  }
  
  public int Minute {
    get {
      return Mathf.FloorToInt(CurrentSeconds % HOUR_SECONDS / MINUTE_SECONDS);
    }
  }
  
  public int Seconds {
    get {
      return Mathf.FloorToInt(CurrentSeconds % MINUTE_SECONDS);
    }
  }
  
  public int SecondsSinceDayStart {
    get {
      return Mathf.FloorToInt(CurrentSeconds % DAY_SECONDS) - config.dawnStartHour * HOUR_SECONDS; 
    }
  }
  
  public GameTime () {
    config = GameTime.defaultConfig;
    Setup();
  }
  public GameTime (Config _config) {
    config = _config;
    Setup();
  }
  
  void Setup () {
    HourChange += UpdateDayNight;
    AddTime(config.startSeconds);
  }
  
  public float ConvertDeltaTime (float deltaTime) {
    var converted = deltaTime * config.currentTimeMultiplier;
    return converted;
  }
  
  public void AddRealTime (float deltaTime) {
    AddTime(ConvertDeltaTime(deltaTime));
  }
  
  public void AddTime (int amount) {
    AddTime((float)amount);
  }
  
  public void AddTime (float amount) {
    CurrentSeconds += amount;
    UpdateMinuteProgress(amount);
    UpdateHourProgress(amount);
  }
  
  void UpdateMinuteProgress (float amount) {
    currentMinuteProgress += amount;
    if (currentMinuteProgress >= MINUTE_SECONDS) {
      if (MinuteChange != null) {
        MinuteChange();
      }
      currentMinuteProgress = 0f;
    }
  }
  
  void UpdateHourProgress (float amount) {
    currentHourProgress += amount;
    if (currentHourProgress >= HOUR_SECONDS) {
      if (HourChange != null) {
        HourChange();
      }
      currentHourProgress = 0f;
    }
  }
  
  void UpdateDayNight () {
    if (Hour == config.dawnStartHour) {
      if (DawnStart != null) {
        DawnStart();
      }
    }
    
    if (Hour == config.nightStartHour) {
      if (NightStart != null) {
        NightStart();
      }
    }
  }
  
  public bool isDayTime {
    get {
      return Hour >= config.dawnStartHour && Hour < config.nightStartHour;
    }
  }
  
  public bool isNightTime {
    get {
      return !isDayTime;
    }
  }
  
  public int hoursInDay {
    get {
      return config.nightStartHour - config.dawnStartHour;
    }
  }
  
  public override string ToString () {
    return string.Format("Day {0}, {1:D2}:{2:D2}:{3:D2}", Day, Hour, Minute, Seconds);
  }
  
}