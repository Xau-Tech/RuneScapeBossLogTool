using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class to track all data & UI for the logs tab
/// </summary>
public class LogsTab : AbstractTab
{
    //  Properties & fields
    public override Enums.TabStates AssociatedTabState { get { return Enums.TabStates.Logs; } }

    [SerializeField] private Dropdown _bossDropdown;
    [SerializeField] private Dropdown _logsDropdown;
    [SerializeField] private Button _newLogButton;
    [SerializeField] private Button _deleteLogButton;
    [SerializeField] private Button _renameLogButton;
    [SerializeField] private GameObject _bossWidgetLoc;
    [SerializeField] private GameObject _logWidgetLoc;
    private BossTotalsDisplay _bossTotalsWidget;
    private LogTotalsDisplay _logTotalsWidget;

    //  Monobehavior methods

    private void Awake()
    {
        //  Set data state to loading
        AppState.DataState = Enums.DataStates.Loading;

        //  Sub to UI events
        _bossDropdown.onValueChanged.AddListener(BossDropdown_OnValueChanged);
        _logsDropdown.onValueChanged.AddListener(LogDropdown_OnValueChanged);
        _newLogButton.onClick.AddListener(NewLogButton_OnClick);
        _deleteLogButton.onClick.AddListener(DeleteLogButton_OnClick);
        _renameLogButton.onClick.AddListener(RenameLogButton_OnClick);

        //  Set up UI
        _bossTotalsWidget = LogDisplayFactory.Instantiate(Enums.LogDisplays.BossTotals, _bossWidgetLoc).GetComponent<BossTotalsDisplay>();
        _logTotalsWidget = LogDisplayFactory.Instantiate(Enums.LogDisplays.LogTotals, _logWidgetLoc).GetComponent<LogTotalsDisplay>();

        //  Add bosses to dropdown
        _bossDropdown.ClearOptions();
        _bossDropdown.AddOptions(ApplicationController.Instance.BossInfo.GetOrderedBossNames());
        base.CurrentBoss = ApplicationController.Instance.BossInfo.GetBoss(_bossDropdown.options[_bossDropdown.value].text);

        //  Set the boss totals display
        _bossTotalsWidget.LoadSprites(base.CurrentBoss);
        _bossTotalsWidget.Display(ApplicationController.Instance.BossLogs.GetBossLogList(base.CurrentBoss.BossId));

        //  Add logs to dropdown
        _logsDropdown.ClearOptions();
        _logsDropdown.AddOptions(ApplicationController.Instance.BossLogs.GetSortedLogList(base.CurrentBoss.BossId));
        base.CurrentLog = ApplicationController.Instance.BossLogs.GetBossLog(base.CurrentBoss.BossId, _logsDropdown.options[_logsDropdown.value].text);

        //  Set the log display
        _logTotalsWidget.LoadSprites(base.CurrentBoss);
        _logTotalsWidget.Display(base.CurrentLog);

        //  Set data state back to normal
        AppState.DataState = Enums.DataStates.None;
    }

    protected override void OnEnable()
    {
        //  Re-display boss totals in case data has been added while this tab was inactive
        _bossTotalsWidget.Display(ApplicationController.Instance.BossLogs.GetBossLogList(base.CurrentBoss.BossId));

        //  Reload logs in case of any changes
        _logsDropdown.ClearOptions();
        _logsDropdown.AddOptions(ApplicationController.Instance.BossLogs.GetSortedLogList(base.CurrentBoss.BossId));

        //  Move than one log and the previous log is a still-existing, player-created one
        if(_logsDropdown.options.Count > 1 && !ApplicationController.Instance.BossLogs.GetBossLog(base.CurrentBoss.BossId, base.CurrentLog.logName).IsEmpty())
        {
            //  Find the current index of the log and update
            int newIndex = ApplicationController.Instance.BossLogs.GetSortedLogList(base.CurrentBoss.BossId).FindIndex(name => name.CompareTo(base.CurrentLog.logName) == 0);
            _logsDropdown.SetValueWithoutNotify(newIndex);
        }
        //  Either only one log exists or the previously selected log no longer exists - either way, select the top option
        else
        {
            _logsDropdown.SetValueWithoutNotify(0);
            base.CurrentLog = ApplicationController.Instance.BossLogs.GetBossLog(base.CurrentBoss.BossId, _logsDropdown.options[0].text);
        }

        //  Re-display log in case data has been added while this tab was inactive
        _logTotalsWidget.Display(base.CurrentLog);

        base.OnEnable();
    }

    //  UI events

