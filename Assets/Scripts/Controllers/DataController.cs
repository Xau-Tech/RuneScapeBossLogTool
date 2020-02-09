using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GoogleSheetsToUnity;
using UnityEngine.UI;
using System.Runtime.Serialization;
using System;

/*  
 *  Handles all data for the application including bossinfo, item and drop lists, saved boss log data, etc
 */
public class DataController : MonoBehaviour
{
    public static DataController Instance;

    public BossInfoList BossInfoList { get { return m_BossInfoList; } }
    public DropList DropList { get { return m_DropList; } }
    public ItemList ItemList { get { return m_ItemList; } }
    public BossLogsDictionary BossLogsDictionary { get { return m_BossLogsDictionary; } }
    public string CurrentBoss { set { currentBoss = value; } get { return currentBoss; } }
    public string CurrentDropTabLog { set { currentDropTabLog = value; } get { return currentDropTabLog; } }
    public string CurrentLogTabLog { set { currentLogTabLog = value; } get { return currentLogTabLog; } }
    public bool HasUnsavedData { set { m_HasUnsavedData = value; } get { return m_HasUnsavedData; } }

    private bool m_IsBossInfoLoaded;
    private string sheetID = "13XcVntxy89kaCIQTh9w2FLAJl5z6RtGfvvOEzXVKZxA";
    private string bossInfoSheet = "BossInfo";
    private bool m_HaveRareDropsBeenAdded;
    private string m_BossLogDataPath;
    private BossInfoList m_BossInfoList;
    private DropList m_DropList;
    private ItemList m_ItemList;
    private BossLogsDictionary m_BossLogsDictionary;
    private string currentBoss, currentDropTabLog, currentLogTabLog;
    private bool m_HasUnsavedData;


    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        //  Test save file in editor
        if(Application.isEditor)
        {
            m_BossLogDataPath = Application.persistentDataPath + "/testData.dat";
        }
        //  Real save file
        else
        {
            m_BossLogDataPath = Application.persistentDataPath + "/bosslogs.dat";
        }

        m_DropList = new DropList();
        m_ItemList = new ItemList();
        m_BossLogsDictionary = new BossLogsDictionary();
        m_IsBossInfoLoaded = false;
        m_HasUnsavedData = false;
    }

    private void OnEnable()
    {
        EventManager.Instance.onBossDropdownValueChanged += FillItemList;
        EventManager.Instance.onBossDropdownValueChanged += ClearDrops;
        EventManager.Instance.onAddItemButtonClicked += AddDrop;
    }


    private void OnDisable()
    {
        EventManager.Instance.onBossDropdownValueChanged -= FillItemList;
        EventManager.Instance.onBossDropdownValueChanged -= ClearDrops;
        EventManager.Instance.onAddItemButtonClicked -= AddDrop;
    }

    public void Setup()
    {
        LoadBossInfo();
        StartCoroutine(LoadBossLogData());
    }

    private void AddDrop(string _item, int _amount)
    {
        m_DropList.AddDrop(_item, _amount);
    }

    private void ClearDrops()
    {
        //  Only perform this action if we are in the drops tab or the addtolog window
        if (ProgramState.CurrentState == ProgramState.states.Drops ||
                ProgramState.CurrentState == ProgramState.states.AddToLog)
        {
            m_DropList.ClearDrops();
        }
    }


    //  Run when the BossListDropdown in the Drops tab is changed
    private void FillItemList()
    {
        //  Only perform this action if we are in the drops tab
        if (ProgramState.CurrentState != ProgramState.states.Drops)
            return;

        m_ItemList.data.Clear();
        m_ItemList.HaveRareDropsBeenAdded = false;

        //  Read in the spreadsheet with item data for the new boss
        SpreadsheetManager.ReadPublicSpreadsheet(new GSTU_Search
            (sheetID, CurrentBoss), m_ItemList.FillItemList);

        m_DropList = new DropList();
    }

    //  Load BossInfoList data from Google doc
    private void LoadBossInfo()
    {
        m_BossInfoList = new BossInfoList();

        SpreadsheetManager.ReadPublicSpreadsheet(new GSTU_Search
            (sheetID, bossInfoSheet), FillBossInfoList);
    }

    private void FillBossInfoList(GstuSpreadSheet ss)
    {
        Debug.Log("fillboss in");
        BossInfo bossInfo;
        int numRows = ss.Cells.Count / 2;

        //  Create and add a boss for each row in the sheet
        for (int i = 2; i < (numRows + 1); ++i)
        {
            //temp = new Item(ss["C" + i].value, int.Parse(ss["D" + i].value));
            bossInfo = new BossInfo(ss["A" + i].value, bool.Parse(ss["B" + i].value));
            m_BossInfoList.data.Add(bossInfo);
        }
        Debug.Log("fillboss out");
        EventManager.Instance.BossInfoLoaded();

        //UIController.Instance.OnToolbarDropButtonClicked();
        //EventManager.Instance.BossDropdownValueChanged();
    }

    //  Load BossLog data
    public IEnumerator LoadBossLogData()
    {
        while (currentBoss == null)
            yield return null;


        Debug.Log("loadlog in");

        //  File already exists
        if (File.Exists(m_BossLogDataPath))
        {
            //load data
            Debug.Log("file exists");

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(m_BossLogDataPath, FileMode.Open);
            m_BossLogsDictionary = (BossLogsDictionary)bf.Deserialize(file);

            Debug.Log("file loaded");

            file.Close();

            CheckAndAddNewBosses();
        }
        //  File doesn't exist
        else
        {
            Debug.Log("file created");
            FileStream file = File.Create(m_BossLogDataPath);
            file.Close();
            
            //  Populate dictionary with boss names
            for(int i = 0; i < m_BossInfoList.data.Count; ++i)
            {
                m_BossLogsDictionary.data.Add(m_BossInfoList.data[i].BossName
                    , new List<BossLog>());
            }

            SaveBossLogData();
        }
        Debug.Log("loadlog out");

        //  All data should now be loaded so we can do our final setup
        ProgramControl.Instance.LateSetup();
    }

    private void CheckAndAddNewBosses()
    {
        //  Go through each boss in the info list
        for(int i = 0; i < BossInfoList.data.Count; ++i)
        {
            //  Check if the boss hasn't been added to the dictionary
            if (!BossLogsDictionary.data.ContainsKey(BossInfoList.data[i].BossName))
            {
                //  Add the name and a new list to the dictionary
                Debug.Log(BossInfoList.data[i].BossName + " added");
                m_BossLogsDictionary.data.Add(BossInfoList.data[i].BossName
                    , new List<BossLog>());
            }
        }
    }

    public void SaveBossLogData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(m_BossLogDataPath);
        bf.Serialize(file, m_BossLogsDictionary);
        file.Close();
        m_HasUnsavedData = false;
        UIController.Instance.UpdateSaveText();
    }
}
