using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Toggle option UI
/// </summary>
public class ToggleOption : MonoBehaviour, IDisplayOption
{
    //  Properties & fields
    public string Name { get { return _optionName; } }

    [SerializeField] private string _optionName;
    private Toggle _thisToggle;

    //  Methods

    private void Awake()
    {
        _thisToggle = GetComponent<Toggle>();
    }

    //  Set toggle object to saved value
    public void DisplayChoice(string choice)
    {
        //  Check if passed string can be converted to bool
        if (bool.TryParse(choice, out bool flag))
            _thisToggle.isOn = flag;
    }

    //  Nothing to populate for a toggle
    public void PopulateChoices(List<string> choices) { }
}
