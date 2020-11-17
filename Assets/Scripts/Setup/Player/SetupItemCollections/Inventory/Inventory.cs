using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : AbsItemSlotList
{
    public int TotalCost { get; private set; }

    public Inventory() : base(28)
    {
        TotalCost = 0;
    }

    public Inventory(InventoryGlob inventorySaveGlob) : base(28)
    {
        TotalCost = 0;

        SetupItem setupItem;

        for(int i = 0; i < 28; ++i)
        {
            SetupItemsDictionary.TryGetItem(inventorySaveGlob.itemSlots[i].itemID, out setupItem);
            data[i] = new ItemSlot(setupItem, inventorySaveGlob.itemSlots[i].quantity);
            TotalCost += (int)setupItem.GetValue();
        }
    }

    public override void SetItemAtIndex(in SetupItem setupItem, int index)
    {
        int costDelta = (int)(setupItem.GetValue() - data[index].GetValue());
        TotalCost += costDelta;

        base.SetItemAtIndex(setupItem, index);
        EventManager.Instance.InventoryItemAdded(in setupItem, in index);
    }
}
