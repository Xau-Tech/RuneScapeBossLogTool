using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Static timer class for tracking time of pvm trips
/// </summary>
public class Timer
{
    //  Properties & fields
    public float TimeElapsed { get; private set; }
    public bool IsRunning { get; private set; }

    public Timer()
    {
        TimeElapsed = 0f;
        IsRunning = false;
    }

    //  Methods

    public void Start()
    {
        IsRunning = true;
    }

    public void Stop()
    {
        IsRunning = false;
    }

    public void Reset()
    {
        IsRunning = false;
        TimeElapsed = 0f;
        //  timer updated event
    }

    public void UpdateTime()
    {
        TimeElapsed += Time.deltaTime;
        //timer updated event
    }
}
