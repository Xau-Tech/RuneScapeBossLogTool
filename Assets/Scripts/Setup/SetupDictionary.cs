using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SetupDictionary : IDictionary<string, Setup>
{
    //  Properties & fields

    public bool HasUnsavedData { get; set; }
    public ICollection<string> Keys => ((IDictionary<string, Setup>)_setupDictionary).Keys;
    public ICollection<Setup> Values => ((IDictionary<string, Setup>)_setupDictionary).Values;
    public int Count => ((IDictionary<string, Setup>)_setupDictionary).Count;
    public bool IsReadOnly => ((IDictionary<string, Setup>)_setupDictionary).IsReadOnly;
    public Setup this[string key] { get => ((IDictionary<string, Setup>)_setupDictionary)[key]; set => ((IDictionary<string, Setup>)_setupDictionary)[key] = value; }

    private Dictionary<string, Setup> _setupDictionary;
    private string _filePath = Application.persistentDataPath;

    //  Constructor

    public SetupDictionary()
    {
        _setupDictionary = new Dictionary<string, Setup>();
        HasUnsavedData = false;
        _filePath = Application.persistentDataPath;
        _filePath += Application.isEditor ? "/testSetups.dat" : "/setups.dat";
    }

    //  Methods

    public void Load()
    {
        if (File.Exists(_filePath))
        {
            using (FileStream file = File.Open(_filePath, FileMode.Open))
            {
                if(file.Length != 0)
                {
                    BinaryFormatter bf = new BinaryFormatter();

                    SetupListGlob loadGlob = new SetupListGlob();
                    loadGlob.data = (List<SetupSaveGlob>)bf.Deserialize(file);

                    Setup setup;

                    foreach(SetupSaveGlob sg in loadGlob.data)
                    {
                        setup = new Setup(sg, ApplicationController.Instance.CurrentSetup.Player);
                        Add(setup.SetupName, setup);
                    }
                }
            }
        }
        else
        {
            using (FileStream file = File.Create(_filePath)) { }
            Debug.Log("Setups file created");
        }

        //  Add a default setup if there are none
        if(_setupDictionary.Count == 0)
        {
            Debug.Log("Adding default setup");
            _setupDictionary.Add("Default", new Setup("Default", ApplicationController.Instance.CurrentSetup.Player));
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();

        //  Convert dictionary to save glob
        SetupListGlob saveGlob = new SetupListGlob(SetupList());

        using (FileStream file = File.Create(_filePath))
        {
            bf.Serialize(file, saveGlob.data);
        }

        HasUnsavedData = false;
    }

    public List<string> GetNames()
    {
        List<string> names = new List<string>();

        foreach (string name in _setupDictionary.Keys)
            names.Add(name);

        names.Sort();
        return names;
    }

    public void RenameSetup(string originalName, string newName)
    {
        //  Grab the referenced setup
        Setup referencedSetup = _setupDictionary[originalName];
        //  Change the name
        referencedSetup.SetupName = newName;

        //  Remove from old key and add to new
        _setupDictionary.Add(newName, referencedSetup);
        _setupDictionary.Remove(originalName);

        //  Update any logs that reference the old setup name
        ApplicationController.Instance.BossLogs.UpdateRenamedSetup(originalName, newName);
    }

    private List<Setup> SetupList()
    {
        List<Setup> setups = new List<Setup>();

        foreach (Setup setup in _setupDictionary.Values)
            setups.Add(setup);

        return setups;
    }

    //  IDictionary methods

    public void Add(string key, Setup value)
    {
        HasUnsavedData = true;
        ((IDictionary<string, Setup>)_setupDictionary).Add(key, value);
    }

    public bool ContainsKey(string key)
    {
        return ((IDictionary<string, Setup>)_setupDictionary).ContainsKey(key);
    }

    public bool Remove(string key)
    {
        HasUnsavedData = true;
        return ((IDictionary<string, Setup>)_setupDictionary).Remove(key);
    }

    public bool TryGetValue(string key, out Setup value)
    {
        return ((IDictionary<string, Setup>)_setupDictionary).TryGetValue(key, out value);
    }

    public void Add(KeyValuePair<string, Setup> item)
    {
        HasUnsavedData = true;
        ((IDictionary<string, Setup>)_setupDictionary).Add(item);
    }

    public void Clear()
    {
        HasUnsavedData = true;
        ((IDictionary<string, Setup>)_setupDictionary).Clear();
    }

    public bool Contains(KeyValuePair<string, Setup> item)
    {
        return ((IDictionary<string, Setup>)_setupDictionary).Contains(item);
    }

    public void CopyTo(KeyValuePair<string, Setup>[] array, int arrayIndex)
    {
        ((IDictionary<string, Setup>)_setupDictionary).CopyTo(array, arrayIndex);
    }

    public bool Remove(KeyValuePair<string, Setup> item)
    {
        HasUnsavedData = true;
        return ((IDictionary<string, Setup>)_setupDictionary).Remove(item);
    }

    public IEnumerator<KeyValuePair<string, Setup>> GetEnumerator()
    {
        return ((IDictionary<string, Setup>)_setupDictionary).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IDictionary<string, Setup>)_setupDictionary).GetEnumerator();
    }
}
