using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// List option UI
/// </summary>
public class ListOption : MonoBehaviour, IDisplayOption
{
    //  Properties & fields
    public string Name { get { return _optionName; } }

    [SerializeField] private string _optionName;
    private Dropdown thisDropdown;

    //  Methods
    private void Awake()
    {
        thisDropdown = GetComponent<Dropdown>();
    }

    //  Display saved choice
    public void DisplayChoice(string choice)
    {
        //  Iterate through our dropdown choices to find a match and select it
        for(int i = 0; i < thisDropdown.options.Count; ++i)
        {
            if(thisDropdown.options[i].text.CompareTo(choice) == 0)
            {
                thisDropdown.value = i;
                return;
            }
        }

        //  If no match, select 0
        thisDropdown.value = 0;
    }

    //  Fill the dropdown with choices
    public void PopulateChoices(List<string> choices)
    {
        if (choices == null)
            return;

        //  Clear and add options
        thisDropdown.ClearOptions();
        thisDropdown.AddOptions(choices);
    }
}
