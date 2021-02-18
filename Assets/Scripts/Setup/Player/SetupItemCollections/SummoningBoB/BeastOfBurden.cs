using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastOfBurden : AbsItemSlotList
{
    public int TotalCost { get; private set; }

    private const int SIZE = 32;

    public BeastOfBurden() : base(SIZE)
    {
        TotalCost = 0;
    }

    public BeastOfBurden(BeastOfBurdenGlob beastOfBurdenGlob) : base(SIZE)
    {
        TotalCost = 0;

        bool emptyGlob = (beastOfBurdenGlob == null);
        SetupItem setupItem;

        for (int i = 0; i < SIZE; ++i)
        {
            if (emptyGlob)
            {
                data[i] = new ItemSlot(General.NullItem(), 1);
            }
            else
            {
                SetupItemsDictionary.TryGetItem(beastOfBurdenGlob.itemSlots[i].itemID, out setupItem);
                data[i] = new ItemSlot(setupItem, beastOfBurdenGlob.itemSlots[i].quantity);
                TotalCost += (int)data[i].GetValue();
            }
        }
    }

    public override void SetItemAtIndex(in SetupItem setupItem, uint quantity, int index)
    {
        ulong previousCost = data[index].GetValue();

        base.SetItemAtIndex(setupItem, quantity, index);
        TotalCost += (int)(data[index].GetValue() - previousCost);

        EventManager.Instance.BeastOfBurdenItemAdded(in setupItem, quantity, index);
    }

    public override void FillUI()
    {
        for(int i = 0; i < data.Count; ++i)
        {
            EventManager.Instance.BeastOfBurdenItemAdded(data[i].item as SetupItem, data[i].quantity, i);
        }
    }
}
