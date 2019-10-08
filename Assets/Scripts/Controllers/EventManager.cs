using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  Class for various UI events
public class EventManager : MonoBehaviour
{
    //  Event for when the boss list dropdown is changed within the drops tab
    public delegate void OnValueChanged();
    public static event OnValueChanged OnBossListDropdownChanged;

    public void DropdownChanged()
    {
        if(OnBossListDropdownChanged != null)
            OnBossListDropdownChanged();
    }


    //  Event for the add drop button being clicked within the drops tab
    public delegate void OnAddClicked();
    public static event OnAddClicked OnAddButtonClicked;

    public void AddClicked()
    {
        if (OnAddButtonClicked != null)
            OnAddButtonClicked();
    }
}
