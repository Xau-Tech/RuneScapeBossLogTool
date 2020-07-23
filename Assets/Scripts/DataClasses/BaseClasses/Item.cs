using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//  Class for items that will be dropped by bosses, used by players, etc
public class Item : IComparable<Item>
{
    public Item()
    {

    }
    public Item(string name, uint price, bool isRare)
    {
        this.name = name;
        this.price = price;
        this.isRare = isRare;
    }

    public string name { get; protected set; }
    public uint price { get; protected set; }
    public bool isRare { get; protected set; }

    public override string ToString()
    {
        return $"Item [ Name: {name}, Price: {price}, IsRare: {isRare} ]";
    }

    //  IComparable interface
    public int CompareTo(Item otherItem)
    {
        return this.name.ToLower().CompareTo(otherItem.name.ToLower());
    }
}
