using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//  Holds data for rare items that are saved with the user's log information
[System.Serializable]
public class RareItem : IComparable<RareItem>
{
    public string itemName { get; }
    public ushort quantity { get; set; }

    public RareItem() { }
    public RareItem(string itemName, ushort quantity)
    {
        this.itemName = itemName;
        this.quantity = quantity;
    }
    public RareItem(Drop drop)
    {
        if (drop.quantity > ushort.MaxValue)
            throw new System.Exception($"{drop.name}'s quantity is larger than {ushort.MaxValue}!  Cannot create RareItem from this drop!");
        else
        {
            this.quantity = (ushort)drop.quantity;
            this.itemName = drop.name;
        }
    }

    //  IComparable interface
    public int CompareTo(RareItem other)
    {
        return this.itemName.ToLower().CompareTo(other.itemName.ToLower());
    }
}
