using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimerScript : MonoBehaviour
{
    private float m_Time;
    private bool m_IsRunning;

    
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
            UIController.uicontroller.m_TimerText.text = "Timer: " + readableTime;
        }
    }

    public void OnStartStopClicked()
    {
        if(m_IsRunning)
        {
            m_IsRunning = false;
            UIController.uicontroller.m_TimerStartStopButtonText.text = "Start Timer";
        }
        else
        {
            m_IsRunning = true;
            UIController.uicontroller.m_TimerStartStopButtonText.text = "Stop Timer";
        }
    }

    public void OnResetClicked()
    {
        OnStartStopClicked();
        m_Time = 0f;
        UIController.uicontroller.m_TimerText.text = "Timer: 00h:00m:00s";
    }
}
