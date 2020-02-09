using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public static TimerScript Instance;

    public float TimeAtSwitch { set { m_TimeAtSwitch = value; } }

    private float m_TimeAtSwitch;
    private float m_Time;
    private bool m_IsRunning;

    [SerializeField]
    private Text m_TimerStartStopButtonText;
    [SerializeField]
    private Text m_TimerText;

    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        m_TimeAtSwitch = 0f;
        m_Time = 0f;
        m_IsRunning = false;
    }

    private void OnEnable()
    {
        EventManager.Instance.onBossDropdownValueChanged += OnResetClicked;
        if(m_IsRunning)
        {
            m_Time += (Time.time - m_TimeAtSwitch);
        }
    }

    private void OnDisable()
    {
        EventManager.Instance.onBossDropdownValueChanged -= OnResetClicked;
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

    public int TimerSecondsAsInt()
    {
        return Mathf.RoundToInt(m_Time);
    }
}
