using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Collection of equipment slots
public class Equipment : AbsItemSlotList
{
    public Equipment() : base(13)
    {
        
    }

    public int TotalCost { get; private set; }

    private List<int> augmentedEquipmentIndexes = new List<int>();

    public override void SetItemAtIndex(in SetupItem setupItem, int index)
    {
        //  Set the item as equipped
        setupItem.SetIsEquipped(true);

        //  Check if this is an augmented item
        bool isAugmentedItem = (setupItem is AugmentedArmour || setupItem is AugmentedWeapon);

        //  Equipment index indicates this slot holds an augmented item
        if(augmentedEquipmentIndexes.Exists(value => value == index))
        {
            //  New item is not augmented, so remove this index from the list
            if(!isAugmentedItem)
                augmentedEquipmentIndexes.Remove(index);
        }
        //  This slot does not hold an augmented item
        else
        {
            //  New item is augmented so add index to list
            if (isAugmentedItem)
                augmentedEquipmentIndexes.Add(index);
        }

        base.SetItemAtIndex(setupItem, index);

        //  Update price
        DetermineCost();

        EventManager.Instance.EquipmentAdded(in setupItem, in index);
    }

    public void DetermineCost()
    {
        TotalCost = 0;

        //  Add all base costs to total
        for(int i = 0; i < data.Count; ++i)
        {
            TotalCost += (int)data[i].GetValue();
        }

        //  If there is something augmented equipped, at cost based on intensity + charge drain/s
        //  Because this is calculated from TOTAL drain/s it is not determined item by item, but only once if at least
        //  one augmented item is equipped
        if (augmentedEquipmentIndexes.Count != 0)
        {
            float chargesDrained = CacheManager.SetupTab.Setup.ChargeDrainPerHour * CacheManager.SetupTab.Setup.ChargeDrainRate;

            float percentDrained = chargesDrained / AugmentedArmour.MAXCHARGES;

            int cost = Mathf.RoundToInt(percentDrained * data[augmentedEquipmentIndexes[0]].item.price);

            TotalCost += cost;
        }


    }
}
