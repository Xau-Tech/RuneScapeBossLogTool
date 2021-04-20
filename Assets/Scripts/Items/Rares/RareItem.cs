using System;

//  Holds data for rare items that are saved with the user's log information
[System.Serializable]
public class RareItem : IComparable<RareItem>
{
    public ushort quantity { get; set; }
    public int itemID { get; protected set; }

    public RareItem(int itemID, ushort quantity)
    {
        this.itemID = itemID;
        this.quantity = quantity;
    }
    public RareItem(ItemSlot itemSlot)
    {
        if (itemSlot.quantity > ushort.MaxValue)
            throw new System.Exception($"{itemSlot.item.itemName}'s quantity is larger than {ushort.MaxValue}!  Cannot create RareItem from this drop!");
        else
        {
            this.quantity = (ushort)itemSlot.quantity;
            this.itemID = itemSlot.item.itemID;
        }
    }

    public static RareItem operator +(RareItem firstRare, RareItem secondRare)
    {
        ushort totalQuantity = Convert.ToUInt16(firstRare.quantity + secondRare.quantity);

        return new RareItem(firstRare.itemID, Convert.ToUInt16(totalQuantity));
    }

    public string GetName()
    {
        return RareItemDB.GetRareItemName(CacheManager.currentBoss.bossName, itemID);
    }

    //  IComparable interface
    public int CompareTo(RareItem other)
    {
        return this.itemID.CompareTo(other.itemID);
    }
}
