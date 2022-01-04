using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract option class
/// </summary>
public abstract class GenericOption
{
    //  Properties and fields
    public string Name { get; protected set; }
    public string Value { get; set; }
    public Enums.OptionTypes OptionType { get; protected set; }
    public string[] OptionChoices { set { _optionChoices = value; } }

    protected IDisplayOption _displayInterface;

    private string[] _optionChoices;

    //  Constructor
    public GenericOption(IDisplayOption displayInterface)
    {
        Value = "";
        this._displayInterface = displayInterface;
        _optionChoices = null;
    }

    //  Methods

    public List<string> GetOptionChoices()
    {
        List<string> choices = new List<string>();

        foreach(string choice in _optionChoices)
        {
            choices.Add(choice);
        }

        return choices;
    }

    public bool IsValidChoice(string choice)
    {
        for(int i = 0; i < _optionChoices.Length; ++i)
        {
            if (_optionChoices[i].ToLower().CompareTo(choice.ToLower()) == 0)
                return true;
        }

        return false;
    }

    public override string ToString()
    {
        return (Name + "=" + Value);
    }

    //  Abstract methods
    public abstract void Apply();
    public abstract void PopulateChoices();
    public abstract void DisplayChoice();
}
