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
    }

    private void OnEnable()
    {
        EventManager.Instance.onBossDropdownValueChanged += FillAndSelectTopValue;
        thisDropdown.onValueChanged.AddListener(SetCurrentLog);
        EventManager.Instance.onLogAdded += FillAndSelectLog;
        EventManager.Instance.onTabChanged += FillAndSelectPreviousValue;
        EventManager.Instance.onLogDeleted += FillAndSelectTopValue;
        EventManager.Instance.onLogRename += FillAndSelectLog;
    }

    private void OnDisable()
    {
        EventManager.Instance.onBossDropdownValueChanged -= FillAndSelectTopValue;
        thisDropdown.onValueChanged.RemoveListener(SetCurrentLog);
        EventManager.Instance.onLogAdded -= FillAndSelectLog;
        EventManager.Instance.onTabChanged -= FillAndSelectPreviousValue;
        EventManager.Instance.onLogDeleted -= FillAndSelectTopValue;
        EventManager.Instance.onLogRename -= FillAndSelectLog;
    }

    //  Use on tab changes to refill log list in case of data changes
    //  While retaining previous log selection
    private void FillAndSelectPreviousValue()
    {
        Debug.Log($"TabChange trying to find and select log - {CacheManager.currentLog}");
        FillAndSelectLog(CacheManager.currentLog);
    }

    private void SetCurrentLog(int index)
    {
        //  Select top option if log couldn't be found
        if (index == -1)
            index = 0;

        CacheManager.currentLog = thisDropdown.options[index].text;

        if (thisDropdown.value == index)
            return;

        thisDropdown.value = index;
    }

    private void FillAndSelectTopValue()
    {
        FillDropdown(true);
    }

    //  Called when new log is added
    //  Fills the dropdown and then sets the current value of the dropdown to the new log
    private void FillAndSelectLog(string logName)
    {
        FillDropdown(false);
        SetCurrentLog(GetLogIndex(logName));
    }

    //  Fill the dropdown from our bossLogsDictionary data
    private void FillDropdown(bool setLogFlag)
    {
        thisDropdown.ClearOptions();
        thisDropdown.AddOptions(DataController.Instance.bossLogsDictionary.GetBossLogNamesList(CacheManager.currentBoss.bossID));

        if(setLogFlag)
            SetCurrentLog(0);

        if(ProgramState.CurrentState == ProgramState.states.Drops)
        {
            if (DataState.CurrentState == DataState.states.Loading)
            {
                if (CacheManager.DropsTab.IsUILoaded(CacheManager.DropsTab.Elements.LogDropdown, true))
                    DataState.CurrentState = DataState.states.None;
            }
        }
        else if(ProgramState.CurrentState == ProgramState.states.Logs)
        {
            if(DataState.CurrentState == DataState.states.Loading)
            {
                if (CacheManager.LogsTab.IsUILoaded(CacheManager.LogsTab.Elements.LogDropdown, true))
                    DataState.CurrentState = DataState.states.None;
            }
        }
    }

    //  Select the passed log as the value of this dropdown
    private int GetLogIndex(string logName)
    {
        return thisDropdown.options.FindIndex(option => option.text.CompareTo(logName) == 0);
    }
}
