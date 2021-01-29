using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Class for generic dropdown option UI display
public class ListOption : MonoBehaviour, IDisplayOption
{
    [SerializeField] private string optionName;
    private Dropdown thisDropdown;

    private void Awake()
    {
        thisDropdown = GetComponent<Dropdown>();
        if (!thisDropdown)
            throw new System.Exception("ListOption script has not been attached to a dropdown");
    }

    public string GetName()
    {
        return optionName;
    }

    //  Display our saved choice in the option UI
    public void DisplayChoice(in string choice)
    {
        // Iterate through our dropdown choices to find a match and select it
        for (int i = 0; i < thisDropdown.options.Count; ++i)
        {
            if (thisDropdown.options[i].text.CompareTo(choice) == 0)
            {
                thisDropdown.value = i;
                return;
            }
        }

        //  If no match is found simply select the 0th value
        thisDropdown.value = 0;
    }

    //  Fill the dropdown with all choices for the option
    public void PopulateChoices(in List<string> choices)
    {
        if(choices == null)
            throw new System.Exception("You've sent me no choices to populate the UI with! :<");

        //  Clear and add options
        thisDropdown.ClearOptions();
        thisDropdown.AddOptions(choices);
    }
}
