using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//  Class for items that will be dropped by bosses, used by players, etc
public class Item : ScriptableObject, IComparable<Item>
{
    public Item(int itemID, string name, uint price)
    {
        this.itemID = itemID;
        this.name = name;
        this.price = price;
        isStackable = true;
    }
    public Item(int itemID, string name, uint price, bool isStackable)
    {
        this.itemID = itemID;
        this.name = name;
        this.price = price;
        this.isStackable = isStackable;
    }

    public new string name { get; protected set; }
    public uint price { get; protected set; }
    public int itemID { get; protected set; }
    public bool isStackable { get; protected set; }

    public override string ToString()
    {
        return $"Item [ Name: {name}, Price: {price} ]";
    }

    //  IComparable interface; sort by name
    public int CompareTo(Item otherItem)
    {
        return this.name.ToLower().CompareTo(otherItem.name.ToLower());
    }
}
