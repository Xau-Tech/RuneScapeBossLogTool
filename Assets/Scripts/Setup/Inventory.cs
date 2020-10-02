using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : AbsItemSlotList
{
    private List<ItemSlot> items;

    public Inventory()
    {
        items = new List<ItemSlot>();
    }
}
