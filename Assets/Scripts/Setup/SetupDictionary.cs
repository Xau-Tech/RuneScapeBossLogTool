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

    public bool TryGetSetup(in string setupName, out Setup setup)
    {
        setup = null;

        if (setupDictionary.TryGetValue(setupName, out setup))
            return true;
        else
            return false;
    }
}
