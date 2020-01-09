using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleSheetsToUnity;

//  List of item objects used for the item dropdown 
public class ItemListClass
{
    public ItemListClass()
    {
        m_ItemList = new List<Item>();
    }


    //  Properties
    public List<Item> ItemList { get { return m_ItemList; } }
    public bool HaveRareDropsBeenAdded { set { m_HaveRareDropsBeenAdded = value; } }


    private List<Item> m_ItemList;
    private bool m_HaveRareDropsBeenAdded;
    private string sheetID = "13XcVntxy89kaCIQTh9w2FLAJl5z6RtGfvvOEzXVKZxA";


    //  Return a sorted string list of each item name
    public List<string> GetItemNames()
    {
        List<string> temp = new List<string>();

        foreach (Item item in m_ItemList)
        {
            temp.Add(item.Name);
        }

        temp.Sort();

        return temp;
    }


    //  Check if an item is in the list by name
    public bool IsItemInList(string _name)
    {
        foreach (Item item in m_ItemList)
        {
            if (item.Name.CompareTo(_name) == 0)
                return true;
        }

        return false;
    }


    //Return an item from the list by name
    public Item GetItemByName(string _value)
    {
        foreach(Item item in m_ItemList)
        {
            if (item.Name.CompareTo(_value) == 0)
                return item;
        }

        return null;
    }


    //  Add all items dropped by currently selected boss
    public void FillItemList(GstuSpreadSheet ss)
    {
        Item temp;
        int numRows = ss.Cells.Count / 6;


        //  Create and add an item for each row in the sheet
        for (int i = 2; i < (numRows + 1); ++i)
        {
            temp = new Item(ss["C" + i].value, int.Parse(ss["D" + i].value));

            //  Only add an item if it is not a duplicate
            if (!IsItemInList(temp.Name))
                m_ItemList.Add(temp);
        }


        //  Check if the boss has access to the rare drop table (a separate list of drops)
        if (DataController.dataController.BossInfoList.GetBossInfo(UIController.uicontroller.GetCurrentBoss()).HasAccessToRareDropTable
            && !m_HaveRareDropsBeenAdded)
        {
            GSTU_Search search = new GSTU_Search(sheetID, "Rare Drop Table");

            SpreadsheetManager.ReadPublicSpreadsheet(search, FillItemList);

            m_HaveRareDropsBeenAdded = true;
        }
        else
            EventManager.manager.ItemsLoaded();
    }
}
