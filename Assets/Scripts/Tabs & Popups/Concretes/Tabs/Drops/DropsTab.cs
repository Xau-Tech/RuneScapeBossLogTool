using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleSheetsToUnity;
using System.Threading.Tasks;

/// <summary>
/// Class to track all data & UI for the drops tab
/// </summary>
public class DropsTab : AbstractTab
{
    //  Properties & fields
    public override Enums.TabStates AssociatedTabState { get { return Enums.TabStates.Drops; } }
    public ushort Killcount { get { return _killcountScript.Killcount; } }
    public ulong LootValue { get { return _dropList.Value; } }
    public ItemSlotList DropsList { get { return _dropList.Drops; } }

    [SerializeField] private Dropdown _bossDropdown;
    [SerializeField] private Dropdown _logsDropdown;
    [SerializeField] private Dropdown _itemDropdown;
    [SerializeField] private InputField _itemSearchField;
    [SerializeField] private InputField _itemAmountField;
    [SerializeField] private Button _addItemButton;
    [SerializeField] private Button _newLogButton;
    [SerializeField] private Button _deleteLogButton;
    [SerializeField] private Button _renameLogButton;
    [SerializeField] private Button _logTripButton;
    [SerializeField] private DropList _dropList;
    [SerializeField] private KillcountScript _killcountScript;
    [SerializeField] private TimerDisplay _timerDisplay;
    private ItemList _bossItems;

    //  Monobehavior methods

    private void Awake()
    {
        //  Set data state to loading
        AppState.DataState = Enums.DataStates.Loading;

        //  Sub to UI events
        _bossDropdown.onValueChanged.AddListener(BossDropdown_OnValueChanged);
        _logsDropdown.onValueChanged.AddListener(LogDropdown_OnValueChanged);
        _itemAmountField.GetComponent<IFOnEndEnter>().EndEditAction = AddItemToDrops;
        _addItemButton.onClick.AddListener(AddItemToDrops);
        _newLogButton.onClick.AddListener(NewLogButton_OnClick);
        _deleteLogButton.onClick.AddListener(DeleteLogButton_OnClick);
        _renameLogButton.onClick.AddListener(RenameLogButton_OnClick);
        _logTripButton.onClick.AddListener(LogTripButton_OnClick);

        //  Add bosses to dropdown
        _bossDropdown.ClearOptions();
        _bossDropdown.AddOptions(ApplicationController.Instance.BossInfo.GetOrderedBossNames());
        base.CurrentBoss = ApplicationController.Instance.BossInfo.GetBoss(_bossDropdown.options[_bossDropdown.value].text);

        //  Add logs to dropdown
        _logsDropdown.ClearOptions();
        _logsDropdown.AddOptions(ApplicationController.Instance.BossLogs.GetSortedLogList(base.CurrentBoss.BossId));
        base.CurrentLog = ApplicationController.Instance.BossLogs.GetBossLog(base.CurrentBoss.BossId, _logsDropdown.options[_logsDropdown.value].text);

        //  Send out call to fill items
        _bossItems = new ItemList();
        string workSheetName = base.CurrentBoss.BossName + " " + ApplicationController.OptionController.GetOptionValue(Enums.OptionNames.RSVersion);
        GSTU_Search search = new GSTU_Search(ApplicationController.SHEETID, workSheetName);
        SpreadsheetManager.ReadPublicSpreadsheet(search, _bossItems.FillItemList);
    }

    protected override void OnEnable()
    {
        //  Events
        EventManager.Instance.onBossItemsLoaded += FillItemListUI;

        //  Reload logs in case of any changes
        _logsDropdown.ClearOptions();
        _logsDropdown.AddOptions(ApplicationController.Instance.BossLogs.GetSortedLogList(base.CurrentBoss.BossId));

        //  Move than one log and the previous log is a still-existing, player-created one
        if (_logsDropdown.options.Count > 1 && !ApplicationController.Instance.BossLogs.GetBossLog(base.CurrentBoss.BossId, base.CurrentLog.logName).IsEmpty())
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

        base.OnEnable();
    }

    private void OnDisable()
    {
        //  Events
        EventManager.Instance.onBossItemsLoaded -= FillItemListUI;
    }

    //  Methods

    private void FillItemListUI()
    {
        _itemDropdown.ClearOptions();
        _itemDropdown.AddOptions(_bossItems.ItemNames());

        //  Set data state back to none
        AppState.DataState = Enums.DataStates.None;
    }

