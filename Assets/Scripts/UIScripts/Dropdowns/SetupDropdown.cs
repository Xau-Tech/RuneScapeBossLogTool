using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupDropdown : MonoBehaviour
{
    private Dropdown thisDropdown;

    private void Awake()
    {
        if (!(thisDropdown = (GetComponent<Dropdown>())))
            throw new System.Exception($"SetupDropdown.cs is not attached to a dropdown gameobject!");
        else
            thisDropdown.onValueChanged.AddListener(SetupChanged);
    }

    private void OnEnable()
    {
        EventManager.Instance.onSetupAdded += SetupAdded;
        EventManager.Instance.onSetupDeleted += FillDropdown;
        EventManager.Instance.onDataLoaded += FillDropdown;
    }

    private void OnDisable()
    {
        EventManager.Instance.onSetupAdded -= SetupAdded;
        EventManager.Instance.onSetupDeleted -= FillDropdown;
        EventManager.Instance.onDataLoaded -= FillDropdown;
    }

    private void FillDropdown()
    {
        //  Fill dropdown with all setup names
        thisDropdown.options.Clear();
        thisDropdown.AddOptions(DataController.Instance.setupDictionary.GetSetupNames());

        //  Set to top option
        thisDropdown.value = 0;
        SetupChanged(0);
    }

    private void SetupAdded(string value)
    {
        thisDropdown.options.Clear();

        //  Fill w/ SetupNames & set value to new name
        List<string> setupNames = DataController.Instance.setupDictionary.GetSetupNames();
        thisDropdown.AddOptions(setupNames);
        thisDropdown.value = setupNames.IndexOf(value);

        if(thisDropdown.options[thisDropdown.value].text.CompareTo(CacheManager.SetupTab.Setup.CurrentSetupName) != 0)
            SetupChanged(thisDropdown.value);
    }

    private void SetupChanged(int value)
    {
        string setupName = thisDropdown.options[thisDropdown.value].text;

        Setup newSetup;

        if (!DataController.Instance.setupDictionary.TryGetValue(setupName, out newSetup))
            throw new System.Exception($"{setupName} could not be found in SetupDictionary!");

        CacheManager.SetupTab.Setup.SwitchSetup(in newSetup);
    }
}
