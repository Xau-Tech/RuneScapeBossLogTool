using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Collection of equipment slots
public class Equipment : AbsItemSlotList
{
    public Equipment() : base(13)
    {

    }

    public override void SetItemAtIndex(in SetupItem setupItem, in int index)
    {
        base.SetItemAtIndex(setupItem, index);
        EventManager.Instance.InventoryItemAdded(in setupItem, in index);
    }
}
