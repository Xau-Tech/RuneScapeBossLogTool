using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Dropdown used to display available saved logs based on selected boss/encounter
public class LogDropdown : MonoBehaviour
{
    private Dropdown thisDropdown;

    private void Awake()
    {
        thisDropdown = GetComponent<Dropdown>();
        if (!thisDropdown)
            throw new System.Exception($"This script is not attached to a gameobject with a dropdown!");
        else
            thisDropdown.onValueChanged.AddListener(SetCurrentLog);
    }

    private void OnEnable()
    {
        EventManager.Instance.onBossDropdownValueChanged += FillDropdown;
        EventManager.Instance.onLogAdded += FillAndSelectLog;
        EventManager.Instance.onTabChanged += CacheCurrentLog;
        EventManager.Instance.onLogDeleted += FillDropdown;
        EventManager.Instance.onLogRename += FillAndSelectLog;
    }

    private void OnDisable()
    {
        EventManager.Instance.onBossDropdownValueChanged -= FillDropdown;
        EventManager.Instance.onLogAdded -= FillAndSelectLog;
        EventManager.Instance.onTabChanged -= CacheCurrentLog;
        EventManager.Instance.onLogDeleted -= FillDropdown;
        EventManager.Instance.onLogRename -= FillAndSelectLog;
    }

    //  Use on tab changes to refill log list in case of data changes
    //  While retaining previous log selection
    private void CacheCurrentLog()
    {
        FillAndSelectLog(CacheManager.currentLog);
    }

    private void SetCurrentLog(int index)
    {
        if (ProgramState.CurrentState == ProgramState.states.Drops)
        {
            CacheManager.DropsTab.currentLog = thisDropdown.options[index].text;
            Debug.Log($"New DropsTab log is {CacheManager.DropsTab.currentLog}");
        }
        else if (ProgramState.CurrentState == ProgramState.states.Logs)
        {
            CacheManager.LogsTab.currentLog = thisDropdown.options[index].text;
            Debug.Log($"New LogsTab log is {CacheManager.LogsTab.currentLog}");
        }
    }

    //  Called when new log is added
    //  Fills the dropdown and then sets the current value of the dropdown to the new log
    private void FillAndSelectLog(string logName)
    {
        FillDropdown();
        SelectLog(logName);
    }

    //  Fill the dropdown from our bossLogsDictionary data
    private void FillDropdown()
    {
        thisDropdown.ClearOptions();
        thisDropdown.AddOptions(DataController.Instance.bossLogsDictionary.GetBossLogNamesList(CacheManager.currentBoss));

        DataController.Instance.bossLogsDictionary.PrintLogNames();

        if(ProgramState.CurrentState == ProgramState.states.Drops)
        {
            CacheManager.DropsTab.currentLog = thisDropdown.options[thisDropdown.value].text;
            Debug.Log($"New DropsTab log is {CacheManager.DropsTab.currentLog}");

            if (DataState.CurrentState == DataState.states.Loading)
            {
                if (CacheManager.DropsTab.IsUILoaded(CacheManager.DropsTab.Elements.LogDropdown, true))
                    DataState.CurrentState = DataState.states.None;
            }
        }
        else if(ProgramState.CurrentState == ProgramState.states.Logs)
        {
            CacheManager.LogsTab.currentLog = thisDropdown.options[thisDropdown.value].text;
            Debug.Log($"New LogsTab log is {CacheManager.LogsTab.currentLog}");

            if(DataState.CurrentState == DataState.states.Loading)
            {
                if (CacheManager.LogsTab.IsUILoaded(CacheManager.LogsTab.Elements.LogDropdown, true))
                    DataState.CurrentState = DataState.states.None;
            }
        }

        //  Cheese to make sure SetCurrentLog function isn't called multiple times
        thisDropdown.onValueChanged.RemoveListener(SetCurrentLog);
        thisDropdown.onValueChanged?.Invoke(thisDropdown.value);
        thisDropdown.onValueChanged.AddListener(SetCurrentLog);
    }

    //  Select the passed log as the value of this dropdown
    private void SelectLog(string logName)
    {
        thisDropdown.value = thisDropdown.options.FindIndex(option => option.text.CompareTo(logName) == 0);

        //  Set current log
        if (ProgramState.CurrentState == ProgramState.states.Drops)
        {
            CacheManager.DropsTab.currentLog = thisDropdown.options[thisDropdown.value].text;
            Debug.Log($"New DropsTab log is {CacheManager.DropsTab.currentLog}");
        }
        else if (ProgramState.CurrentState == ProgramState.states.Logs)
        {
            CacheManager.LogsTab.currentLog = thisDropdown.options[thisDropdown.value].text;
            Debug.Log($"New LogsTab log is {CacheManager.LogsTab.currentLog}");
        }
    }
}
