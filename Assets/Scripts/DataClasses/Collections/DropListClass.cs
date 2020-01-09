using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//  List of drop objects used for the list of player-entered drops
public class DropListClass
{
    public DropListClass()
    {
        m_DropList = new List<Drop>();
    }


    //  Properties
    public List<Drop> DropList { get { return m_DropList; } }


    private List<Drop> m_DropList;


    //  Add an item to the list
    //  Called when Add is clicked in the Drops tab
    public void AddDrop()
    {
        //  Check that value has been entered
        if (UIController.uicontroller.m_ItemAmountInputField.text == "")
        {
            Debug.Log("ERROR: No Value entered.");
            return;
        }

        //  Get the item by name from the ItemListDropdown's currently selected value
        Item item = DataController.dataController.ItemListClass.GetItemByName(UIController.uicontroller.m_ItemListDropdown.options
            [UIController.uicontroller.m_ItemListDropdown.value].text);

        //  Make sure an item has been returned
        if (item != null)
        {
            Drop drop;

            /*
             * If the item isn't in the drop list yet, create a new drop and add it
             * If the item is in the drop list already, simply update that drop object's NumberOfItems value
             */
            if ((drop = IsDropAlreadyInList(item.Name)) == null)
            {
                drop = new Drop(item.Name, item.Price, int.Parse(UIController.uicontroller.m_ItemAmountInputField.text));
                //  Only add new item if entered number is greater than 0
                if (drop.NumberOfItems > 0)
                    m_DropList.Add(drop);
            }
            else
            {
                drop.NumberOfItems += int.Parse(UIController.uicontroller.m_ItemAmountInputField.text);
                //  Remove the drop from list if new the new NumberOfItems is 0 or below
                if (drop.NumberOfItems <= 0)
                {
                    m_DropList.Remove(drop);
                }
            }


            //  Generate the drop list UI in the Drops tab
            UIController.uicontroller.DropListController.GenerateList();
        }
        else
        {
            Debug.Log("ERROR: Item returned is equal to null.");
        }
    }

    //  Clear droplist
    public void ClearDrops()
    {
        m_DropList = new List<Drop>();
        UIController.uicontroller.DropListController.GenerateList();
    }


    //  Check if drop is in the list by name and return that drop if true
    public Drop IsDropAlreadyInList(string _name)
    {
        foreach(Drop drop in m_DropList)
        {
            if (drop.Name.CompareTo(_name) == 0)
                return drop;
        }


        return null;
    }


    //  Return the total value of all drops
    public int GetTotalValue()
    {
        int value = 0;

        for(int i = 0; i < m_DropList.Count; ++i)
        {
            value += (m_DropList[i].Price * m_DropList[i].NumberOfItems);
        }

        return value;
    }
}
