using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//  Timer data class
//  User can track the length of their current boss trip
public static class Timer
{
    public static float timeElapsed { get; private set; }
    public static bool IsRunning { get; private set; }

    public static void Start()
    {
        IsRunning = true;
    }

    public static void Stop()
    {
        IsRunning = false;
    }

    public static void Reset()
    {
        IsRunning = false;
        timeElapsed = 0f;
        EventManager.Instance.TimerUpdated();
    }

    public static void UpdateTime()
    {
        timeElapsed += Time.deltaTime;
        EventManager.Instance.TimerUpdated();
    }
}
