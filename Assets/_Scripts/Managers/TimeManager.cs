using System;
using System.Collections;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    public static Action OnTimeToChangeReality;
    public static Action onSecondChange;

    public static int minute { get; private set; }
    public static int second { get; private set; }

    private int timeToChangeReality = 10;
    private int secondToRealTime = 1;
    public float timer;
    private  void OnEnable() => RealityManager.onChangeReality += RestartTimer;
    private void OnDisable() => RealityManager.onChangeReality -= RestartTimer;
    void Start() 
    {
        minute = timeToChangeReality;
        second = 0;
        timer = secondToRealTime;
    }

   void RestartTimer()
    {
        minute = timeToChangeReality;
        second = 0;
        timer = secondToRealTime;
        onSecondChange?.Invoke();
    }

    void Update()
    {
       timer -= Time.deltaTime;
        if(timer <= 0)
        {
            second++;
            onSecondChange?.Invoke();
            if(second >= minute)
            {
                minute++;
                second = 0;
                OnTimeToChangeReality?.Invoke();
            }
            timer = secondToRealTime;
        }
    }
}
