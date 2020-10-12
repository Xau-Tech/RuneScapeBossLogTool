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

    public override void SetItemAtIndex(in SetupItem setupItem, int index)
    {
        int costDelta = (int)(setupItem.GetValue() - data[index].GetValue());
        TotalCost += costDelta;

        base.SetItemAtIndex(setupItem, index);
        EventManager.Instance.InventoryItemAdded(in setupItem, in index);
    }
}
