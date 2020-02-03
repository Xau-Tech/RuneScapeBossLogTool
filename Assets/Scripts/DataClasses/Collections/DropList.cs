using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//  List of drop objects used for the list of player-entered drops
public class DropList
{
    public DropList()
    {
        m_Data = new List<Drop>();
    }


    //  Properties
    public List<Drop> data { get { return m_Data; } }


    private List<Drop> m_Data;


    //  Add an item to the list
    //  Called when Add is clicked in the Drops tab
    public void AddDrop(string _item, int _amount)
    {
        //  Get the item by name from the ItemListDropdown's currently selected value
        Item item = DataController.Instance.ItemList.GetItemByName(_item);

        //  Make sure an item has been returned
        if (item != null)
        {
            Drop drop;
            TryGetDrop(item.Name, out drop);

            /*
             * If the item isn't in the drop list yet, create a new drop and add it
             * If the item is in the drop list already, simply update that drop object's NumberOfItems value
             */
            if (drop == null)
            {
                drop = new Drop(item, _amount);
                data.Add(drop);
            }
            else
            {
                drop.NumberOfItems += _amount;
                //  Remove the drop from list if new the new NumberOfItems is 0 or below
                if (drop.NumberOfItems <= 0)
                    data.Remove(drop);
            }


            //  Generate the drop list UI in the Drops tab
            UIController.Instance.DropListController.GenerateList();
        }
        else
        {
            EventManager.Instance.ErrorOpen("ERROR: Item returned is equal to null.");
        }
    }

    //  Clear droplist
    public void ClearDrops()
    {
        m_Data = new List<Drop>();
        UIController.Instance.DropListController.GenerateList();
    }


    //  Check if drop is in the list by name and return that drop if true
    public void TryGetDrop(string _name, out Drop d)
    {
        foreach(Drop drop in data)
        {
            if (drop.Name.CompareTo(_name) == 0)
            {
                d = drop;
                return;
            }
        }

        d = null;
    }


    //  Return the total value of all drops
    public int GetTotalValue()
    {
        if (data == null || data.Count == 0)
            return 0;

        int value = 0;

        for(int i = 0; i < data.Count; ++i)
        {
            value += (data[i].Price * data[i].NumberOfItems);
        }

        return value;
    }
}
