using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GoogleSheetsToUnity;
using UnityEngine.UI;


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


    private string sheetID = "13XcVntxy89kaCIQTh9w2FLAJl5z6RtGfvvOEzXVKZxA";
    private bool m_HasFinishedInitialLoading;
    private bool m_HaveRareDropsBeenAdded;
    private string m_BossInfoDataPath;
    private BossInfoListClass m_BossInfoList;
    private DropListClass m_DropList;
    private ItemListClass m_ItemList;


    private void Awake()
    {
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
        m_DropList = new DropListClass();
        m_ItemList = new ItemListClass();
        m_HasFinishedInitialLoading = false;

        Load();

        m_HasFinishedInitialLoading = true;
    }


    private void OnEnable()
    {
        EventManager.OnBossListDropdownChanged += OnBossListValueChanged;
        EventManager.OnAddButtonClicked += AddButtonClicked;
    }


    private void OnDisable()
    {
        EventManager.OnBossListDropdownChanged -= OnBossListValueChanged;
        EventManager.OnAddButtonClicked -= AddButtonClicked;
    }


    //  Add button in Drops tab clicked
    private void AddButtonClicked()
    {
        m_DropList.AddDrop();
    }


    //  Run when the BossListDropdown in the Drops tab is changed
    public void OnBossListValueChanged()
    {
        m_ItemList.ItemList.Clear();
        m_ItemList.HaveRareDropsBeenAdded = false;

        //  Read in the spreadsheet with item data for the new boss
        SpreadsheetManager.ReadPublicSpreadsheet(new GSTU_Search
            (sheetID, UIController.uicontroller.GetCurrentBoss()), m_ItemList.FillItemList);

        m_DropList = new DropListClass();
    }



    private void Load()
    {
        LoadBossInfo();
    }


    //  Load BossInfoList data from /StreamingAssets/bossinfo.txt
    private void LoadBossInfo()
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
}
