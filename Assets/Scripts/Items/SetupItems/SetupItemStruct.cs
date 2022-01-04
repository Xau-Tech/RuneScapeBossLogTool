using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct SetupItemStruct : IComparable<SetupItemStruct>
{
    //  Properties & fields

    public int ItemId;
    public string ItemName;

    //  Constructor

    public SetupItemStruct(int itemId, string itemName)
    {
        this.ItemId = itemId;
        this.ItemName = itemName;
    }

    //  Methods

    public int CompareTo(SetupItemStruct other)
    {
        return this.ItemName.CompareTo(other.ItemName);
    }
}
