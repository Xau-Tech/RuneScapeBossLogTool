using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : AbsItemSlotList
{
    public Inventory() : base(28)
    {

    }

    public override void SetItemAtIndex(in SetupItem setupItem, in int index)
    {
        base.SetItemAtIndex(setupItem, index);
        EventManager.Instance.InventoryItemAdded(in setupItem, in index);
    }
}
