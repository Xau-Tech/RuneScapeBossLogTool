using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SetupDictionary : IDictionary<string, Setup>
{
    public bool HasUnsavedData { get; set; }

    private Dictionary<string, Setup> setupDictionary;

    private readonly string filePath = Application.persistentDataPath;

    public SetupDictionary()
    {
        setupDictionary = new Dictionary<string, Setup>();
        HasUnsavedData = false;
    }

    public void Load()
    {
        string path = filePath;

        if (Application.isEditor)
            path += "/testSetups.dat";
        else
            path += "/setups.dat";

        //  File exists
        if (File.Exists(path))
        {
            using (FileStream file = File.Open(path, FileMode.Open))
            {
                //  Make sure file isn't empty
                if (file.Length != 0)
                {
                    BinaryFormatter bf = new BinaryFormatter();

                    //  Deserialize
                    SetupListGlob loadGlob = new SetupListGlob();
                    loadGlob.data = (List<SetupSaveGlob>)bf.Deserialize(file);

                    Setup setup;

                    foreach(SetupSaveGlob save in loadGlob.data)
                    {
                        setup = new Setup(save, CacheManager.SetupTab.Setup.Player);
                        Add(setup.SetupName, setup);
                    }
                }
            }

            Debug.Log($"Setups loaded from file");
        }
        //  File doesn't exist
        else
        {
            using (FileStream file = File.Create(path)) { }
            Debug.Log("Setups file created");
        }

        //  Add a default setup if there are none
        if(setupDictionary.Count == 0)
        {
            Debug.Log("Adding default setup");
            setupDictionary.Add("Default", new Setup("Default", CacheManager.SetupTab.Setup.Player));
        }
    }

    public void Save()
    {
        string path = filePath;

        if (Application.isEditor)
            path += "/testSetups.dat";
        else
            path += "/setups.dat";

        //  convert dictionary to save glob
        SetupListGlob saveGlob = new SetupListGlob(SetupList());

        BinaryFormatter bf = new BinaryFormatter();

        using (FileStream file = File.Create(path))
        {
            bf.Serialize(file, saveGlob.data);
            HasUnsavedData = false;
        }
    }

    public List<string> GetSetupNames()
    {
        List<string> setupNames = new List<string>();

        foreach (string name in setupDictionary.Keys)
            setupNames.Add(name);

        setupNames.Sort();

        return setupNames;
    }

    private List<Setup> SetupList()
    {
        List<Setup> setups = new List<Setup>();

        foreach(Setup setup in Values)
        {
            setups.Add(setup);
        }

        return setups;
    }

    //  IDictionary methods

    public ICollection<string> Keys => ((IDictionary<string, Setup>)setupDictionary).Keys;

    public ICollection<Setup> Values => ((IDictionary<string, Setup>)setupDictionary).Values;

    public int Count => ((IDictionary<string, Setup>)setupDictionary).Count;

    public bool IsReadOnly => ((IDictionary<string, Setup>)setupDictionary).IsReadOnly;

    public Setup this[string key] { get => ((IDictionary<string, Setup>)setupDictionary)[key]; set => ((IDictionary<string, Setup>)setupDictionary)[key] = value; }

    public void Add(string key, Setup value)
    {
        ((IDictionary<string, Setup>)setupDictionary).Add(key, value);
        HasUnsavedData = true;
    }

    public bool ContainsKey(string key)
    {
        return ((IDictionary<string, Setup>)setupDictionary).ContainsKey(key);
    }

    public bool Remove(string key)
    {
        HasUnsavedData = true;
        return ((IDictionary<string, Setup>)setupDictionary).Remove(key);
    }

    public bool TryGetValue(string key, out Setup value)
    {
        return ((IDictionary<string, Setup>)setupDictionary).TryGetValue(key, out value);
    }

    public void Add(KeyValuePair<string, Setup> item)
    {
        ((IDictionary<string, Setup>)setupDictionary).Add(item);
        HasUnsavedData = true;
    }

    public void Clear()
    {
        ((IDictionary<string, Setup>)setupDictionary).Clear();
    }

    public bool Contains(KeyValuePair<string, Setup> item)
    {
        return ((IDictionary<string, Setup>)setupDictionary).Contains(item);
    }

    public void CopyTo(KeyValuePair<string, Setup>[] array, int arrayIndex)
    {
        ((IDictionary<string, Setup>)setupDictionary).CopyTo(array, arrayIndex);
    }

    public bool Remove(KeyValuePair<string, Setup> item)
    {
        HasUnsavedData = true;
        return ((IDictionary<string, Setup>)setupDictionary).Remove(item);
    }

    public IEnumerator<KeyValuePair<string, Setup>> GetEnumerator()
    {
        return ((IDictionary<string, Setup>)setupDictionary).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IDictionary<string, Setup>)setupDictionary).GetEnumerator();
    }
}
