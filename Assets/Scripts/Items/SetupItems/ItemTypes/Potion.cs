using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Setup/ItemTypes/Potion", order = 2)]
public class Potion : SetupItem, ICloneable
{
    public Potion(PotionSO potionData) : base(new Item(potionData.itemID, potionData.itemName, 0), potionData.isStackable, potionData.itemSprite)
    {

    }

    public override object Clone()
    {
        return MemberwiseClone() as Potion;
    }

    public override SetupItemCategories GetItemCategory()
    {
        return SetupItemCategories.Potion;
    }

    public override ulong GetValue()
    {
        return price;
    }

    public override void SetIsEquipped(bool flag) { }
}
