using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Extends Item Class
//  Used for drops entered by user to get loot values
public class Drop : Item
{
    public Drop()
    {

    }
    public Drop(Item item, ushort quantity)
    {
        this.name = item.name;
        this.price = item.price;
        this.isRare = item.isRare;
        this.quantity = quantity;
    }

    public ushort quantity;

    public ulong GetValue()
    {
        return (quantity * (ulong)this.price);
    }

    public string Print()
    {
        return $"Drop [ Name: {this.name}, Price: {this.price}\nIsRare: {this.isRare}, Quantity: {quantity} ]";
    }

    public override string ToString()
    {
        return $"{this.name}\nQuantity: {this.quantity}\nValue: {GetValue().ToString("N0")} gp";
    }
}
