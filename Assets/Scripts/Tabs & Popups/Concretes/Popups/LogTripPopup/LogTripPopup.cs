using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Popup to allow users to add data to their currently selected boss log with data entered via the drops tab
/// </summary>
public class LogTripPopup : AbstractPopup
{
    //  Properties & fields
    public override Enums.PopupStates AssociatedPopupState { get { return Enums.PopupStates.LogTrip; } }
    public readonly int KILLS = 0;
    public readonly int LOOT = 1;
    public readonly int TIME = 2;

    [SerializeField] InputField _killsInputField;
    [SerializeField] InputField _timeInputField;
    [SerializeField] InputField _lootInputField;
    [SerializeField] Text _logText;
    [SerializeField] Text _killsText;
    [SerializeField] Text _timeText;
    [SerializeField] Text _lootText;
    [SerializeField] Dropdown _timeUnitDropdown;
    [SerializeField] Button _enterButton;
    [SerializeField] Button _cancelButton;
    private uint _currentKills;
    private ulong _currentLoot;
    private uint _currentTimeInMins;
    private DropsTab _dropsTab;

    //  Monobehavior methods

    private void Awake()
    {
        _enterButton.onClick.AddListener(EnterButton_OnClick);
        _cancelButton.onClick.AddListener(ClosePopup);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        _timeUnitDropdown.onValueChanged.RemoveAllListeners();
        base.OnDisable();
    }

    //  Methods

    public override bool OpenPopup(DropsTab dropTab)
    {
        _dropsTab = dropTab;
        bool flag = base.OpenPopup(_dropsTab);
        FillValues();
        _timeUnitDropdown.onValueChanged.AddListener(TimeUnitDropdown_OnValueChanged);
        _killsInputField.Select();
        StartCoroutine(KillsFieldCaretToEnd());
        return flag;
    }

    protected override void ClosePopup()
    {
        base.ClosePopup();
    }

    private void FillValues()
    {
        float timeElapsed = ApplicationController.Instance.TimeElapsed;

        //  Display current log info
        BossLog currentLog = ApplicationController.Instance.CurrentLog;
        _currentKills = currentLog.kills;
        _currentLoot = currentLog.loot;
        _currentTimeInMins = currentLog.time / 60;
        uint newTimeInMins = (uint)Mathf.FloorToInt((currentLog.time + timeElapsed) / 60.0f);

        _logText.text = $"Log: {currentLog.logName}";
        _killsText.text = $"Kills: {_currentKills.ToString("N0")} -> {(_currentKills + _dropsTab.Killcount).ToString("N0")}";
        _lootText.text = $"Loot: {_currentLoot.ToString("N0")} -> {(_currentLoot + _dropsTab.LootValue).ToString("N0")}";
        _timeText.text = $"Time (mins): {_currentTimeInMins.ToString("N0")} -> {newTimeInMins.ToString("N0")}";

        _killsInputField.text = _dropsTab.Killcount.ToString();
        _lootInputField.text = _dropsTab.LootValue.ToString("N0");

        //  Timer is not 0 - fill from timer and set dropdown to seconds
        if(timeElapsed != 0.0f)
        {
            _timeInputField.text = Mathf.FloorToInt(timeElapsed).ToString();
            _timeUnitDropdown.value = 0;
        }
        //  Timer is 0 - set to 0 and set dropdown to minutes
        else
        {
            _timeInputField.text = "0";
            _timeUnitDropdown.value = 1;
        }
    }

    private IEnumerator KillsFieldCaretToEnd()
    {
        yield return new WaitForSeconds(float.MinValue);
        _killsInputField.MoveTextEnd(false);
    }

