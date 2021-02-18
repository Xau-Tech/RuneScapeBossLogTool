using System;

//  Class for the special move scrolls used by familiars
public class SummoningScroll : SetupItem, ICloneable
{
    public SummoningScroll(SummoningScrollSO scrollData) : base(new Item(scrollData.itemID, scrollData.itemName, 0), scrollData.isStackable,
        scrollData.itemSprite)
    {

    }

    public override object Clone()
    {
        return MemberwiseClone() as SummoningScroll;
    }

    public override SetupItemCategories GetItemCategory()
    {
        return SetupItemCategories.Familiars;
    }

    public override ulong GetValue()
    {
        return price;
    }

    public override void SetIsEquipped(bool flag)
    {

    }
}