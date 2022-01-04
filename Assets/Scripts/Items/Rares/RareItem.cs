using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Class for saving rare items inside boss logs
/// </summary>
[System.Serializable]
public class RareItem : IComparable<RareItem>
{
    //  Properties & fields
    public ushort quantity { get; set; }
    public int itemID { get; protected set; }

    //  Constructors
    public RareItem(ushort quantity, int itemId)
    {
        this.quantity = quantity;
        this.itemID = itemId;
    }
    public RareItem(ItemSlot itemSlot)
    {
        if(itemSlot.Quantity > ushort.MaxValue)
        {
            throw new System.Exception($"{itemSlot.Item.ItemName}'s quantity is larger than {ushort.MaxValue}!  Cannot create RareItem from this drop!");
        }
        else
        {
            this.quantity = (ushort)itemSlot.Quantity;
            this.itemID = itemSlot.Item.ItemId;
        }
    }

    //  Methods
    public string GetName(string bossName)
    {
        return RareItemDB.GetRareItemName(bossName, itemID);
    }

    public static RareItem operator +(RareItem firstRare, RareItem secondRare)
    {
        ushort totalQuantity = Convert.ToUInt16(firstRare.quantity + secondRare.quantity);
        return new RareItem(Convert.ToUInt16(totalQuantity), firstRare.itemID);
    }

    public int CompareTo(RareItem other)
    {
        return this.itemID.CompareTo(other.itemID);
    }
}
