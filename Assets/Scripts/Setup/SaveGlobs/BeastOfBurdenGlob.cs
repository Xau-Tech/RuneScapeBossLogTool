using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BeastOfBurdenGlob : ICollection<ItemSlotGlob>
{
    public List<ItemSlotGlob> itemSlots { get; set; }

    public int Count => ((ICollection<ItemSlotGlob>)itemSlots).Count;

    public bool IsReadOnly => ((ICollection<ItemSlotGlob>)itemSlots).IsReadOnly;

    public BeastOfBurdenGlob(in BeastOfBurden beastOfBurden)
    {
        itemSlots = new List<ItemSlotGlob>();

        foreach(ItemSlot itemSlot in beastOfBurden.GetData())
        {
            ItemSlotGlob it = new ItemSlotGlob(in itemSlot);
            itemSlots.Add(it);
        }
    }

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
