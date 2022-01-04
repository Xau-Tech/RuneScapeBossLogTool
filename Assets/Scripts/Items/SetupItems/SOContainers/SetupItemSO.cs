using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract ScriptableObject for SetupItems
/// </summary>
public abstract class SetupItemSO : ScriptableObject
{
    public int _itemId;
    public string _itemName;
    public bool _isStackable;
    public Sprite _itemSprite;
}
