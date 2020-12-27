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

    public override void SetItemAtIndex(in SetupItem setupItem, uint quantity, int index)
    {
        ulong previousCost = data[index].GetValue();

        base.SetItemAtIndex(setupItem, quantity, index);
        TotalCost += (int)(data[index].GetValue() - previousCost);

        EventManager.Instance.InventoryItemAdded(in setupItem, quantity, in index);
    }
}
