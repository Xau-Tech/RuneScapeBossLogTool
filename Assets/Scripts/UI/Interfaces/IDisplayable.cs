using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Interface for any general info that is displayable in UI
public interface IDisplayable<T>
{
    void Display(in T value);
}