    /// <summary>
    /// Updates text showing how the data in the log will change based on user interaction
    /// </summary>
    /// <param name="propertyChanged"></param>
    /// <param name="newAddedValue"></param>
    public void UpdateAddedValue(int propertyChanged, int newAddedValue)
    {
        if(propertyChanged == KILLS)
        {
            if (newAddedValue < 0)
            {
                _killsInputField.text = "0";
                newAddedValue = 0;
            }

            _killsText.text = $"Kills: {_currentKills.ToString("N0")} -> {(_currentKills + newAddedValue).ToString("N0")}";
        }
        else if(propertyChanged == LOOT)
        {
            if (newAddedValue < 0)
            {
                _lootInputField.text = "0";
                newAddedValue = 0;
            }

            _lootText.text = $"Loot: {_currentLoot.ToString("N0")} -> {(_currentLoot + (ulong)newAddedValue).ToString("N0")}";
        }
        else if(propertyChanged == TIME)
        {
            Debug.Log("time updated with value of " + newAddedValue);
            if (newAddedValue < 0)
            {
                _timeInputField.text = "0";
                newAddedValue = 0;
            }

            uint addedTimeInMins;

            //  Dropdown is in seconds
            if (_timeUnitDropdown.value == 0)
                addedTimeInMins = (uint)(newAddedValue / 60);
            //  Dropdown is in minutes
            else
                addedTimeInMins = (uint)newAddedValue;

            _timeText.text = $"Time (mins): {_currentTimeInMins.ToString("N0")} -> {(_currentTimeInMins + addedTimeInMins).ToString("N0")}";
        }
        else
        {
            Debug.Log("Improper value has been passed - 0 = kills, 1 = loot, 2 = time");
        }
    }
    
    //  UI events

    //  Attempt to update the log
    private void EnterButton_OnClick()
    {
        //  Make sure none of the input fields are empty
        if(_killsInputField.text == "" || _lootInputField.text == "" || _timeInputField.text == "")
        {
            PopupManager.Instance.ShowNotification("You must enter some integer value in each field!");
        }
        else
        {
            //  Make sure each value is within the proper ranges for its data type
            uint kills;
            if (!uint.TryParse(_killsInputField.text, out kills))
            {
                PopupManager.Instance.ShowNotification($"Kills must be a value from 0 to {uint.MaxValue}!");
                return;
            }

            ulong loot;
            if (!ulong.TryParse(_lootInputField.text, out loot))
            {
                PopupManager.Instance.ShowNotification($"Loot must be a value from 0 to {ulong.MaxValue}!");
                return;
            }

            uint time;
            if (!uint.TryParse(_timeInputField.text, out time))
            {
                PopupManager.Instance.ShowNotification($"Time must be a value from 0 to {uint.MaxValue}!");
            }

            //  Convert from minutes to seconds if needed
            if (_timeUnitDropdown.value == 1)
            {
                time *= 60;
            }

            //  Populate a new BossLog object with our data
            BossLog currentLog = ApplicationController.Instance.CurrentLog;
            BossLog newData = new BossLog(ApplicationController.Instance.CurrentBoss.BossId, currentLog.logName, kills, loot, time);

            //  Add this to the selected log
            ApplicationController.Instance.BossLogs.AddToLog(ApplicationController.Instance.CurrentBoss.BossId, ApplicationController.Instance.CurrentLog.logName, newData, _dropsTab.DropsList);

            //  Close the window
            ClosePopup();
            _dropsTab.LoggedTrip_Reset();
        }
    }

    private void TimeUnitDropdown_OnValueChanged(int value)
    {
        int timeInput;
        int timeOutput;
        string finalTimeValue = "";

        if (string.IsNullOrEmpty(_timeInputField.text))
            timeInput = 0;
        else
            timeInput = int.Parse(_timeInputField.text);

        //  Swapped from minutes to seconds
        if (value == 0)
        {
            timeOutput = timeInput * 60;
            _timeInputField.text = timeOutput.ToString();
            finalTimeValue = (_currentTimeInMins + timeInput).ToString("N0");
        }
        //  Swapped from seconds to minutes
        else if (value == 1)
        {
            timeOutput = timeInput / 60;
            _timeInputField.text = timeOutput.ToString();
            finalTimeValue = (_currentTimeInMins + timeOutput).ToString("N0");
        }

        _timeText.text = $"Time (mins): {_currentTimeInMins.ToString("N0")} -> {finalTimeValue}";
    }
}
