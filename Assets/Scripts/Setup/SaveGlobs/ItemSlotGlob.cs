using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemSlotGlob
{
    //  Properties & fields

    public int itemID;
    public uint quantity;

    //  Constructor

    public ItemSlotGlob(ItemSlot itemSlot)
    {
        this.itemID = itemSlot.Item.ItemId;
        this.quantity = itemSlot.Quantity;
    }
}
