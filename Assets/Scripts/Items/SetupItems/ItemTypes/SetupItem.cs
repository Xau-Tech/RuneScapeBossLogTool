using UnityEngine;
using System;

public abstract class SetupItem : Item, ICloneable
{
    public SetupItem() { }

    public SetupItem(Item item, bool isStackable, Sprite itemSprite)
    {
        itemID = item.itemID;
        itemName = item.itemName;
        price = item.price;
        this.isStackable = isStackable;
        this.itemSprite = itemSprite;
    }

    public bool isStackable;
    public Sprite itemSprite;

    public override string ToString()
    {
        return $"SetupItem [ Name: {itemName}, Price: {price}, ItemID: {itemID}, Stackable: {isStackable} ]";
    }

    public abstract object Clone();
    public abstract void SetIsEquipped(bool flag);
    public abstract SetupItemCategories GetItemCategory();
    public override abstract ulong GetValue();
}