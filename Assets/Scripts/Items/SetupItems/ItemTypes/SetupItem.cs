using System.Collections;
using System.Collections.Generic;
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

    /*public bool IsStackable;
    public Sprite ItemSprite { get { return itemSprite; } }

    //[SerializeField] private bool isStackable;
    [SerializeField] private Sprite itemSprite;*/

    public bool isStackable;
    public Sprite itemSprite;

    public override abstract ulong GetValue();

    public override string ToString()
    {
        return $"SetupItem [ Name: {itemName}, Price: {price}, ItemID: {itemID}, Stackable: {isStackable} ]";
    }

    public abstract object Clone();
}