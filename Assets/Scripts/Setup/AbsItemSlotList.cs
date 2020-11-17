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
            data.Add(new ItemSlot(General.NullItem(), 1));
        }
    }

    protected List<ItemSlot> data = new List<ItemSlot>();

    public virtual void SetItemAtIndex(in SetupItem setupItem, int index)
    {
        //  Update the item
        data[index].item = setupItem;
    }

    public List<ItemSlot> GetData()
    {
        return data;
    }
}
