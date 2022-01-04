using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Data object for all options
/// </summary>
public class Options
{
    //  Properties & fields
    private static Dictionary<string, GenericOption> _optionDictionary;

    private static string OPTIONFILEPATH;

    //  Constructor
    public Options()
    {
        _optionDictionary = new Dictionary<string, GenericOption>();
    }

    ~Options()
    {
        EventManager.Instance.onOptionUISetup -= DisplayCurrentSelectedOptions;
    }

    //  Methods
    public void Setup()
    {
        if (Application.isEditor)
            OPTIONFILEPATH = Application.persistentDataPath + "/testOptions.ini";
        else
            OPTIONFILEPATH = Application.persistentDataPath + "/options.ini";

        EventManager.Instance.onOptionUISetup += DisplayCurrentSelectedOptions;

        CreateOptions();
        FillOptions();
        LoadOptions();
    }

    private void CreateOptions()
    {
        OptionFactory optionFactory = new OptionFactory();
        for(int i = 0; i < OptionData.Options.Length; ++i)
        {
            string name = OptionData.Options[i];

            //  Make sure we don't already have an option with the same name
            if (_optionDictionary.ContainsKey(name))
                throw new System.Exception($"There is already a {name} option!");

            GenericOption op = optionFactory.BuildOption(name);
            _optionDictionary.Add(op.Name, op);
        }
    }

    //  Fill UI objects with each option's valid choices
    private void FillOptions()
    {
        foreach(GenericOption option in _optionDictionary.Values)
        {
            option.PopulateChoices();
        }
    }

    //  Load our saved option values from file
    private void LoadOptions()
    {
        string fileLine;

        //  Check option file exists
        if (File.Exists(OPTIONFILEPATH))
        {
            using (StreamReader sr = new StreamReader(OPTIONFILEPATH))
            {
                //  Parse each line in the file
                while((fileLine = sr.ReadLine()) != null)
                {
                    //  Should be format of [OptionName]=[OptionValue]
                    string[] values = fileLine.Split('=');

                    if (values.Length != 2)
                        continue;

                    //  Make sure option name matches one of our listed options
                    if (!OptionData.IsOption(values[0]))
                        continue;

                    //  Get our option from data
                    _optionDictionary.TryGetValue(values[0], out GenericOption option);

                    //  Make sure value is valid selection for that option
                    if (option.IsValidChoice(values[1]))
                        option.Value = values[1];
                    else
                        Debug.Log($"{values[0]} has an invalid choice from file!");
                }
            }
        }

        //  Re-save options to file to fix any errors or improper data detected on load
        SaveOptions();
        DisplayCurrentSelectedOptions();
        ApplyOptionUpdates();
    }

    public void SaveOptions()
    {
        using (StreamWriter sw = new StreamWriter(OPTIONFILEPATH))
        {
            foreach (var option in _optionDictionary.Values)
                sw.WriteLine(option.ToString());
        }
    }

    public string GetOptionValue(Enums.OptionNames optionName)
    {
        string name = optionName.ToString();
        _optionDictionary.TryGetValue(name, out GenericOption option);

        if (option != null)
            return option.Value;
        else
            return "ERROR";
    }

    //  Fill UI with each option's currently selected value
    private void DisplayCurrentSelectedOptions()
    {
        foreach(GenericOption option in _optionDictionary.Values)
        {
            option.DisplayChoice();
            Debug.Log($"Displaying {option.Value} value for the {option.Name} option.");
        }
    }

    //  Apply effects of option changes
    private void ApplyOptionUpdates()
    {
        foreach(GenericOption option in _optionDictionary.Values)
        {
            option.Apply();
        }
    }

    //  Get our list of options
    public List<GenericOption> GetOptionList()
    {
        List<GenericOption> optionList = new List<GenericOption>();

        foreach (GenericOption option in _optionDictionary.Values)
        {
            optionList.Add(option);
        }

        return optionList;
    }

    //  Get the name of the RareDropTable sheet in Google Docs based on RSVersion option
    public static string RareDropTableName()
    {
        if (!_optionDictionary.ContainsKey(RSVersionOption.NAME))
            throw new System.Exception($"Options dictionary does not include an RSVersionOption!");

        GenericOption go;
        _optionDictionary.TryGetValue(RSVersionOption.NAME, out go);

        if (go.Value.ToLower().CompareTo("rs3") == 0)
            return "Rare Drop Table";
        else
            return "OS Rare Drop Table";
    }
}
