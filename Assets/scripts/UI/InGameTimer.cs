using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameTimer : MonoBehaviour {

    public static InGameTimer instance;
    string minutes;
    string seconds;
    float timerCounter;
    bool timerGo;
    string timetoShow;

    private bool timerStarted;

    private void Start()
    {
        instance = instance ?? this;
        ResetValues();
    }
    public void ResetValues()
    {
        timetoShow = string.Empty;
        timerStarted = timerGo = false;
    }
    private void Update()
    {
        if (timerGo)
        {
            timerCounter += MainCount.instance.deltaTime;
            minutes = ((int)(timerCounter / 60)).ToString();
            seconds = (timerCounter % 60).ToString("f1");
        }
        else
        {
            if (!string.IsNullOrEmpty(seconds))
            {
                timetoShow = minutes + ":" + seconds;
            }
            else
            {
                minutes = "0";
                seconds = "00";
            }
        }
        InGameWiever.instance.SetTimerText(minutes + ":" + seconds);
    }

    public void StartTimer()
    {
        if (timerStarted)
        {
            return;
        }
        timerGo = timerStarted= true;
        timerCounter = 0.001f;
    }
    public void StopTimer()
    {
        timerGo = false;
    }

    public void ResetTimer()
    {
        minutes = string.Empty;
        seconds = string.Empty;
    }
}
