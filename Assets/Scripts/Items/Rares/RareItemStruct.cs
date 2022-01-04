using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data holder for RareItem object
/// </summary>
public struct RareItemStruct
{
    //  Properties & fields
    public string Name { get; private set; }
    public int ItemId { get; private set; }

    //  Constructor
    public RareItemStruct(string name, int itemId)
    {
        this.Name = name;
        this.ItemId = itemId;
    }

    public override string ToString()
    {
        return $"[ ItemID: {ItemId}, Name: {Name} ]";
    }
}
