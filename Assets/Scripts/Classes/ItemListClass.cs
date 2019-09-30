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


    //  Properties
    public List<Item> ItemList { get { return m_ItemList; } }


    private List<Item> m_ItemList;


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
}
