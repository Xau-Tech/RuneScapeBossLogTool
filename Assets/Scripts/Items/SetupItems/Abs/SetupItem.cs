using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract base class for all setup items
/// </summary>
public abstract class SetupItem : Item, ICloneable
{
    //  Properties & fields

    public bool IsStackable { get; set; }
    public Sprite ItemSprite { get; set; }

    //  Constructor

    public SetupItem() { }
    public SetupItem(Item item, bool isStackable, Sprite itemSprite)
    {
        base.ItemId = item.ItemId;
        base.ItemName = item.ItemName;
        base.Price = item.Price;
        this.IsStackable = isStackable;
        this.ItemSprite = itemSprite;
    }

    //  Methods

    public override string ToString()
    {
        return $"SetupItem [ Name: {ItemName}, Price: {Price}, ItemID: {ItemId}, Stackable: {IsStackable} ]";
    }

    public abstract object Clone();
    public abstract void SetIsEquipped(bool flag);
    public abstract Enums.SetupItemCategory GetItemCategory();
    public abstract override ulong GetValue();
}
