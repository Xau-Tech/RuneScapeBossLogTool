using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleSheetsToUnity;

/// <summary>
/// List of Item objects used for keeping track of what the player can receive from their current pvm encounter
/// </summary>
public class ItemList
{
    //  Properties & fields
    public bool HaveRareDropsBeenAdded { private get; set; }

    private List<Item> _data;
    private readonly byte _COLUMNS = 7;

    //  Constructor
    public ItemList()
    {
        _data = new List<Item>();
    }

    //  Methods

    public List<string> ItemNames()
    {
        List<string> temp = new List<string>();
        foreach(Item item in _data)
        {
            temp.Add(item.ItemName);
        }
        return temp;
    }

    public Item AtIndex(int index)
    {
        if (index < 0 || index >= _data.Count)
            throw new System.ArgumentOutOfRangeException();
        else
            return _data[index];
    }

    public bool Exists(string name)
    {
        return _data.Exists(item => item.ItemName.CompareTo(name) == 0);
    }

    /// <summary>
    /// Callback function for loading items from google doc sheet
    /// </summary>
    /// <param name="ss"></param>
    public void FillItemList(GstuSpreadSheet ss)
    {
        Item temp;

        if (ss.Cells.Count == 0)
            throw new System.Exception($"There is no data in the {ApplicationController.Instance.CurrentBoss.BossName} spreadsheet!");

        int numRows = ss.Cells.Count / _COLUMNS;

        //  Create and add an item for each row in the sheet
        for(int i = 2; i < (numRows + 1); ++i)
        {
            string name = ss["C" + i].value;
            Debug.Log($"Item name: {name}");
            uint price;
            int itemId;
            
            if(!uint.TryParse(ss["D" + i].value, out price))
            {
                Debug.Log($"Value in sheet {ApplicationController.Instance.CurrentBoss.BossName}, cell D{i} cannot be parsed to a uint!");
                continue;
            }
            if (!int.TryParse(ss["B" + i].value, out itemId))
            {
                Debug.Log($"Value in sheet {ApplicationController.Instance.CurrentBoss.BossName}, cell B{i} cannot be parsed to an int!");
                continue;
            }

            temp = new Item(itemId, name, price);

            //  Only add an item if it is not a duplicate
            if (!Exists(temp.ItemName))
                _data.Add(temp);
            else
                Debug.Log($"Not adding Item [ {temp.ToString()} ]");
        }

        //  Make sure the boss exists in our data
        BossInfo bossInfo;
        if ((bossInfo = ApplicationController.Instance.BossInfo.GetBoss(ApplicationController.Instance.CurrentBoss.BossId)) == null)
            Debug.Log($"{ApplicationController.Instance.CurrentBoss.BossName} is not in the dictionary of current bosses!");

        //  Check if the boss has access to the rare drop table
        if(bossInfo.HasAccessToRareDropTable && !HaveRareDropsBeenAdded)
        {
            string rdtName = Options.RareDropTableName();
            Debug.Log("Adding rare drop table items!");
            GSTU_Search search = new GSTU_Search(ApplicationController.SHEETID, rdtName);
            HaveRareDropsBeenAdded = true;
            SpreadsheetManager.ReadPublicSpreadsheet(search, FillItemList);
        }
        else
        {
            HaveRareDropsBeenAdded = false;
            _data.Sort();
            Print();
            EventManager.Instance.BossItemsLoaded();
        }
    }

    public void Clear()
    {
        _data.Clear();
    }

    public void Print()
    {
        string message = "ItemList:";
        foreach(Item item in _data)
        {
            message += $"\n{item.ToString()}";
        }
        Debug.Log(message);
    }
}
