using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Abstract class for any item that can be equipped
/// </summary>
public abstract class EquipableItem : SetupItem, ICloneable
{
    //  Properties & fields

    public bool IsEquipped { get; set; }

    private Enums.SetupItemCategory _itemCategory;

    //  Constructor

    public EquipableItem() { }
    public EquipableItem(NondegradeWeaponSO equipmentSO) : base(new Item(equipmentSO._itemId, equipmentSO._itemName, 0), equipmentSO._isStackable, equipmentSO._itemSprite)
    {
        IsEquipped = false;
        this._itemCategory = equipmentSO._itemCategory;
    }
    public EquipableItem(NondegradeArmourSO equipmentSO) : base(new Item(equipmentSO._itemId, equipmentSO._itemName, 0), equipmentSO._isStackable, equipmentSO._itemSprite)
    {
        IsEquipped = false;
        this._itemCategory = equipmentSO._itemCategory;
    }
    public EquipableItem(AugArmourSO equipmentSO) : base(new Item(equipmentSO._itemId, equipmentSO._itemName, 0), equipmentSO._isStackable, equipmentSO._itemSprite)
    {
        IsEquipped = false;
        this._itemCategory = equipmentSO._itemCategory;
    }
    public EquipableItem(AugWeaponSO equipmentSO) : base(new Item(equipmentSO._itemId, equipmentSO._itemName, 0), equipmentSO._isStackable, equipmentSO._itemSprite)
    {
        IsEquipped = false;
        this._itemCategory = equipmentSO._itemCategory;
    }
    public EquipableItem(DegradableWeaponSO equipmentSO) : base(new Item(equipmentSO._itemId, equipmentSO._itemName, 0), equipmentSO._isStackable, equipmentSO._itemSprite)
    {
        IsEquipped = false;
        this._itemCategory = equipmentSO._itemCategory;
    }
    public EquipableItem(DegradableArmourSO equipmentSO) : base(new Item(equipmentSO._itemId, equipmentSO._itemName, 0), equipmentSO._isStackable, equipmentSO._itemSprite)
    {
        IsEquipped = false;
        this._itemCategory = equipmentSO._itemCategory;
    }
    public EquipableItem(TimeDegradeArmourSO equipmentSO) : base(new Item(equipmentSO._itemId, equipmentSO._itemName, 0), equipmentSO._isStackable, equipmentSO._itemSprite)
    {
        IsEquipped = false;
        this._itemCategory = equipmentSO._itemCategory;
    }

    //  Methods

    public override object Clone()
    {
        EquipableItem clone = MemberwiseClone() as EquipableItem;
        clone.IsEquipped = false;
        return clone;
    }

    public override void SetIsEquipped(bool flag)
    {
        IsEquipped = flag;
    }

    public override Enums.SetupItemCategory GetItemCategory()
    {
        return _itemCategory;
    }

    public abstract override ulong GetValue();
}
