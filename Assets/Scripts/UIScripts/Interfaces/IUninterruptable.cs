using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Used on UI objects that can't be de-selected by the TabNavigateSelectable system in certain circumstances
public interface IUninterruptable
{
    bool CanDeselect();
}
