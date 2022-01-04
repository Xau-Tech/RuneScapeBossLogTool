using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for displaying and populating option data
/// </summary>
public interface IDisplayOption
{
    void DisplayChoice(string choice);
    void PopulateChoices(List<string> choices);
}
