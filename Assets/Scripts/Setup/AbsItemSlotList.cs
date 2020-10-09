using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Abstract class for List of ItemSlots for use in Setups
public abstract class AbsItemSlotList
{
    public AbsItemSlotList(int size)
    {
        for(int i = 0; i < size; ++i)
        {
            data.Add(new ItemSlot(new Item(-1, "Null", 0), 1));
        }
    }

    protected List<ItemSlot> data = new List<ItemSlot>();

    public virtual void SetItemAtIndex(in SetupItem setupItem, in int index)
    {
        //  Calculate and apply the change in cost
        long deltaCost = (long)setupItem.GetValue() - (long)data[index].GetValue();
        SetupInfo.Instance.AddToTotalCost(in deltaCost);

        //  Update the item
        data[index].item = setupItem;
    }
}
