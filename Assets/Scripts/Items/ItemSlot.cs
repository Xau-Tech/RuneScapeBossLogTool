using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a quantity of an Item object
/// </summary>
public class ItemSlot
{
    //  Properties & fields
    public uint Quantity { get; set; }
    public Item Item { get; set; }

    //  Constructors
    public ItemSlot()
    {
        this.Quantity = 1;
    }
    public ItemSlot(Item item, uint quantity)
    {
        this.Item = item;
        this.Quantity = quantity;
    }

    //  Methods

    public virtual ulong GetValue()
    {
        return (Quantity * Item.GetValue());
    }

    public string Print()
    {
        return $"ItemQuantity [ Name: {Item.ItemName}, Price: {Item.Price}\nQuantity: {Quantity} ]";
    }

    public override string ToString()
    {
        return $"{Item.ItemName}\nQuantity: {Quantity}\nValue: {GetValue().ToString("N0")} gp";
    }
}
