using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//  Timer data class
//  User can track the length of their current boss trip
public static class Timer
{
    public static TimeSpan time { get; private set; }
    public static bool IsRunning { get; private set; }

    private static float startTime, previousTime;

    public static void Start()
    {
        IsRunning = true;
        previousTime = Time.time;
    }

    public static void Stop()
    {
        IsRunning = false;
    }

    public static void Reset()
    {
        IsRunning = false;
        time = TimeSpan.Zero;
        EventManager.Instance.TimerUpdated();
    }

    public static void UpdateTime()
    {
        float timeElapsed = Time.time - previousTime;

        time += TimeSpan.FromSeconds(timeElapsed);

        EventManager.Instance.TimerUpdated();

        previousTime = Time.time;
    }
}
