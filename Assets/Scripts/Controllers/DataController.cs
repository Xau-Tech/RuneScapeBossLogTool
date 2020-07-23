using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GoogleSheetsToUnity;
using System;

/*  
 *  Handles all data for the application including bossinfo, item and drop lists, saved boss log data, etc
 */
public class DataController : MonoBehaviour
{
    private static DataController instance = new DataController();
    public static DataController Instance { get { return instance; } }

    public BossInfoDictionary bossInfoDictionary { get; private set; }
    public ItemList itemList { get; private set; }
    public DropList dropList { get; private set; }
    public BossLogsDictionary bossLogsDictionary { get; private set; }

    private bool isBossInfoLoaded;
    private bool haveRareDropsBeenAdded;
    private string bossLogDataPath;

    private readonly string sheetID = "13XcVntxy89kaCIQTh9w2FLAJl5z6RtGfvvOEzXVKZxA";
    private readonly byte bossSheetColumns = 4;

    ~DataController()
    {
        //  Unsub to any events
        EventManager.Instance.onRSVersionChanged -= LoadBossInfo;
        EventManager.Instance.onBossDropdownValueChanged -= FillItemList;
        EventManager.Instance.onBossInfoLoaded -= LoadBossLogData;
        EventManager.Instance.onLogUpdated -= ClearDropList;
        EventManager.Instance.onBossDropdownValueChanged -= ClearDropList;
    }

    public void Setup()
    {
        //  Set state to none
        DataState.CurrentState = DataState.states.None;

        //  Sub to any events
        //EventManager.Instance.onRSVersionChanged += LoadBossInfo;
        EventManager.Instance.onBossDropdownValueChanged += FillItemList;
        EventManager.Instance.onBossInfoLoaded += LoadBossLogData;
        EventManager.Instance.onLogUpdated += ClearDropList;
        EventManager.Instance.onBossDropdownValueChanged += ClearDropList;

        //  Instantiate data
        itemList = new ItemList();
        dropList = new DropList();
        bossLogsDictionary = new BossLogsDictionary();
        isBossInfoLoaded = false;
        
        LoadBossInfo();
    }

    private void ClearDropList()
    {
        dropList.Clear();
    }

    //  Fill our item list with data from Google Doc
    private void FillItemList()
    {
        //  Do not re-fill the item list if user is not loading data AND has the BossSync option turned off (false)
        if ((ProgramState.CurrentState != ProgramState.states.Drops) && !bool.Parse(ProgramControl.Options.GetOptionValue(BossSyncOption.Name())))
        {
            Debug.Log($"Not filling item list");
            return;
        }

        itemList.Clear();
        itemList.haveRareDropsBeenAdded = false;

        //  Read in the spreadsheet with item data for the new boss
        SpreadsheetManager.ReadPublicSpreadsheet(new GSTU_Search
            (sheetID, CacheManager.currentBoss), itemList.FillItemList);
    }

    //  Load BossInfoList data from Google doc
    private void LoadBossInfo()
    {
        bossInfoDictionary = new BossInfoDictionary();
        bossLogsDictionary = new BossLogsDictionary();
        string rsVersion = ProgramControl.Options.GetOptionValue(RSVersionOption.Name());

        SpreadsheetManager.ReadPublicSpreadsheet(new GSTU_Search
            (sheetID, BossInfoDictionary.BossInfoFile(rsVersion)), bossInfoDictionary.Load);
    }

    //  Load BossLog data
    public void LoadBossLogData()
    {
        bossLogsDictionary.Load(bossInfoDictionary.GetBossNames());
    }

    //  Save our log data to file
    public void SaveBossLogData()
    {
        bossLogsDictionary.Save();
    }
}

public class DataState
{
    public enum states { None, Saving, Loading };

    private static states currentState;

    public static states CurrentState
    {
        get { return currentState; }
        set
        {
            if (currentState != states.Saving && currentState != states.Loading)
            {
                //  New state is either saving or loading
                if (value == states.Saving || value == states.Loading)
                {
                    //  Turn on input restrict UI 
                    currentState = value;
                    UIController.Instance.InputRestrictStart($"{value.ToString()}...");
                }
            }
            //  Program is in saving, loading, or setup state
            else if (currentState == states.Saving || currentState == states.Loading)
            {
                //  New state is not saving or loading or setup
                if (value != states.Saving && value != states.Loading)
                {
                    //  Turn off input restrict UI
                    currentState = value;
                    UIController.Instance.InputRestrictEnd();
                }
            }
            else
            {
                currentState = value;
            }

            Debug.Log($"New data state is {value.ToString()}");
        }
    }
}
