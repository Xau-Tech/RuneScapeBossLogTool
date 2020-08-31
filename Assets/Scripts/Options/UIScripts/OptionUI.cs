using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

/*
 * Handles all UI objects for our options such as passing our interface used to display data
 * as well as updating our option data from UI values when prompted by user
 */
public class OptionUI : MonoBehaviour
{
    private static GameObject thisWindow;
    private static Dictionary<string, GameObject> objectDictionary = new Dictionary<string, GameObject>();
    [SerializeField] private Dropdown[] dropdownArray;
    [SerializeField] private Toggle[] toggleArray;

    public static void OpenOptionsWindow()
    {
        thisWindow.SetActive(true);
    }

    public async Task Setup()
    {
        thisWindow = this.gameObject;

        //  Populate our dictionary of option names ->  gameobjects
        for(int i = 0; i < dropdownArray.Length; ++i)
        {
            Dropdown dropdown = dropdownArray[i];

            ListOption script = dropdown.GetComponent<ListOption>();
            if (!script)
                throw new System.Exception("Option dropdown does not have a ListOption script attached!");

            objectDictionary.Add(script.GetName(), dropdown.gameObject);
        }
        for(int i = 0; i < toggleArray.Length; ++i)
        {
            Toggle toggle = toggleArray[i];

            ToggleOption script = toggle.GetComponent<ToggleOption>();
            if (!script)
                throw new System.Exception("Option toggle does not have a ToggleOption script attached!");

            objectDictionary.Add(script.GetName(), toggle.gameObject);
        }

        //  See note below
        await DisplaySelectedOptionsEvent();

        toggleArray = null;
        dropdownArray = null;
    }

    /*
     * This must be called at minimum one frame after the Monobehavior::Awake() function because of some check(s) Unity
     * does on Toggle UI objects within Awake() that causes them to be null before and up to that point even when set
     * directly within the inspector window
     */
    public async Task DisplaySelectedOptionsEvent()
    {
        await Task.Delay((int)uint.MinValue);

        //  Triggers our option UI objects to have the proper value selected from our data class
        EventManager.Instance.OptionUISetup();
    }

    //  Used by GetDisplayInterfaceByOptionName
    //  This tries to get the gameobject by option name
    private static GameObject GetOptionObjectByName(in string name)
    {
        GameObject obj = null;
        objectDictionary.TryGetValue(name, out obj);

        if (!obj)
            throw new System.Exception("No " + name + " option found!");

        return obj;
    }

    //  Returns our DisplayOption interface for use in initializing our concrete option classes
    public static IDisplayOption GetDisplayInterfaceByOptionName(in string name, in OptionData.OptionTypes optionType)
    {
        if(optionType == OptionData.OptionTypes.Dropdown)
        {
            return GetOptionObjectByName(in name).GetComponent<ListOption>();
        }
        else if(optionType == OptionData.OptionTypes.Toggle)
        {
            return GetOptionObjectByName(in name).GetComponent<ToggleOption>();
        }

        return null;
    }

    //  Update our option values from UI
    public void UpdateOptions(in List<GenericOption> optionList)
    {
        //  Iterate through gameobject dictionary
        foreach(KeyValuePair<string, GameObject> pair in objectDictionary)
        {
            //  Find the matching option within our passed in option data
            GenericOption opt = optionList.Find(option => option.GetName().CompareTo(pair.Key) == 0);
            string value = "";
            
            //  Determine how we need to get our value based on the option type
            //  And then get our value however needed from that UI object's component
            if(opt.GetOptionType() == OptionData.OptionTypes.Dropdown)
            {
                Dropdown dropdown = pair.Value.GetComponent<Dropdown>();
                if (!dropdown)
                    throw new System.Exception($"Dropdown component not found on {pair.Value.name}!\nOptionUI.cs::UpdateOptions");

                value = dropdown.options[dropdown.value].text;
            }
            else if (opt.GetOptionType() == OptionData.OptionTypes.Toggle)
            {
                Toggle toggle = pair.Value.GetComponent<Toggle>();
                if (!toggle)
                    throw new System.Exception($"Toggle component not found on {pair.Value.name}!\nOptionUI.cs::UpdateOptions");

                value = toggle.isOn.ToString();
            }
            else
                throw new System.Exception("Option type either not set or set incorrectly!\nOptionUI.cs::UpdateOptions");

            //  If the value is new and not still empty string, set our new value and apply any changes needed
            if(value != opt.GetValue() && value != "")
            {
                opt.SetValue(in value);
                opt.Apply();
                Debug.Log($"{opt.GetName()}'s value set to {value}");
            }
        }
    }
}
