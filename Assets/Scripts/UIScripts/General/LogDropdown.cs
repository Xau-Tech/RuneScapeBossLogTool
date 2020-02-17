using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogDropdown : MonoBehaviour
{
    private Dropdown m_ThisDropdown;

    private void Awake()
    {
        m_ThisDropdown = this.gameObject.GetComponent<Dropdown>();
    }

    private void OnEnable()
    {
        EventManager.Instance.onLogsPopulated += UpdateCurrentLog;
        EventManager.Instance.onBossDropdownValueChanged += PopulateDropdown;
        EventManager.Instance.onLogAdded += CacheLogValue;
        EventManager.Instance.onTabSwitched += CacheLogValue;
        EventManager.Instance.onLogDeleted += PopulateDropdown;
        EventManager.Instance.onLogUpdated += CacheLogValue;
    }

    private void OnDisable()
    {
        EventManager.Instance.onLogsPopulated -= UpdateCurrentLog;
        EventManager.Instance.onBossDropdownValueChanged -= PopulateDropdown;
        EventManager.Instance.onLogAdded -= CacheLogValue;
        EventManager.Instance.onTabSwitched -= CacheLogValue;
        EventManager.Instance.onLogDeleted -= PopulateDropdown;
        EventManager.Instance.onLogUpdated -= CacheLogValue;
    }

    private void UpdateCurrentLog()
    {
        if(ProgramState.CurrentState == ProgramState.states.Drops)
        {
            DataController.Instance.CurrentDropTabLog = m_ThisDropdown.options[m_ThisDropdown.value].text;
        }
        else if(ProgramState.CurrentState == ProgramState.states.Logs)
        {
            DataController.Instance.CurrentLogTabLog = m_ThisDropdown.options[m_ThisDropdown.value].text;
        }
    }

    private void CacheLogValue(string _logName)
    {
        PopulateDropdown();
        SelectLog(_logName);
    }

    private void PopulateDropdown()
    {
        m_ThisDropdown.ClearOptions();

        List<string> logNames;

        if ((logNames = DataController.Instance.BossLogsDictionary.GetBossLogNamesList(DataController.Instance.CurrentBoss)).Count == 0)
        {
            logNames.Add("-Create new log-");
        }

        logNames.Sort();
        m_ThisDropdown.AddOptions(logNames);

        //  This is the end of our loading process if we are in the logs tab
        if (ProgramState.CurrentState == ProgramState.states.Logs)
            PopupState.currentState = PopupState.states.None;

        EventManager.Instance.LogsPopulated();
    }

    //  Select the passed log; select top value if null passed
    private void SelectLog(string _logName)
    {
        if(_logName == "")
        {
            m_ThisDropdown.value = 0;
        }

        m_ThisDropdown.value = m_ThisDropdown.options.FindIndex(option => option.text.CompareTo(_logName) == 0);

        EventManager.Instance.LogsPopulated();
    }

    public void OnValueChanged()
    {
        UpdateCurrentLog();
        EventManager.Instance.LogDropDownValueChanged();
    }
}
