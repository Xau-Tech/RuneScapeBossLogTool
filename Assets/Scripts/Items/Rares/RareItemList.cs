using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Wrapper for a list of RareItem objects
/// </summary>
[System.Serializable]
public class RareItemList : ICollection<RareItem>
{
    //  Properties & fields
    public int Count => ((ICollection<RareItem>)data).Count;
    public bool IsReadOnly => ((ICollection<RareItem>)data).IsReadOnly;

    private List<RareItem> data { get; set; }

    //  Constructor
    public RareItemList()
    {
        data = new List<RareItem>();
    }

    //  Methods

    public void AddFromDropsList(ItemSlotList itemSlotList)
    {
        foreach(ItemSlot drop in itemSlotList)
        {
            //  Add if drop is a rare drop
            if (RareItemDB.IsRare(ApplicationController.Instance.CurrentBoss.BossName, drop.Item.ItemId))
                Add(drop);
        }

        data.Sort();
    }

    private void Add(ItemSlot itemSlot)
    {
        RareItem rare;

        //  Item is already in the list
        if ((rare = data.Find(rareItem => rareItem.itemID.CompareTo(itemSlot.Item.ItemId) == 0)) != null)
        {
            //  Check if adding would wrap the quantity field
            if (rare.quantity.WillWrap((ushort)itemSlot.Quantity))
            {
                PopupManager.Instance.ShowNotification($"Cannot add to the quantity of {itemSlot.Item.ItemName}!\n" +
                    $"Quantity is at {rare.quantity} of {ushort.MaxValue} maximum.");
                return;
            }
            else
            {
                rare.quantity += (ushort)itemSlot.Quantity;
            }
        }
        //  Item isn't in the list
        else
        {
            data.Add(new RareItem(itemSlot));
        }

        data.Sort();
    }

    public static RareItemList operator +(RareItemList firstList, RareItemList secondList)
    {
        foreach(RareItem rareItem in secondList)
        {
            int index = firstList.data.FindIndex(item => item.itemID == rareItem.itemID);

            //  Add item to list
            if (index == -1)
                firstList.Add(rareItem);
            //  Increment quantity in list
            else
                firstList.data[index] += rareItem;
        }

        firstList.data.Sort();
        return firstList;
    }

    public void Add(RareItem item)
    {
        ((ICollection<RareItem>)data).Add(item);
    }

    public void Clear()
    {
        ((ICollection<RareItem>)data).Clear();
    }

    public bool Contains(RareItem item)
    {
        return ((ICollection<RareItem>)data).Contains(item);
    }

    public void CopyTo(RareItem[] array, int arrayIndex)
    {
        ((ICollection<RareItem>)data).CopyTo(array, arrayIndex);
    }

    public IEnumerator<RareItem> GetEnumerator()
    {
        return ((ICollection<RareItem>)data).GetEnumerator();
    }

    public bool Remove(RareItem item)
    {
        return ((ICollection<RareItem>)data).Remove(item);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((ICollection<RareItem>)data).GetEnumerator();
    }
}
