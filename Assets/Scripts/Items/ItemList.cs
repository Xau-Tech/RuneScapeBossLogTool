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

    //  Return a list of item names
    public List<string> GetItemNames()
    {
        List<string> temp = new List<string>();

        foreach (Item item in data)
            temp.Add(item.itemName);

        return temp;
    }

    //  Returns an item by index
    public Item AtIndex(in int index)
    {
        if(index >= 0 && index < data.Count)
            return data[index];
        else
            throw new System.ArgumentOutOfRangeException();
    }

    //  Wrapper for List.Exists
    public bool Exists(string name)
    {
        return data.Exists(item => item.itemName.CompareTo(name) == 0);
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
            int itemID;

            if (!uint.TryParse(ss["D" + i].value, out price))
            {
                Debug.Log($"Value in sheet {CacheManager.currentBoss.bossName}, cell D{i} cannot be parsed to a uint!");
                continue;
            }
            if (!int.TryParse(ss["B" + i].value, out itemID))
            {
                Debug.Log($"Value in sheet {CacheManager.currentBoss.bossName}, cell B{i} cannot be parsed to an int!");
                continue;
            }

            temp = new Item(itemID, name, price);

            //  Only add an item if it is not a duplicate
            if (!Exists(temp.itemName))
                data.Add(temp);
            else
                Debug.Log($"Not adding Item [ {temp.ToString()} ]");
        }

        //  Make sure the boss exists in our data
        BossInfo bInfo;
        if ((bInfo = DataController.Instance.bossInfoDictionary.GetBossByID(CacheManager.currentBoss.bossID)) == null)
            Debug.Log($"{CacheManager.currentBoss} is not in the dictionary of current bosses!\nItemList.cs::FillItemList");

        //  Check if the boss has access to the rare drop table (a separate list of drops)
        if (bInfo.hasAccessToRareDropTable && !haveRareDropsBeenAdded)
        {
            Debug.Log("adding rares");
            //  Get the correct drop table sheet name based on rsversion
            string rareDropTable = Options.RareDropTableName();

            GSTU_Search search = new GSTU_Search(sheetID, rareDropTable);
            SpreadsheetManager.ReadPublicSpreadsheet(search, FillItemList);
            haveRareDropsBeenAdded = true;
        }
        else
        {
            Debug.Log("done adding items");
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

    //  Print list to debug
    public void Print()
    {
        string output = "ItemList:";

        foreach (Item item in data)
            output += $"\n{item.ToString()}";

        Debug.Log(output);
    }
}
