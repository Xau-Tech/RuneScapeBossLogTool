using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//  Item class that holds only name and price info of an item
public class Item
{
    public Item()
    {

    }
    public Item(string _name, int _price)
    {
        m_Name = _name;
        m_Price = _price;
    }
    public string Name { get { return m_Name; } set { m_Name = value; } }
    public int Price { get { return m_Price; } set { m_Price = value; } }


    private string m_Name;
    private int m_Price;
}
