using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// List of ItemSlot objects
/// </summary>
public class ItemSlotList : IEnumerable
{
    //  Properties & fields
    public int Count { get { return _data.Count; } }

    private List<ItemSlot> _data;

    //  Constructor
    public ItemSlotList()
    {
        _data = new List<ItemSlot>();
    }

    //  Methods

    public void Add(ItemSlot itemSlot)
    {
        _data.Add(itemSlot);
    }

    public void AddToDrop(string dropName, uint addedQuantity)
    {
        ItemSlot drop = Find(dropName);

        //  Make sure adding wouldn't wrap the quantity
        if(drop.Quantity.WillWrap(addedQuantity) && !RareItemDB.IsRare(ApplicationController.Instance.CurrentBoss.BossName, drop.Item.ItemId))
        {
            PopupManager.Instance.ShowNotification($"Cannot add {addedQuantity} to {drop.Item.ItemName}!\nMaximum item quantity for non-rares is {uint.MaxValue}.");
            return;
        }
        else if(drop.Quantity + addedQuantity > ushort.MaxValue && RareItemDB.IsRare(ApplicationController.Instance.CurrentBoss.BossName, drop.Item.ItemId))
        {
            PopupManager.Instance.ShowNotification($"Cannot add {addedQuantity} to {drop.Item.ItemName}!\nMaximum item quantity for rares is {ushort.MaxValue}.");
            return;
        }

        //  Make sure adding wouldn't wrap the BossLog's lootValue (ulong)
        //  Seriously though it's 18 quintillion and the current most expensive drops hover around 500m - that's 36 billion of that item to reach this code
        if(TotalValue().WillWrap(addedQuantity * drop.Item.Price))
        {
            PopupManager.Instance.ShowNotification($"Cannot add {addedQuantity} to {drop.Item.ItemName}!\nMaximum loot value is {ulong.MaxValue}.");
            return;
        }

        drop.Quantity += addedQuantity;
    }

    public void Remove(int index)
    {
        if (index < 0 || index > _data.Count)
            throw new System.ArgumentOutOfRangeException();
        else
            _data.RemoveAt(index);
    }

    public void Remove(ItemSlot itemSlot)
    {
        _data.Remove(itemSlot);
    }

    public void Clear()
    {
        _data.Clear();
    }

    public void Print()
    {
        Debug.Log($"Printing drop list");
        foreach(ItemSlot slot in _data)
        {
            Debug.Log(slot.Print());
        }
    }

    public bool Exists(string dropName)
    {
        return _data.Exists(drop => drop.Item.ItemName.CompareTo(dropName) == 0);
    }

    public ItemSlot Find(string dropName)
    {
        return _data.Find(drop => drop.Item.ItemName.CompareTo(dropName) == 0);
    }

    public ItemSlot Find(int itemId)
    {
        return _data.Find(drop => drop.Item.ItemId == itemId);
    }

    public ItemSlot AtIndex(int index)
    {
        if (index < 0 || index > _data.Count)
            throw new System.ArgumentOutOfRangeException();
        else
            return _data[index];
    }

    public int IndexOf(ItemSlot itemSlot)
    {
        for(int i = 0; i < _data.Count; ++i)
        {
            if (_data[i] == itemSlot)
                return i;
        }

        return -1;
    }

    public ulong TotalValue()
    {
        ulong total = 0;
        foreach(ItemSlot slot in _data)
        {
            total += slot.GetValue();
        }
        return total;
    }

    public IEnumerator GetEnumerator()
    {
        return ((IEnumerable)_data).GetEnumerator();
    }
}
