using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract ScriptableObject for any item that can be equipped
/// </summary>
public abstract class EquipableItemSO : SetupItemSO
{
    public Enums.SetupItemCategory _itemCategory;
}
