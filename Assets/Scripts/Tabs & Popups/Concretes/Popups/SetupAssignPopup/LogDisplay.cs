using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays a single log name for a boss and a check mark to indicate if this log is associated with setup in question
/// </summary>
public class LogDisplay : MonoBehaviour
{
    //  Properties & fields

    private Action<string, string> _toggleCallback;
    private string _bossName;
    private string _logName;
    [SerializeField] private Toggle _logNameToggle;

    //  Methods

    public void Setup(string bossName, BossLogsScrollList.LogDisplayInfo logDisplayInfo, float yPos, Action<string, string> toggleCallback)
    {
        _bossName = bossName;
        _logName = logDisplayInfo.LogName;
        _toggleCallback = toggleCallback;
        _logNameToggle.isOn = logDisplayInfo.IsLinkedToSetup;
        string currentSetupMessage = string.IsNullOrEmpty(logDisplayInfo.SetupName) ? "(No linked setup)" : $"(Current setup is {logDisplayInfo.SetupName})";
        GetComponentInChildren<Text>().text = $"  {_logName} {currentSetupMessage}";
        transform.position = new Vector3(transform.position.x, -yPos, transform.position.z);
        _logNameToggle.onValueChanged.AddListener(LogNameToggle_OnValueChanged);
    }

    private void LogNameToggle_OnValueChanged(bool flag)
    {
        _toggleCallback(_bossName, _logName);
    }
}
