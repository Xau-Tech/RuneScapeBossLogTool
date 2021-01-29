using System.Collections.Generic;

//  Abstract class to be extended by conrete options
public abstract class GenericOption
{
    protected IDisplayOption displayInterface;

    private string[] optionChoices;
    private string name, value;
    private OptionData.OptionTypes type;

    public GenericOption(IDisplayOption displayInterface)
    {
        value = "";
        this.displayInterface = displayInterface;
        optionChoices = null;
    }

    public string GetName()
    {
        return name;
    }

    public string GetValue()
    {
        return value;
    }

    public OptionData.OptionTypes GetOptionType()
    {
        return type;
    }

    protected void SetOptionType(in OptionData.OptionTypes type)
    {
        this.type = type;
    }

    public void SetValue(in string value)
    {
        this.value = value;
    }

    protected void SetName(in string name)
    {
        this.name = name;
    }

    protected void SetChoices(in string[] choices)
    {
        this.optionChoices = choices;
    }

    //  Get a list<string> of the choices for this option -- used in filling dropdown options
    public List<string> GetChoices()
    {
        List<string> temp = new List<string>();
        
        foreach(string choice in optionChoices)
        {
            temp.Add(choice);
        }

        return temp;
    }

    //  Used to make sure values set from loaded file are valid
    public bool IsValidChoice(in string choice)
    {
        for(int i = 0; i < optionChoices.Length; ++i)
        {
            if(optionChoices[i].ToLower().CompareTo(choice.ToLower()) == 0)
            {
                return true;
            }
        }

        return false;
    }

    //  Use to print data in .ini file
    public override string ToString()
    {
        return (name + "=" + value);
    }

    public abstract void Apply();
    public abstract void PopulateChoices();
    public abstract void DisplayChoice();
}