    /// <summary>
    /// Add the current item in the item dropdown to the droplist
    /// </summary>
    private void AddItemToDrops()
    {
        //  Get a reference to the selected item
        Item item = _bossItems.AtIndex(_itemDropdown.value);
        ulong itemAmount;
        bool isItemRare = RareItemDB.IsRare(base.CurrentBoss.BossName, item.ItemId);

        //  Make sure text field value is in valid range
        if(!ulong.TryParse(_itemAmountField.text, out itemAmount))
        {
            PopupManager.Instance.ShowNotification($"You must enter a value from 1 to {uint.MaxValue} (or {ushort.MaxValue} for rare items)!");
            _itemAmountField.text = "";
            return;
        }

        //  Ensure user didn't enter 0
        if(itemAmount == 0)
        {
            PopupManager.Instance.ShowNotification($"You must enter a value from 1 to {uint.MaxValue} (or {ushort.MaxValue} for rare items)!");
            _itemAmountField.text = "";
            return;
        }

        //  Check to ensure proper input
        if(itemAmount > ushort.MaxValue && isItemRare)
        {
            PopupManager.Instance.ShowNotification($"Rare items are limited to a quantity of {ushort.MaxValue}!");
            _itemAmountField.text = "";
            return;
        }
        else if(itemAmount > uint.MaxValue && !isItemRare)
        {
            PopupManager.Instance.ShowNotification($"Non-rare items are limited to a quantity of {uint.MaxValue}!");
            _itemAmountField.text = "";
            return;
        }

        if((ulong)itemAmount * item.Price > ulong.MaxValue)
        {
            PopupManager.Instance.ShowNotification($"Cannot add {itemAmount} {item.ItemName}!\nMax loot value is {ulong.MaxValue}.");
            _itemAmountField.text = "";
            return;
        }

        //  Item is already in the drop list so increment with amount
        if (_dropList.Exists(item.ItemName))
            _dropList.AddToDrop(item.ItemName, (uint)itemAmount, item.ItemId);
        else
            _dropList.Add(new ItemSlot(item, (uint)itemAmount));

        _itemAmountField.text = "";
        _dropList.Print();
        _itemDropdown.Select();
    }

    /// <summary>
    /// Call to reset UI when a trip has been succesfully logged
    /// </summary>
    public void LoggedTrip_Reset()
    {
        _itemDropdown.SetValueWithoutNotify(0);
        _itemAmountField.text = "";
        _dropList.Clear();
        _timerDisplay.ResetButton_OnClick();
        _killcountScript.ResetKillcount();
    }

    //  UI events

    private async void BossDropdown_OnValueChanged(int value)
    {
        //  Check if there is active data in the drop list
        if(_dropList.Drops.Count > 0)
        {
            //  Confirm the user wants to switch bosses - they will lose their entered data
            bool willSwapBosses = await PopupManager.Instance.ShowConfirm("If you switch bosses, all of the items you have added will be reset - is that okay?");

            //  If user denies, swap back to previous option without triggering events and end the function
            if (!willSwapBosses)
            {
                int i = _bossDropdown.options.FindIndex(option => option.text.CompareTo(base.CurrentBoss.BossName) == 0);
                _bossDropdown.SetValueWithoutNotify(i);
                return;
            }
        }

        //  Set data state to loading
        AppState.DataState = Enums.DataStates.Loading;

        //  Get newly selected boss
        BossInfo newBoss = ApplicationController.Instance.BossInfo.GetBoss(_bossDropdown.options[value].text);

        //  Set current boss for this tab & entire app to the new boss
        base.CurrentBoss = newBoss;
        ApplicationController.Instance.CurrentBoss = newBoss;

        //  Update log data
        _logsDropdown.ClearOptions();
        _logsDropdown.AddOptions(ApplicationController.Instance.BossLogs.GetSortedLogList(base.CurrentBoss.BossId));
        _logsDropdown.SetValueWithoutNotify(0);
        base.CurrentLog = ApplicationController.Instance.BossLogs.GetBossLog(base.CurrentBoss.BossId, _logsDropdown.options[_logsDropdown.value].text);
        ApplicationController.Instance.CurrentLog = base.CurrentLog;

        //  Reset UI elements
        _itemDropdown.SetValueWithoutNotify(0);
        _itemAmountField.text = "";
        _dropList.Clear();
        _timerDisplay.ResetButton_OnClick();
        _killcountScript.ResetKillcount();

        //  Send out call to fill items
        _bossItems.Clear();
        string workSheetName = base.CurrentBoss.BossName + " " + ApplicationController.OptionController.GetOptionValue(Enums.OptionNames.RSVersion);
        GSTU_Search search = new GSTU_Search(ApplicationController.SHEETID, workSheetName);
        SpreadsheetManager.ReadPublicSpreadsheet(search, _bossItems.FillItemList);
    }

    private void LogDropdown_OnValueChanged(int value)
    {
        //  Get the newly selected log
        short bossId = base.CurrentBoss.BossId;
        string newLogName = _logsDropdown.options[value].text;
        BossLog newLog = ApplicationController.Instance.BossLogs.GetBossLog(bossId, newLogName);

        //  Set current log for this tab & entire app to the new log
        base.CurrentLog = newLog;
        ApplicationController.Instance.CurrentLog = newLog;
    }

    private void LogTripButton_OnClick()
    {
        //  Make sure there is a log created for the current boss
        if (base.CurrentLog.IsEmpty())
            PopupManager.Instance.ShowNotification("You must create a log before attempting to add new data!");
        else
            PopupManager.Instance.ShowLogTrip(this);
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
        }
    }
}
