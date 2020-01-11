using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogDropdownChange : MonoBehaviour
{
    private Dropdown m_ThisDropdown;

    private void Awake()
    {
        m_ThisDropdown = this.gameObject.GetComponent<Dropdown>();
    }

    private void OnEnable()
    {
        EventManager.manager.onLogsPopulated += UpdateCurrentLog;
    }

    private void OnDisable()
    {
        EventManager.manager.onLogsPopulated -= UpdateCurrentLog;
    }

    private void UpdateCurrentLog()
    {
        if(ProgramState.CurrentState == ProgramState.states.Drops)
        {
            DataController.dataController.CurrentDropTabLog = m_ThisDropdown.options[m_ThisDropdown.value].text;
        }
        else if(ProgramState.CurrentState == ProgramState.states.Logs)
        {
            DataController.dataController.CurrentLogTabLog = m_ThisDropdown.options[m_ThisDropdown.value].text;
        }
    }

    public void OnValueChanged()
    {
        UpdateCurrentLog();

        EventManager.manager.LogDropDownValueChanged();
    }
}
