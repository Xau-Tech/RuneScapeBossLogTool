using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RareItemList : ICollection<RareItem>
{
    private List<RareItem> data { get; set; }

    public int Count => ((ICollection<RareItem>)data).Count;

    public bool IsReadOnly => ((ICollection<RareItem>)data).IsReadOnly;

    public RareItemList()
    {
        data = new List<RareItem>();
    }

    public static RareItemList operator +(RareItemList firstList, RareItemList secondList)
    {
        RareItemList returnList = new RareItemList();

        foreach(RareItem rareItem in firstList)
            returnList.Add(rareItem);

        foreach (RareItem rareItem in secondList)
            returnList.Add(rareItem);

        return returnList;
    }

    //  Take in a DropList from which to add/update this instance's data
    public void AddFromDropsList(in DropList dropList)
    {
        PrintToDebug();
        foreach(Drop drop in dropList)
        {
            //  Add if drop is a rare drop
            if (RareItemDB.IsRare(CacheManager.currentBoss, drop.name))
            {
                Add(drop);
            }
        }

        Debug.Log($"After addition of current drops:");
        PrintToDebug();
    }

    //  Wrapper to add w/ a passed Drop object
    //  Handles new drops and adding to existing drops
    private void Add(Drop drop)
    {
        RareItem rare;
        if((rare = data.Find(rareItem => rareItem.itemName.CompareTo(drop.name) == 0)) != null)
        {
            //  Check if adding would wrap the quantity field
            if(rare.quantity.WillWrap((ushort)drop.quantity))
            {
                InputWarningWindow.Instance.OpenWindow($"Cannot add to the quantity of {drop.name}!\n" +
                    $"Quantity is at {rare.quantity} of {ushort.MaxValue} maximum.");
                return;
            }
            else
                rare.quantity += (ushort)drop.quantity;
        }
        else
        {
            data.Add(new RareItem(drop));
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
            Debug.Log($"Item [ Name: {rare.itemName}, Quantity: {rare.quantity} ]");
        }
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
