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
        EventManager.Instance.onLogAdded += PopulateDropdown;
        EventManager.Instance.onTabSwitched += PopulateDropdown;
        EventManager.Instance.onLogDeleted += PopulateDropdown;
    }

    private void OnDisable()
    {
        EventManager.Instance.onLogsPopulated -= UpdateCurrentLog;
        EventManager.Instance.onBossDropdownValueChanged -= PopulateDropdown;
        EventManager.Instance.onLogAdded -= PopulateDropdown;
        EventManager.Instance.onTabSwitched -= PopulateDropdown;
        EventManager.Instance.onLogDeleted -= PopulateDropdown;
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

        EventManager.Instance.LogsPopulated();
    }

    public void OnValueChanged()
    {
        UpdateCurrentLog();
        EventManager.Instance.LogDropDownValueChanged();
    }
}
