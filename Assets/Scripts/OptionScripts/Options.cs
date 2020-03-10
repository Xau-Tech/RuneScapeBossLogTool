using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//  Our collection of options
public class Options: MonoBehaviour
{
    //  Dictionary of options using the option name as a key
    public static Dictionary<string, GenericOption> optionDictionary;
    private static string OPTIONFILEPATH;

    public Options()
    {
        optionDictionary = new Dictionary<string, GenericOption>();
        AddOption(new ResolutionOption());
        AddOption(new SyncBossOption());
    }

    public static void Setup()
    {
        OPTIONFILEPATH = Application.persistentDataPath + "/options.ini";
        LoadOptions();
    }

    private static void LoadOptions()
    {
        //  Option file exists
        if (File.Exists(OPTIONFILEPATH))
        {
            SimpleOutput.Instance.Print("file exists");
        }
        //  File does not exist
        else
            SaveOptions();
    }

    public static void SaveOptions()
    {
        //  Set state to saving
        // UNCOMMENT ON FINISH PopupState.currentState = PopupState.states.Saving;

        //  Write stuff
        using (StreamWriter sw = new StreamWriter(OPTIONFILEPATH))
        {
            foreach (var option in optionDictionary.Values)
                sw.WriteLine(option.ToString());
        }
    }

    //  Wrapper for Dictionary.add
    public void AddOption(GenericOption option)
    {
        optionDictionary.Add(option.GetName(), option);
    }

    //  Wrapper for Dictionary.TryGetValue
    public GenericOption TryGetOption(string name)
    {
        GenericOption go;
        optionDictionary.TryGetValue(name, out go);
        return go;
    }
}
