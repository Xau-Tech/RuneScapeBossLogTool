using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupDictionary
{
    public SetupDictionary()
    {
        setupDictionary = new Dictionary<string, Setup>();
    }

    private Dictionary<string, Setup> setupDictionary;

    public Setup GetSetup(in string setupName)
    {
        Setup setup;
        setupDictionary.TryGetValue(setupName, out setup);
        return setup;
    }
}
