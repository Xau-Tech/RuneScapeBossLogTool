using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : AbsItemSlotList
{
    //  Properties & fields

    public int TotalCost { get; private set; }

    private const int _SIZE = 28;

    //  Constructor

    public Inventory() : base(_SIZE)
    {
        TotalCost = 0;
    }
    public Inventory(InventoryGlob ig) : base(_SIZE)
    {
        TotalCost = 0;
        SetupItem si;

        for(int i = 0; i < _SIZE; ++i)
        {
            SetupItemDictionary.TryGetItem(ig.itemSlots[i].itemID, out si);
            _data[i] = new ItemSlot(si, ig.itemSlots[i].quantity);
            TotalCost += (int)_data[i].GetValue();
        }
    }

    //  Methods

    public override void SetItemAtIndex(SetupItem setupItem, uint quantity, int index)
    {
        ulong previousCost = _data[index].GetValue();

        base.SetItemAtIndex(setupItem, quantity, index);
        TotalCost += (int)(_data[index].GetValue() - previousCost);
    }
}
