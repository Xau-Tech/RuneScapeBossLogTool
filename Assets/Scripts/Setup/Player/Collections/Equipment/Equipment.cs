using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : AbsItemSlotList
{
    //  Properties & fields

    public static readonly int MAINHANDINDEX = 5;
    public static readonly int OFFHANDINDEX = 7;
    public int TotalCost { get; private set; }
    public int AugmentsCost { get; private set; }
    public SetupItem Mainhand
    {
        get
        {
            return _data[MAINHANDINDEX].Item as SetupItem;
        }
        set
        {
            SetItemAtIndex(value, 1, MAINHANDINDEX);
        }
    }
    public SetupItem Offhand
    {
        set
        {
            SetItemAtIndex(value, 1, OFFHANDINDEX);
        }
    }

    private List<int> _slotsWithAugEquip = new List<int>();
    private const int _SIZE = 12;
    

    //  Constructor
    
    public Equipment() : base(_SIZE)
    {
        TotalCost = 0;
    }
    public Equipment(EquipmentGlob eg) : base(_SIZE)
    {
        TotalCost = 0;
        SetupItem si;

        for(int i = 0; i < _SIZE; ++i)
        {
            SetupItemDictionary.TryGetItem(eg.itemSlots[i].itemID, out si);
            si.SetIsEquipped(true);
            _data[i] = new ItemSlot(si, eg.itemSlots[i].quantity);

            if (si is AugmentedArmour || si is AugmentedWeapon)
                _slotsWithAugEquip.Add(i);
        }
    }

    //  Methods

    public override void SetItemAtIndex(SetupItem setupItem, uint quantity, int index)
    {
        //  Set the item as equipped
        setupItem.SetIsEquipped(true);

        //  Check if this is an augmented item
        bool isAugItem = (setupItem is AugmentedArmour || setupItem is AugmentedWeapon);

        //  Check if this slotid exists in the list
        if (_slotsWithAugEquip.Exists(value => value == index))
        {
            //  New item is not augmented, so remove this index from the list
            if (!isAugItem)
                _slotsWithAugEquip.Remove(index);
        }
        else
        {
            //  New item is augmented so add slot index to list
            if (isAugItem)
                _slotsWithAugEquip.Add(index);
        }

        base.SetItemAtIndex(setupItem, quantity, index);
        DetermineCost();
    }

    public void DetermineCost()
    {
        TotalCost = 0;

        //  Add all base costs to total
        for (int i = 0; i < _data.Count; ++i)
        {
            TotalCost += (int)_data[i].GetValue();
        }

        //  If there is something augmented equipped, at cost based on intensity + charge drain/s
        //  Because this is calculated from TOTAL drain/s it is not determined item by item, but only once if at least
        //  one augmented item is equipped
        if(_slotsWithAugEquip.Count > 0)
        {
            float chargesDrained = ApplicationController.Instance.CurrentSetup.ChargeDrainPerHour * ApplicationController.Instance.CurrentSetup.ChargeDrainRate;
            float percentDrained = chargesDrained / AugmentedArmour.MAXCHARGES;
            int cost = Mathf.RoundToInt(percentDrained * _data[_slotsWithAugEquip[0]].Item.Price);

            TotalCost += cost;
            AugmentsCost = cost;
        }
    }
}
