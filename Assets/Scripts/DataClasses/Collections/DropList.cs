using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Collection for drop objects
public class DropList : IEnumerable
{
    public DropList()
    {
        data = new List<Drop>();
    }

    private List<Drop> data;

    // Wrapper for List.Add
    public void Add(in Drop drop)
    {
        data.Add(drop);
        EventManager.Instance.DropListModified();
    }

    //  Modify an existing drop in the collection
    public void AddToDrop(string dropName, in uint addedQuantity)
    {
        Drop drop = Find(dropName);

        //  Make sure adding wouldn't wrap the Drop's quantity (ushort)
        if (drop.quantity.WillWrap(in addedQuantity) && !RareItemDB.IsRare(CacheManager.currentBoss, drop.name))
        {
            InputWarningWindow.Instance.OpenWindow($"Cannot add {addedQuantity} to {drop.name}!\nMaximum item quantity for non-rares is {uint.MaxValue}.");
            return;
        }
        else if(drop.quantity + addedQuantity > ushort.MaxValue && RareItemDB.IsRare(CacheManager.currentBoss, drop.name))
        {
            InputWarningWindow.Instance.OpenWindow($"Cannot add {addedQuantity} to {drop.name}!\nMaximum item quantity for rares is {ushort.MaxValue}.");
            return;
        }

        //  Make sure adding wouldn't wrap the BossLog's lootValue (ulong)
        //  Seriously though it's 18 quintillion and the current most expensive drops hover around 500m - that's 36 billion of that item to reach this code
        if(TotalValue().WillWrap(addedQuantity * drop.price))
        {
            InputWarningWindow.Instance.OpenWindow($"Cannot add {addedQuantity} to {drop.name}!\nMaximum loot value is {ulong.MaxValue}.");
            return;
        }

        drop.quantity += addedQuantity;
        EventManager.Instance.DropListModified();

    }

    //  Wrapper for List.Remove
    public void Remove(in Drop drop)
    {
        data.Remove(drop);
        EventManager.Instance.DropListModified();
    }

    //  Wrapper for List.Clear
    public void Clear()
    {
        data.Clear();
        EventManager.Instance.DropListModified();
    }

    public void Print()
    {
        Debug.Log($"Printing Drop List");

        for (int i = 0; i < data.Count; ++i)
        {
            Debug.Log(data[i].Print());
        }
    }

    //  Wrapper for List.Exists
    public bool Exists(string dropName)
    {
        return data.Exists(drop => drop.name.CompareTo(dropName) == 0);
    }

    //  Wrapper for List.Find
    public Drop Find(string dropName)
    {
        return data.Find(drop => drop.name.CompareTo(dropName) == 0);
    }

    //  Returns the drop at the specified index
    public Drop AtIndex(in int index)
    {
        if(index >= 0 && index < data.Count)
        {
            return data[index];
        }
        else
        {
            return null;
            throw new System.ArgumentOutOfRangeException();
        }
    }

    //  Wrapper for List.Count
    public int Count
    {
        get { return data.Count; }
    }

    //  Return the total value of drops in the collection
    public ulong TotalValue()
    {
        ulong totalValue = 0;

        for(int i = 0; i < data.Count; ++i)
        {
            totalValue += data[i].GetValue();
        }

        return totalValue;
    }

    public IEnumerator GetEnumerator()
    {
        return ((IEnumerable)data).GetEnumerator();
    }
}
