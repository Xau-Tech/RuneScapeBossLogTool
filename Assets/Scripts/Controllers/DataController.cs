using UnityEngine;
using GoogleSheetsToUnity;

//  Handles all data for the application including bossinfo, item and drop lists, saved boss log data, etc
public class DataController : MonoBehaviour
{
    private static DataController instance = new DataController();
    public static DataController Instance { get { return instance; } }

    public BossInfoDictionary bossInfoDictionary { get; private set; }
    public ItemList itemList { get; private set; }
    public ItemSlotList dropList { get; private set; }
    public BossLogsDictionary bossLogsDictionary { get; private set; }
    public SetupDictionary setupDictionary { get; private set; }
    public SetupItemsDB setupItemsDB { get; private set; }

    private bool isBossInfoLoaded;
    private bool haveRareDropsBeenAdded;
    private string bossLogDataPath;

    private readonly string sheetID = "13XcVntxy89kaCIQTh9w2FLAJl5z6RtGfvvOEzXVKZxA";
    private readonly byte bossSheetColumns = 4;

    public DataController()
    {
        //  Sub to any events
        EventManager.Instance.onBossDropdownValueChanged += FillItemList;
        EventManager.Instance.onLogUpdated += ClearDropList;
        EventManager.Instance.onBossDropdownValueChanged += ClearDropList;
    }
    ~DataController()
    {
        //  Unsub to any events
        //EventManager.Instance.onRSVersionChanged -= LoadBossInfo;
        EventManager.Instance.onBossDropdownValueChanged -= FillItemList;
        EventManager.Instance.onLogUpdated -= ClearDropList;
        EventManager.Instance.onBossDropdownValueChanged -= ClearDropList;
    }

    public void Setup()
    {
        //  Set state to none
        DataState.CurrentState = DataState.states.None;

        //  Instantiate data
        itemList = new ItemList();
        dropList = new ItemSlotList();
        bossLogsDictionary = new BossLogsDictionary();
        bossInfoDictionary = new BossInfoDictionary();
        setupDictionary = new SetupDictionary();
        isBossInfoLoaded = false;

        LoadData();
    }

    private void LoadData()
    {
        //  Load rare item data
        RareItemDB.Load(ProgramControl.Options.GetOption(OptionData.OptionNames.RSVersion) as RSVersionOption);

        //  Load boss info
        bossInfoDictionary.Load(ProgramControl.Options.GetOptionValue(RSVersionOption.Name()));

        //  Load bosslog dictionary
        bossLogsDictionary.Load(bossInfoDictionary.GetBossIDs());

        //  Setup data
        SetupTheSetupData();

        //  Trigger events for after all initial required data has been loaded
        EventManager.Instance.DataLoaded();
    }

    //  Setup for the setup tab
    private void SetupTheSetupData()
    {
        //  Create an empty setup
        CacheManager.SetupTab.Setup = new SetupMVC(GameObject.Find("SetupPanel").GetComponent<SetupView>());

        //  Check if a username is stored and set+load from that if so
        if (PlayerPrefs.HasKey(PlayerPrefKeys.GetKeyName(PlayerPrefKeys.KeyNamesEnum.Username)))
            CacheManager.SetupTab.Setup.LoadNewPlayerStatsAsync(PlayerPrefs.GetString(PlayerPrefKeys.GetKeyName(PlayerPrefKeys.KeyNamesEnum.Username)));
    }

    private void ClearDropList()
    {
        dropList.Clear();
    }

    //  Fill our item list with data from Google Doc
    private void FillItemList()
    {
        //  Do not re-fill the item list if user is not loading data AND has the BossSync option turned off
        if ((ProgramState.CurrentState != ProgramState.states.Drops) && !bool.Parse(ProgramControl.Options.GetOptionValue(BossSyncOption.Name())))
        {
            Debug.Log($"Not filling item list");
            return;
        }
        Debug.Log("filling item list");
        itemList.Clear();
        itemList.haveRareDropsBeenAdded = false;

        //  Read in the spreadsheet with item data for the new boss
        SpreadsheetManager.ReadPublicSpreadsheet(new GSTU_Search
            (sheetID, (CacheManager.currentBoss.bossName + " " + ProgramControl.Options.GetOptionValue(RSVersionOption.Name()))), itemList.FillItemList);
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
