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
        //  Check that value has been entered
        if(UIController.uicontroller.m_ItemAmountInputField.text == "")
        {
            Debug.Log("ERROR: No Value entered.");
            return;
        }

        //  Get the item by name from the ItemListDropdown's currently selected value
        Item item = m_ItemList.GetItemByName(UIController.uicontroller.m_ItemListDropdown.options
            [UIController.uicontroller.m_ItemListDropdown.value].text);

        //  Make sure an item has been returned
        if(item != null)
        {
            Drop drop;

            /*
             * If the item isn't in the drop list yet, create a new drop and add it
             * If the item is in the drop list already, simply update that drop object's NumberOfItems value
             */
            if((drop = m_DropList.IsDropAlreadyInList(item.Name)) == null)
            {
                drop = new Drop(item.Name, item.Price, int.Parse(UIController.uicontroller.m_ItemAmountInputField.text));
                m_DropList.DropList.Add(drop);
            }
            else
            {
                drop.NumberOfItems += int.Parse(UIController.uicontroller.m_ItemAmountInputField.text);
            }

            
            //  Generate the drop list UI in the Drops tab
            UIController.uicontroller.DropListController.GenerateList();
        }
        else
        {
            Debug.Log("ERROR: Item returned is equal to null.");
        }
    }


    //  Run when the BossListDropdown in the Drops tab is changed
    private void OnBossListValueChanged()
    {
        m_ItemList.ItemList.Clear();
        m_HaveRareDropsBeenAdded = false;

        //  Read in the spreadsheet with item data for the new boss
        SpreadsheetManager.ReadPublicSpreadsheet(new GSTU_Search
            (sheetID, UIController.uicontroller.GetCurrentBoss()), FillItemList);

        m_DropList = new DropListClass();
    }


    //  Add items to the ItemList from google sheet
    private void FillItemList(GstuSpreadSheet ss)
    {
        Item temp;
        int numRows = ss.Cells.Count / 6;


        //  Create and add an item for each row in the sheet
        for(int i = 2; i < (numRows + 1); ++i)
        {
            temp = new Item(ss["C" + i].value, int.Parse(ss["D" + i].value));

            //  Only add an item if it is not a duplicate
            if (!m_ItemList.IsItemInList(temp.Name))
                m_ItemList.ItemList.Add(temp);
        }


        //  Check if the boss has access to the rare drop table (a separate list of drops)
        if(m_BossInfoList.GetBossInfo(UIController.uicontroller.GetCurrentBoss()).HasAccessToRareDropTable
            && !m_HaveRareDropsBeenAdded)
        {
            GSTU_Search search = new GSTU_Search(sheetID, "Rare Drop Table");

            SpreadsheetManager.ReadPublicSpreadsheet(search, FillItemList);

            m_HaveRareDropsBeenAdded = true;
        }
        else
            UIController.uicontroller.PopulateItemDropdown();
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
