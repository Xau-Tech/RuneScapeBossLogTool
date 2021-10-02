using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//  Window used to add data to an existing BossLog
public class AddToLogWindow : MonoBehaviour
{
    public AddToLogWindow()
    {
        Instance = this;
    }

    public static AddToLogWindow Instance;

    public readonly int KILLS = 0;
    public readonly int LOOT = 1;
    public readonly int TIME = 2;

    [SerializeField] private GameObject thisWindow;
    [SerializeField] private InputField killsInputField;
    [SerializeField] private InputField timeInputField;
    [SerializeField] private InputField lootInputField;
    [SerializeField] private Text logText;
    [SerializeField] private Text killsText;
    [SerializeField] private Text lootText;
    [SerializeField] private Text timeText;
    [SerializeField] private Dropdown timeDropdown;
    private ProgramState.states previousState;
    private uint currentKills;
    private ulong currentLoot;
    private uint currentTimeInMins;

    private void OnDisable()
    {
        timeDropdown.onValueChanged.RemoveAllListeners();
    }

    private void TimeDropdown_OnValueChanged(int value)
    {
        int timeInput;
        int timeOutput;
        string finalTimeValue = "";

        if (string.IsNullOrEmpty(timeInputField.text))
            timeInput = 0;
        else
            timeInput = int.Parse(timeInputField.text);

        //  Swapped from minutes to seconds
        if (value == 0)
        {
            timeOutput = timeInput * 60;
            timeInputField.text = timeOutput.ToString();
            finalTimeValue = (currentTimeInMins + timeInput).ToString("N0");
        }
        //  Swapped from seconds to minutes
        else if (value == 1)
        {
            timeOutput = timeInput / 60;
            timeInputField.text = timeOutput.ToString();
            finalTimeValue = (currentTimeInMins + timeOutput).ToString("N0");
        }

        timeText.text = $"Time (mins): {currentTimeInMins.ToString("N0")} -> {finalTimeValue}";
    }

    public void OpenWindow(in ProgramState.states currentState)
    {
        previousState = currentState;

        WindowState.currentState = WindowState.states.AddToLog;
        thisWindow.SetActive(true);

        FillValues();
        timeDropdown.onValueChanged.AddListener(TimeDropdown_OnValueChanged);
        killsInputField.Select();
        StartCoroutine(InputFieldCaretToEnd());
    }

    private IEnumerator InputFieldCaretToEnd()
    {
        yield return new WaitForSeconds(float.MinValue);
        killsInputField.MoveTextEnd(false);
    }

    //  Fills UI values according to respective data
    private void FillValues()
    {
        //  Display current log info
        BossLog currentLog = DataController.Instance.bossLogsDictionary.GetBossLogList(CacheManager.DropsTab.currentBoss.bossID).Find(CacheManager.DropsTab.currentLog);
        currentKills = currentLog.kills;
        currentLoot = currentLog.loot;
        currentTimeInMins = (currentLog.time / 60);
        uint newTimeInMins = (uint)Mathf.FloorToInt((currentLog.time + Timer.timeElapsed) / 60.0f);

        logText.text = $"Log: {CacheManager.DropsTab.currentLog}";
        killsText.text = $"Kills: {currentLog.kills.ToString("N0")} -> {(currentLog.kills + Killcount.killcount).ToString("N0")}";
        lootText.text = $"Loot: {currentLog.loot.ToString("N0")} -> {(currentLog.loot + DataController.Instance.dropList.TotalValue()).ToString("N0")}";
        timeText.text = $"Time (mins): {currentTimeInMins.ToString("N0")} -> {newTimeInMins.ToString("N0")}";

        killsInputField.text = Killcount.killcount.ToString();
        lootInputField.text = DataController.Instance.dropList.TotalValue().ToString();

        //  Timer is not 0 - fill from timer and set dropdown to seconds
        if(Timer.timeElapsed != 0)
        {
            timeInputField.text = Mathf.FloorToInt(Timer.timeElapsed).ToString();
            timeDropdown.value = 0;
        }
        //  Timer is 0 - Set to 0 and set dropdown to minutes
        else
        {
            timeInputField.text = "0";
            timeDropdown.value = 1;
        }
    }

    public void CloseWindow()
    {
        thisWindow.SetActive(false);
        ProgramState.CurrentState = previousState;
    }

    public void UpdateAddedValue(int propertyChanged, int newAddedValue)
    {
        if(propertyChanged == KILLS)
        {
            if (newAddedValue < 0)
            {
                killsInputField.text = "0";
                newAddedValue = 0;
            }

            killsText.text = $"Kills: {currentKills.ToString("N0")} -> {(currentKills + newAddedValue).ToString("N0")}";
        }
        else if(propertyChanged == LOOT)
        {
            if (newAddedValue < 0)
            {
                lootInputField.text = "0";
                newAddedValue = 0;
            }

            lootText.text = $"Loot: {currentLoot.ToString("N0")} -> {(currentLoot + (ulong)newAddedValue).ToString("N0")}";
        }
        else if (propertyChanged == TIME)
        {
            if(newAddedValue < 0)
            {
                timeInputField.text = "0";
                newAddedValue = 0;
            }

            uint addedTimeInMins;

            //  Dropdown is in seconds
            if(timeDropdown.value == 0)
                addedTimeInMins = (uint)(newAddedValue / 60);
            //  Dropdown is in minutes
            else
                addedTimeInMins = (uint)newAddedValue;

            timeText.text = $"Time (mins): {currentTimeInMins.ToString("N0")} -> {(currentTimeInMins + addedTimeInMins).ToString("N0")}";
        }
        else
        {
            Debug.Log("Improper value has been passed to AddToLogWindow:UpdateAddedValue(int, int)");
        }
    }
}
