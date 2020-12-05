using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : SetupItem
{
    public General() { }
    public General(Item item, bool isStackable, Sprite itemSprite) : base(item, isStackable, itemSprite) { }
    public General(GeneralItemSO generalItemData) : base(new Item(generalItemData.itemID, generalItemData.itemName, 0), generalItemData.isStackable, generalItemData.itemSprite) { }

    public static General NullItem()
    {
        return new General(new Item(-1, "", 0), false, null);
    }

    public override object Clone()
    {
        return MemberwiseClone() as General;
    }

    public override SetupItemCategories GetItemCategory()
    {
        return SetupItemCategories.General;
    }

    public override ulong GetValue()
    {
        return price;
    }

    public override void SetIsEquipped(bool flag)
    {
        
    }
}
