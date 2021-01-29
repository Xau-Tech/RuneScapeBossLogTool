using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Class for generic toggle option UI display
public class ToggleOption : MonoBehaviour, IDisplayOption
{
    [SerializeField] private string optionName;
    private Toggle thisToggle;

    private void Awake()
    {
        thisToggle = GetComponent<Toggle>();
        if (thisToggle == null)
            throw new System.Exception("Toggle object not set in ToggleOption.cs");
    }

    public string GetName()
    {
        return optionName;
    }

    //  Set toggle object to our saved value
    public void DisplayChoice(in string choice)
    {
        bool flag;

        //  Check if passed string can be converted to valid bool
        if (!bool.TryParse(choice, out flag))
            throw new System.Exception("Toggle value cannot be converted to bool");

        thisToggle.isOn = flag;
    }

    //  Nothing to populate for a toggle
    public void PopulateChoices(in List<string> choices) { }
}
