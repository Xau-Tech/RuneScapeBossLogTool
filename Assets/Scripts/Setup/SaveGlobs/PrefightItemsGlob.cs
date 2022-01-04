using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PrefightItemsGlob : ICollection<ItemSlotGlob>
{
    //  Properties & fields

    public List<ItemSlotGlob> itemSlots { get; set; }
    public int Count => ((ICollection<ItemSlotGlob>)itemSlots).Count;
    public bool IsReadOnly => ((ICollection<ItemSlotGlob>)itemSlots).IsReadOnly;

    //  Constructor

    public PrefightItemsGlob(Prefight pf)
    {
        itemSlots = new List<ItemSlotGlob>();

        foreach(ItemSlot slot in pf.GetData())
        {
            ItemSlotGlob it = new ItemSlotGlob(slot);
            itemSlots.Add(it);
        }
    }

    //  Methods

    public void Add(ItemSlotGlob item)
    {
        ((ICollection<ItemSlotGlob>)itemSlots).Add(item);
    }

    public void Clear()
    {
        ((ICollection<ItemSlotGlob>)itemSlots).Clear();
    }

    public bool Contains(ItemSlotGlob item)
    {
        return ((ICollection<ItemSlotGlob>)itemSlots).Contains(item);
    }

    public void CopyTo(ItemSlotGlob[] array, int arrayIndex)
    {
        ((ICollection<ItemSlotGlob>)itemSlots).CopyTo(array, arrayIndex);
    }

    public bool Remove(ItemSlotGlob item)
    {
        return ((ICollection<ItemSlotGlob>)itemSlots).Remove(item);
    }

    public IEnumerator<ItemSlotGlob> GetEnumerator()
    {
        return ((ICollection<ItemSlotGlob>)itemSlots).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((ICollection<ItemSlotGlob>)itemSlots).GetEnumerator();
    }
}
