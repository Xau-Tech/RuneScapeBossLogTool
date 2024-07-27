using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Base class for any item within the game
/// </summary>
public class Item : IComparable<Item>, IValuable
{
    //  Properties & fields
    public int ItemId { get; set; }
    public string ItemName { get; set; }
    public ulong Price { get; set; }

    //  Constructors
    public Item() { }
    public Item(int itemId, string itemName, ulong price)
    {
        this.ItemId = itemId;
        this.ItemName = itemName;
        this.Price = price;
    }

    //  Methods

    public virtual ulong GetValue()
    {
        return Price;
    }

    public int CompareTo(Item otherItem)
    {
        return this.ItemName.ToLower().CompareTo(otherItem.ItemName.ToLower());
    }

    public override string ToString()
    {
        return $"Item [ Name: {ItemName}, Price: {Price}, ItemId: {ItemId} ]";
    }
}

public interface IValuable
{
    ulong GetValue();
}
