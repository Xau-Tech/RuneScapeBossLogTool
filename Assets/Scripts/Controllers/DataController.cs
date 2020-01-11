using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GoogleSheetsToUnity;
using UnityEngine.UI;
using System.Runtime.Serialization;


/*  
 *  Handles all data for the application including bossinfo, item and drop lists, saved boss log data, etc
 */
public class DataController : MonoBehaviour
{
    public static DataController dataController;
    public BossInfoListClass BossInfoList { get { return m_BossInfoList; } }
    public bool HasFinishedLoading { get { return m_HasFinishedInitialLoading; } }
    public DropListClass DropListClass { get { return m_DropList; } }
    public ItemListClass ItemListClass { get { return m_ItemList; } }
    public BossLogsDictionaryClass BossLogsDictionaryClass { get { return m_BossLogsDictionaryClass; } }
    public string CurrentBoss { set { currentBoss = value; } get { return currentBoss; } }
    public string CurrentDropTabLog { set { currentDropTabLog = value; } get { return currentDropTabLog; } }
    public string CurrentLogTabLog { set { currentLogTabLog = value; } get { return currentLogTabLog; } }


    private string sheetID = "13XcVntxy89kaCIQTh9w2FLAJl5z6RtGfvvOEzXVKZxA";
    private bool m_HasFinishedInitialLoading;
    private bool m_HaveRareDropsBeenAdded;
    private string m_BossInfoDataPath;
    private string m_BossLogDataPath;
    private BossInfoListClass m_BossInfoList;
    private DropListClass m_DropList;
    private ItemListClass m_ItemList;
    private BossLogsDictionaryClass m_BossLogsDictionaryClass;
    private string currentBoss, currentDropTabLog, currentLogTabLog;


    private void Awake()
    {
        m_HasFinishedInitialLoading = false;

        if (dataController == null)
        {
            DontDestroyOnLoad(gameObject);
            dataController = this;
        }
        else if (dataController != this)
        {
            Destroy(gameObject);
        }

        m_BossInfoDataPath = Application.streamingAssetsPath + "/bossinfo.txt";
        m_BossLogDataPath = Application.persistentDataPath + "/bosslogs.dat";
        m_DropList = new DropListClass();
        m_ItemList = new ItemListClass();
        m_BossLogsDictionaryClass = new BossLogsDictionaryClass();

        m_HasFinishedInitialLoading = true;
    }

    private void OnEnable()
    {
        EventManager.manager.onBossDropdownValueChanged += FillItemList;
        EventManager.manager.onBossDropdownValueChanged += ClearDrops;
        EventManager.manager.onAddItemButtonClicked += AddDrop;
    }


    private void OnDisable()
    {
        EventManager.manager.onBossDropdownValueChanged -= FillItemList;
        EventManager.manager.onBossDropdownValueChanged -= ClearDrops;
        EventManager.manager.onAddItemButtonClicked -= AddDrop;
    }

    public void Setup()
    {
        LoadBossInfo();
        LoadBossLogData();
    }

    private void AddDrop()
    {
        m_DropList.AddDrop();
    }

    private void ClearDrops()
    {
        m_DropList.ClearDrops();
    }


    //  Run when the BossListDropdown in the Drops tab is changed
    private void FillItemList()
    {
        m_ItemList.ItemList.Clear();
        m_ItemList.HaveRareDropsBeenAdded = false;

        //  Read in the spreadsheet with item data for the new boss
        SpreadsheetManager.ReadPublicSpreadsheet(new GSTU_Search
            (sheetID, CurrentBoss), m_ItemList.FillItemList);

        m_DropList = new DropListClass();
    }


    //  Load BossInfoList data from /StreamingAssets/bossinfo.txt
    public void LoadBossInfo()
    {
        if(File.Exists(m_BossInfoDataPath))
        {
            m_BossInfoList = new BossInfoListClass();
            string[] input;
            BossInfo info;
            string line;

            StreamReader reader = new StreamReader(m_BossInfoDataPath);
            while((line = reader.ReadLine()) != null)
            {
                input = line.Split(';');
                info = new BossInfo(input[0], bool.Parse(input[1]));
                m_BossInfoList.BossInfoList.Add(info);
            }

            reader.Close();
        }
        else
        {
            UIController.uicontroller.m_ItemAmountInputField.text = "file not found";
            Debug.Log("ERROR: bossinfo.dat does not exist or cannot be found.");
        }
    }


    //  Load BossLog data
    public void LoadBossLogData()
    {
        //  File already exists
        if (File.Exists(m_BossLogDataPath))
        {
            //load data
            Debug.Log("file exists");

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(m_BossLogDataPath, FileMode.Open);
            m_BossLogsDictionaryClass = (BossLogsDictionaryClass)bf.Deserialize(file);
            file.Close();
        }
        //  File doesn't exist
        else
        {
            Debug.Log("file created");
            //FileStream file = File.Create(m_BossLogDataPath);
            //file.Close();
            
            //  Populate dictionary with boss names
            for(int i = 0; i < m_BossInfoList.BossInfoList.Count; ++i)
            {
                m_BossLogsDictionaryClass.BossLogsDictionary.Add(m_BossInfoList.BossInfoList[i].BossName
                    , new List<SingleBossLogData>());
            }
        }
    }
}
