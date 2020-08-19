using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//  Class for items that will be dropped by bosses, used by players, etc
public class Item : ScriptableObject, IComparable<Item>
{
    public Item() { }
    public Item(int itemID, string name, uint price)
    {
        this.itemID = itemID;
        this.name = name;
        this.price = price;
    }

    public string ItemName { get { return name; } set { name = value; } }
    public uint Price { get { return price; } set { price = value; } }
    public int ItemID { get { return itemID; } set { itemID = value; } }

    [SerializeField] private string itemName;
    [SerializeField] private uint price;
    [SerializeField] private int itemID;

    public override string ToString()
    {
        return $"Item [ Name: {name}, Price: {price}, ItemID: {itemID} ]";
    }

    //  IComparable interface; sort by name
    public int CompareTo(Item otherItem)
    {
        return this.ItemName.ToLower().CompareTo(otherItem.ItemName.ToLower());
    }
}
