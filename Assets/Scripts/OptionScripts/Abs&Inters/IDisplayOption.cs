using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Interface to display option data to UI
public interface IDisplayOption
{
    void DisplayChoice(in string choice);
    void PopulateChoices(in List<string> choices);
}
