using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SetupListGlob : ICollection<SetupSaveGlob>
{
    //  Properties & fields

    public List<SetupSaveGlob> data { get; set; }
    public int Count => ((ICollection<SetupSaveGlob>)data).Count;
    public bool IsReadOnly => ((ICollection<SetupSaveGlob>)data).IsReadOnly;

    //  Constructor

    public SetupListGlob()
    {
        data = new List<SetupSaveGlob>();
    }
    public SetupListGlob(List<Setup> setups)
    {
        data = new List<SetupSaveGlob>();

        foreach(Setup setup in setups)
        {
            SetupSaveGlob sg = new SetupSaveGlob(setup);
            data.Add(sg);
        }
    }

    //  Methods

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

    public bool Remove(SetupSaveGlob item)
    {
        return ((ICollection<SetupSaveGlob>)data).Remove(item);
    }

    public IEnumerator<SetupSaveGlob> GetEnumerator()
    {
        return ((ICollection<SetupSaveGlob>)data).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((ICollection<SetupSaveGlob>)data).GetEnumerator();
    }
}
