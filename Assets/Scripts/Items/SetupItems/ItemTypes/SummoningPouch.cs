using System;

//  Class for pouches that summon familiars
public class SummoningPouch : SetupItem, ICloneable
{
    public SummoningPouch(SummoningPouchSO pouchData) : base(new Item(pouchData.itemID, pouchData.itemName, 0), pouchData.isStackable,
        pouchData.itemSprite)
    {

    }

    public override object Clone()
    {
        return MemberwiseClone() as SummoningPouch;
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
