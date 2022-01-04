using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI elements and display for the timer
/// </summary>
public class TimerDisplay : MonoBehaviour
{
    //  Properties & fields
    public Timer Timer { get; private set; } = new Timer();

    [SerializeField] private Text _timerText;
    [SerializeField] private Button _startStopButton;
    [SerializeField] private Button _resetButton;

    //  Methods

    private void Awake()
    {
        _startStopButton.onClick.AddListener(StartStopButton_OnClick);
        _resetButton.onClick.AddListener(ResetButton_OnClick);
    }

    private void StartStopButton_OnClick()
    {
        if (Timer.IsRunning)
        {
            Timer.Stop();
            _startStopButton.GetComponentInChildren<Text>().text = "Start Timer";
        }
        else
        {
            Timer.Start();
            _startStopButton.GetComponentInChildren<Text>().text = "Stop Timer";
        }
    }

    public void ResetButton_OnClick()
    {
        Timer.Reset();
        _startStopButton.GetComponentInChildren<Text>().text = "Start Timer";
        TimeSpan ts = TimeSpan.FromSeconds(Timer.TimeElapsed);
        _timerText.text = $"Timer: {ts.ToString("hh\\h\\:mm\\m\\:ss\\s")}";
    }

    public void SetText()
    {
        TimeSpan ts = TimeSpan.FromSeconds(Timer.TimeElapsed);
        _timerText.text = $"Timer: {ts.ToString("hh\\h\\:mm\\m\\:ss\\s")}";
    }
}
