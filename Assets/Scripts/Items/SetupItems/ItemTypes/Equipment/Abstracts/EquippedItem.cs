using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//  Abstract class for any equippable item
public abstract class EquippedItem : SetupItem, ICloneable
{
    public EquippedItem(NondegradeWeaponSO equipmentSO) : base(new Item(equipmentSO.itemID, equipmentSO.itemName, 0), equipmentSO.isStackable, equipmentSO.itemSprite)
    {
        isEquipped = false;
        this.itemCategory = equipmentSO.itemCategory;
    }
    public EquippedItem(NondegradeArmourSO equipmentSO) : base(new Item(equipmentSO.itemID, equipmentSO.itemName, 0), equipmentSO.isStackable, equipmentSO.itemSprite)
    {
        isEquipped = false;
        this.itemCategory = equipmentSO.itemCategory;
    }
    public EquippedItem(AugArmourSO equipmentSO) : base(new Item(equipmentSO.itemID, equipmentSO.itemName, 0), equipmentSO.isStackable, equipmentSO.itemSprite)
    {
        isEquipped = false;
        this.itemCategory = equipmentSO.itemCategory;
    }
    public EquippedItem(AugWeaponSO equipmentSO) : base(new Item(equipmentSO.itemID, equipmentSO.itemName, 0), equipmentSO.isStackable, equipmentSO.itemSprite)
    {
        isEquipped = false;
        this.itemCategory = equipmentSO.itemCategory;
    }
    public EquippedItem(DegradableWeaponSO equipmentSO) : base(new Item(equipmentSO.itemID, equipmentSO.itemName, 0), equipmentSO.isStackable, equipmentSO.itemSprite)
    {
        isEquipped = false;
        this.itemCategory = equipmentSO.itemCategory;
    }
    public EquippedItem(DegradableArmourSO equipmentSO) : base(new Item(equipmentSO.itemID, equipmentSO.itemName, 0), equipmentSO.isStackable, equipmentSO.itemSprite)
    {
        isEquipped = false;
        this.itemCategory = equipmentSO.itemCategory;
    }

    public SetupItemCategories itemCategory;
    public bool isEquipped;

    public override object Clone()
    {
        EquippedItem clone = MemberwiseClone() as EquippedItem;
        clone.isEquipped = false;
        return clone;
    }

    public override void SetIsEquipped(bool flag)
    {
        isEquipped = flag;
    }

    public override abstract ulong GetValue();
}