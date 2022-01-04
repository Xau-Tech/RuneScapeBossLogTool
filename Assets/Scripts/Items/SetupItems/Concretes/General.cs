using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Any non-specific other setup item
/// </summary>
public class General : SetupItem
{
    //  Constructor

    public General() { }
    public General(Item item, bool isStackable, Sprite itemSprite) : base(item, isStackable, itemSprite) { }
    public General(GeneralItemSO generalData) : base(new Item(generalData._itemId, generalData._itemName, 0), generalData._isStackable, generalData._itemSprite) { }

    //  Methods

    public static General NullItem()
    {
        return new General(new Item(-1, "", 0), false, null);
    }

    public override object Clone()
    {
        return MemberwiseClone() as General;
    }

    public override Enums.SetupItemCategory GetItemCategory()
    {
        return Enums.SetupItemCategory.General;
    }

    public override ulong GetValue()
    {
        return Price;
    }

    public override void SetIsEquipped(bool flag) { }
}
