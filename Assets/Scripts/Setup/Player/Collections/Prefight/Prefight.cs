using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefight : AbsItemSlotList
{
    //  Properties & fields

    public int TotalCost { get; private set; }

    private const int _SIZE = 8;

    //  Constructor

    public Prefight() : base(_SIZE)
    {
        TotalCost = 0;
    }
    public Prefight(PrefightItemsGlob pg) : base(_SIZE)
    {
        TotalCost = 0;
        bool emptyGlob = (pg == null);
        SetupItem si;

        for(int i = 0; i < _SIZE; ++i)
        {
            if (emptyGlob)
            {
                _data[i] = new ItemSlot(General.NullItem(), 1);
            }
            else
            {
                SetupItemDictionary.TryGetItem(pg.itemSlots[i].itemID, out si);
                _data[i] = new ItemSlot(si, pg.itemSlots[i].quantity);
                TotalCost += (int)_data[i].GetValue();
            }
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
