﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//  Holds data for rare items that are saved with the user's log information
[System.Serializable]
public class RareItem : IComparable<RareItem>
{
    public RareItem(int itemID, ushort quantity)
    {
        this.itemID = itemID;
        this.quantity = quantity;
    }
    public RareItem(ItemSlot itemSlot)
    {
        if (itemSlot.quantity > ushort.MaxValue)
            throw new System.Exception($"{itemSlot.item.name}'s quantity is larger than {ushort.MaxValue}!  Cannot create RareItem from this drop!");
        else
        {
            this.quantity = (ushort)itemSlot.quantity;
            this.itemID = itemSlot.item.itemID;
        }
    }

    public ushort quantity { get; set; }
    public int itemID { get; protected set; }

    public string GetName()
    {
        return RareItemDB.GetRareItemName(CacheManager.currentBoss, itemID);
    }

    //  IComparable interface
    public int CompareTo(RareItem other)
    {
        return this.GetName().ToLower().CompareTo(other.GetName().ToLower());
    }
}
