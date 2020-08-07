using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RareItemList : ICollection<RareItem>
{
    //  ICollection properties
    public int Count => ((ICollection<RareItem>)data).Count;
    public bool IsReadOnly => ((ICollection<RareItem>)data).IsReadOnly;

    private List<RareItem> data { get; set; }

    public RareItemList()
    {
        data = new List<RareItem>();
    }

    //  Addition operator overload
    public static RareItemList operator +(RareItemList firstList, RareItemList secondList)
    {
        RareItemList returnList = new RareItemList();

        foreach(RareItem rareItem in firstList)
            returnList.Add(rareItem);

        foreach (RareItem rareItem in secondList)
            returnList.Add(rareItem);

        returnList.data.Sort();

        return returnList;
    }

    //  Take in a DropList from which to add/update this instance's data
    public void AddFromDropsList(in ItemSlotList itemSlotList)
    {
        PrintToDebug();
        foreach(ItemSlot drop in itemSlotList)
        {
            //  Add if drop is a rare drop
            if (RareItemDB.IsRare(CacheManager.currentBoss, drop.item.itemID))
            {
                Add(drop);
            }
        }

        data.Sort();
        Debug.Log($"After addition of current drops:");
        PrintToDebug();
    }

    //  Handles new drops and adding to existing drops
    private void Add(ItemSlot itemSlot)
    {
        RareItem rare;
        
        //  Item is already in the list
        if((rare = data.Find(rareItem => rareItem.itemID.CompareTo(itemSlot.item.itemID) == 0)) != null)
        {
            //  Check if adding would wrap the quantity field
            if(rare.quantity.WillWrap((ushort)itemSlot.quantity))
            {
                InputWarningWindow.Instance.OpenWindow($"Cannot add to the quantity of {itemSlot.item.name}!\n" +
                    $"Quantity is at {rare.quantity} of {ushort.MaxValue} maximum.");
                return;
            }
            else
                rare.quantity += (ushort)itemSlot.quantity;
        }
        //  Item isn't in the list
        else
        {
            data.Add(new RareItem(itemSlot));
        }

        data.Sort();
    }

    public void PrintToDebug()
    {
        if(data.Count == 0)
        {
            Debug.Log("RareItemList is currently empty");
            return;
        }

        foreach(RareItem rare in data)
        {
            Debug.Log($"Item [ Name: {rare.GetName()}, Quantity: {rare.quantity} ]");
        }
    }


    //  ICollection Interface Methods

    public void Add(RareItem item)
    {
        ((ICollection<RareItem>)data).Add(item);
        data.Sort();
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

    public bool Remove(RareItem item)
    {
        return ((ICollection<RareItem>)data).Remove(item);
    }

    public IEnumerator<RareItem> GetEnumerator()
    {
        return ((ICollection<RareItem>)data).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((ICollection<RareItem>)data).GetEnumerator();
    }
}
