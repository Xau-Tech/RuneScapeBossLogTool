using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abs base class for any list of ItemSlots used in setup collections
/// </summary>
public abstract class AbsItemSlotList
{
    //  Properties & fields

    protected List<ItemSlot> _data = new List<ItemSlot>();

    //  Constructor

    public AbsItemSlotList(int size)
    {
        for(int i = 0; i < size; ++i)
        {
            _data.Add(new ItemSlot(General.NullItem(), 1));
        }
    }

    //  Methods

    public virtual void SetItemAtIndex(SetupItem setupItem, uint quantity, int index)
    {
        _data[index].Item = setupItem;
        _data[index].Quantity = quantity;
    }

    /// <summary>
    /// Get a list of empty slot indices
    /// </summary>
    /// <param name="startIndex">The index to start searching at</param>
    /// <param name="maxNumberOfSlots">The maximum number of slots to return</param>
    /// <returns></returns>
    public List<int> GetEmptySlots(int startIndex, int maxNumberOfSlots)
    {
        List<int> indices = new List<int>();
        int emptySlotsFound = 0;

        for(int i = startIndex; i < _data.Count; ++i)
        {
            //  Stop if enough empty slots have been found
            if(emptySlotsFound >= maxNumberOfSlots)
            {
                break;
            }
            else
            {
                //  Add index and increment slots found if slot is empty (id == -1)
                if(_data[i].Item.ItemId == -1)
                {
                    indices.Add(i);
                    ++emptySlotsFound;
                }
            }
        }

        return indices;
    }

    public List<ItemSlot> GetData()
    {
        return _data;
    }
}
