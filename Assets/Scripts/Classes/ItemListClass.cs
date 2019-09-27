using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  List of item objects used for the item dropdown 
public class ItemListClass
{
    public ItemListClass()
    {
        m_ItemList = new List<Item>();
    }

    public List<Item> ItemList { get { return m_ItemList; } }


    private List<Item> m_ItemList;


    //  Return a sorted string list of each item name
    public List<string> GetItemNames()
    {
        List<string> temp = new List<string>();

        for (int i = 0; i < m_ItemList.Count; ++i)
        {
            temp.Add(m_ItemList[i].Name);
        }

        temp.Sort();

        return temp;
    }


    //  Check if an item is in the list by name
    public bool IsItemInList(string _name)
    {
        for (int i = 0; i < m_ItemList.Count; ++i)
        {
            if (m_ItemList[i].Name.CompareTo(_name) == 0)
                return true;
        }

        return false;
    }


    //Return an item from the list by name
    public Item GetItemByName(string _value)
    {
        for(int i = 0; i < m_ItemList.Count; ++i)
        {
            if (m_ItemList[i].Name.CompareTo(_value) == 0)
                return m_ItemList[i];
        }

        return null;
    }
}
