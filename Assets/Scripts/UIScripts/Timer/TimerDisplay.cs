using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//  Display class for the timer
public class TimerDisplay : MonoBehaviour
{
    [SerializeField] private Text timerText;

    public void SetText()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(Timer.timeElapsed);
        timerText.text = $"Timer: {timeSpan.ToString("hh\\h\\:mm\\m\\:ss\\s")}";
    }
}