    private void BossDropdown_OnValueChanged(int value)
    {
        //  Update boss
        BossInfo newBoss = ApplicationController.Instance.BossInfo.GetBoss(_bossDropdown.options[value].text);
        base.CurrentBoss = newBoss;
        ApplicationController.Instance.CurrentBoss = newBoss;

        //  Update logs
        _logsDropdown.ClearOptions();
        _logsDropdown.AddOptions(ApplicationController.Instance.BossLogs.GetSortedLogList(newBoss.BossId));
        _logsDropdown.SetValueWithoutNotify(0);

        //  Update boss display
        _bossTotalsWidget.LoadSprites(newBoss);
        _bossTotalsWidget.Display(ApplicationController.Instance.BossLogs.GetBossLogList(base.CurrentBoss.BossId));

        //  Update logs display
        _logTotalsWidget.LoadSprites(newBoss);
        _logsDropdown.value = 0;
        LogDropdown_OnValueChanged(0);
    }

    private void LogDropdown_OnValueChanged(int value)
    {
        BossLog newLog = ApplicationController.Instance.BossLogs.GetBossLog(base.CurrentBoss.BossId, _logsDropdown.options[value].text);
        base.CurrentLog = newLog;
        ApplicationController.Instance.CurrentLog = newLog;
        _logTotalsWidget.Display(base.CurrentLog);
    }

    private async void NewLogButton_OnClick()
    {
        string newLogName = await PopupManager.Instance.ShowInputPopup(InputPopup.ADDLOG);

        //  User created a new log
        if (!string.IsNullOrEmpty(newLogName))
        {
            //  Refresh log list
            _logsDropdown.ClearOptions();
            List<string> logNames = ApplicationController.Instance.BossLogs.GetSortedLogList(base.CurrentBoss.BossId);
            _logsDropdown.AddOptions(logNames);

            //  Select the new log
            int newLogIndex = logNames.FindIndex(name => name.CompareTo(newLogName) == 0);
            _logsDropdown.SetValueWithoutNotify(newLogIndex);
            base.CurrentLog = ApplicationController.Instance.BossLogs.GetBossLog(base.CurrentBoss.BossId, newLogName);
            ApplicationController.Instance.CurrentLog = base.CurrentLog;
            LogDropdown_OnValueChanged(newLogIndex);
        }
    }

    private async void DeleteLogButton_OnClick()
    {
        //  Make sure a valid log exists
        if (!CurrentLog.IsEmpty())
        {
            //  Ask the user if they are sure they want to delete the log
            bool deleteLog = await PopupManager.Instance.ShowConfirm($"Are you sure you want to delete {CurrentLog.logName}?");

            //  User confirms delete
            if (deleteLog)
            {
                //  Remove log
                ApplicationController.Instance.BossLogs.RemoveLog(base.CurrentBoss.BossId, base.CurrentLog.logName);

                //  Refresh log dropdown options
                _logsDropdown.ClearOptions();
                _logsDropdown.AddOptions(ApplicationController.Instance.BossLogs.GetSortedLogList(base.CurrentBoss.BossId));

                //  Select top option
                _logsDropdown.SetValueWithoutNotify(0);
                base.CurrentLog = ApplicationController.Instance.BossLogs.GetBossLog(base.CurrentBoss.BossId, _logsDropdown.options[0].text);
                _logTotalsWidget.Display(base.CurrentLog);

                //  Refresh the totals widget
                _bossTotalsWidget.Display(ApplicationController.Instance.BossLogs.GetBossLogList(base.CurrentBoss.BossId));
            }
        }
        else
        {
            PopupManager.Instance.ShowNotification($"There are no logs to delete!");
        }
    }

    private async void RenameLogButton_OnClick()
    {
        string newLogName = await PopupManager.Instance.ShowInputPopup(InputPopup.RENAMELOG);

        //  User created a new log
        if (!string.IsNullOrEmpty(newLogName))
        {
            //  Refresh log list
            _logsDropdown.ClearOptions();
            List<string> logNames = ApplicationController.Instance.BossLogs.GetSortedLogList(base.CurrentBoss.BossId);
            _logsDropdown.AddOptions(logNames);

            //  Select the new log
            int newLogIndex = logNames.FindIndex(name => name.CompareTo(newLogName) == 0);
            _logsDropdown.SetValueWithoutNotify(newLogIndex);
            base.CurrentLog = ApplicationController.Instance.BossLogs.GetBossLog(base.CurrentBoss.BossId, newLogName);
            ApplicationController.Instance.CurrentLog = base.CurrentLog;

            _logTotalsWidget.Display(base.CurrentLog);
        }
    }
}
