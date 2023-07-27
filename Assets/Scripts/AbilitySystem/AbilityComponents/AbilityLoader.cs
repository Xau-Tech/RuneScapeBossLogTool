using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;

/// <summary>
/// Loader class for abilities
/// Will be expanded much more later probably into a builder pattern when effects are added but this is all I should need for now
/// </summary>
public class AbilityLoader
{
    private readonly string M_FILEPATH = "/Data/Abilities.json";

    public AbilityLoader()
    {

    }

    /// <summary>
    /// Load abilities from JSON file into memory
    /// </summary>
    /// <returns>A dictionary with Ability Ids as the key their respective Ability as the corresponding value</returns>
    public Dictionary<string, Ability> LoadAbilities()
    {
        Dictionary<string, Ability> abilDict = new();
        string filepath = Application.streamingAssetsPath + M_FILEPATH;

        try
        {
            using FileStream file = File.Open(filepath, FileMode.Open);
            using StreamReader sr = new(file);

            //  Read in whole file and then separate get the array component
            JObject abilFileText = JObject.Parse(sr.ReadToEnd());
            JToken abilArray = abilFileText["abilities"];

            //  Each JToken child should be its own ability
            foreach (var abilJson in abilArray.Children())
            {
                Ability newAbil = new(abilJson);

                //  Error check for duplicates - on the off chance people want to edit the JSON file, I'll leave this here in production as opposed to Editor only error checking
                if (abilDict.ContainsKey(newAbil.Id))
                    throw new System.Exception($"ERROR: Duplicate ability with id of [{newAbil.Id}] attempted to be added!");
                else
                    abilDict.Add(newAbil.Id, newAbil);
            }
        }
        catch(FileNotFoundException ex)
        {
            throw new FileNotFoundException($"ERROR: The ability file has been deleted from the system!\n{ex.Message}");
        }
        catch(IOException ex)
        {
            throw new IOException($"ERROR: Ability file could not be loaded!  Please try restarting the application!\n{ex.Message}");
        }
        catch(System.Exception ex)
        {
            throw new System.Exception($"ERROR: {ex.Message}");
        }

        return abilDict;
    }
}