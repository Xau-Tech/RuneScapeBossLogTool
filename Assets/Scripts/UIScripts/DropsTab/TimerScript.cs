using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    private float m_Time;
    private bool m_IsRunning;

    [SerializeField]
    private Text m_TimerStartStopButtonText;
    [SerializeField]
    private Text m_TimerText;

    
    private void Awake()
    {
        m_Time = 0f;
        m_IsRunning = false;
    }

    private void Update()
    {
        if (m_IsRunning)
        {
            m_Time += Time.deltaTime;
            string readableTime = TimeSpan.FromSeconds(m_Time).ToString("hh\\h\\:mm\\m\\:ss\\s");
            UpdateTimerText("Timer: " + readableTime);
        }
    }

    public string GetTimerMinutes()
    {
        string time;

        time = Mathf.FloorToInt((m_Time / 60.0f)).ToString();

        return time;
    }

    public void OnStartStopClicked()
    {
        if (m_IsRunning)
            StopTimer();
        else
            StartTimer();
    }

    private void StartTimer()
    {
        m_IsRunning = true;
        UpdateStartStopButtonText("Stop Timer");
    }

    private void StopTimer()
    {
        m_IsRunning = false;
        UpdateStartStopButtonText("Start Timer");
    }

    public void OnResetClicked()
    {
        StopTimer();
        m_Time = 0f;
        UpdateTimerText("Timer: 00h:00m:00s");
    }

    private void UpdateStartStopButtonText(string _s)
    {
        m_TimerStartStopButtonText.text = _s;
    }

    private void UpdateTimerText(string _s)
    {
        m_TimerText.text = _s;
    }
}
