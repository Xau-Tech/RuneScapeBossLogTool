using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Slot that holds a quantity of one type of item
public class ItemSlot
{
    public ItemSlot(Item item, uint quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }

    public uint quantity { get; set; }
    public Item item { get; protected set; }

    public ulong GetValue()
    {
        return (quantity * (ulong)item.Price);
    }

    public string Print()
    {
        return $"ItemQuantity [ Name: {item.name}, Price: {item.Price}\nQuantity: {quantity} ]";
    }

    public override string ToString()
    {
        return $"{item.name}\nQuantity: {quantity}\nValue: {GetValue().ToString("N0")} gp";
    }
}
