using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class SetupListGlob : ICollection<SetupSaveGlob>
{
    public List<SetupSaveGlob> data { get; set; }

    public SetupListGlob()
    {
        data = new List<SetupSaveGlob>();
    }

    public SetupListGlob(in List<Setup> list)
    {
        data = new List<SetupSaveGlob>();

        for(int i = 0; i < list.Count; ++i)
        {
            SetupSaveGlob glob = new SetupSaveGlob(list[i]);
            data.Add(glob);
        }
    }

    public int Count => ((ICollection<SetupSaveGlob>)data).Count;

    public bool IsReadOnly => ((ICollection<SetupSaveGlob>)data).IsReadOnly;

    public void Add(SetupSaveGlob item)
    {
        ((ICollection<SetupSaveGlob>)data).Add(item);
    }

    public void Clear()
    {
        ((ICollection<SetupSaveGlob>)data).Clear();
    }

    public bool Contains(SetupSaveGlob item)
    {
        return ((ICollection<SetupSaveGlob>)data).Contains(item);
    }

    public void CopyTo(SetupSaveGlob[] array, int arrayIndex)
    {
        ((ICollection<SetupSaveGlob>)data).CopyTo(array, arrayIndex);
    }

    public IEnumerator<SetupSaveGlob> GetEnumerator()
    {
        return ((ICollection<SetupSaveGlob>)data).GetEnumerator();
    }

    public bool Remove(SetupSaveGlob item)
    {
        return ((ICollection<SetupSaveGlob>)data).Remove(item);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((ICollection<SetupSaveGlob>)data).GetEnumerator();
    }
}
