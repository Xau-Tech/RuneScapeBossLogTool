using UnityEngine;
using System;

public class Food : SetupItem, ICloneable
{
    public Food(FoodSO foodData) : base(new Item(foodData.itemID, foodData.itemName, 0), foodData.isStackable, foodData.itemSprite)
    {

    }

    public override object Clone()
    {
        return MemberwiseClone() as Food;
    }

    public override SetupItemCategories GetItemCategory()
    {
        return SetupItemCategories.Food;
    }

    public override ulong GetValue()
    {
        return price;
    }

    public override void SetIsEquipped(bool flag) { }
}
