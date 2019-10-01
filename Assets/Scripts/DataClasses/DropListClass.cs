using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
