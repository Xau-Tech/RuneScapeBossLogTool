using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleSheetsToUnity;

//  List of item objects used for the item dropdown 
public class ItemList
{
    public ItemList()
    {
        data = new List<Item>();
    }

    //  Properties
    public bool haveRareDropsBeenAdded { private get; set; }

    private List<Item> data;
    private const string sheetID = "13XcVntxy89kaCIQTh9w2FLAJl5z6RtGfvvOEzXVKZxA";
    private const byte COLUMNS = 7;

    //  Get the name of the RareDropTable sheet in Google Docs based on RSVersion option
    private string RareDropTableName()
    {
        string rsVersion = ProgramControl.Options.GetOptionValue(RSVersionOption.Name());

        if (rsVersion.ToLower().CompareTo("rs3") == 0)
            return "Rare Drop Table";
        else
            return "OS Rare Drop Table";
    }

    //  Return a list of item names
    public List<string> GetItemNames()
    {
        List<string> temp = new List<string>();

        foreach (Item item in data)
        {
            temp.Add(item.name);
        }

        return temp;
    }

    //  Returns an item by index
    public Item AtIndex(in int index)
    {
        if(index >= 0 && index < data.Count)
        {
            return data[index];
        }
        else
        {
            return null;
            throw new System.ArgumentOutOfRangeException();
        }
    }

    //  Wrapper for List.Exists
    public bool Exists(string name)
    {
        return data.Exists(item => item.name.CompareTo(name) == 0);
    }

    //Return an item from the list by name
    public Item GetItemByName(in string value)
    {
        foreach(Item item in data)
        {
            if (item.name.CompareTo(value) == 0)
                return item;
        }

        return null;
    }

    //  Add all items dropped by currently selected boss
    public void FillItemList(GstuSpreadSheet ss)
    {
        Item temp;

        if (ss.Cells.Count == 0)
            throw new System.Exception($"There is no data in the {CacheManager.currentBoss} spreadsheet!");

        int numRows = ss.Cells.Count / COLUMNS;

        //  Create and add an item for each row in the sheet
        for (int i = 2; i < (numRows + 1); ++i)
        {
            string name = ss["C" + i].value;
            uint price;

            if (!uint.TryParse(ss["D" + i].value, out price))
                throw new System.Exception($"Value in sheet {CacheManager.currentBoss}, cell D{i} cannot be parsed to a uint!");

            temp = new Item(name, price);

            //  Only add an item if it is not a duplicate
            if (!Exists(temp.name))
                data.Add(temp);
        }

        //  Make sure the boss exists in our data
        BossInfo bInfo;
        if (!DataController.Instance.bossInfoDictionary.TryGetBossInfo(CacheManager.currentBoss, out bInfo))
            throw new System.Exception($"{CacheManager.currentBoss} is not in the dictionary of current bosses!\nItemList.cs::FillItemList");

        //  Check if the boss has access to the rare drop table (a separate list of drops)
        if (bInfo.hasAccessToRareDropTable && !haveRareDropsBeenAdded)
        {
            //  Get the correct drop table sheet name based on rsversion
            string rareDropTable = RareDropTableName();

            GSTU_Search search = new GSTU_Search(sheetID, rareDropTable);
            SpreadsheetManager.ReadPublicSpreadsheet(search, FillItemList);
            haveRareDropsBeenAdded = true;
        }
        else
        {
            data.Sort();
            Print();
            EventManager.Instance.ItemsLoaded();
        }
    }

    //  Wrapper for List.Clear
    public void Clear()
    {
        data.Clear();
    }

    public void Print()
    {
        foreach(Item item in data)
            Debug.Log(item.ToString());
    }
}
