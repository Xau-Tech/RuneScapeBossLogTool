using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDropdown : MonoBehaviour
{
    private Dropdown m_ThisDropdown;

    private void Awake()
    {
        m_ThisDropdown = GetComponent<Dropdown>();
    }

    private void OnEnable()
    {
        EventManager.Instance.onItemsLoaded += PopulateDropdown;
        EventManager.Instance.onUIReset += SetToTopValue;
    }


    private void OnDisable()
    {
        EventManager.Instance.onItemsLoaded -= PopulateDropdown;
        EventManager.Instance.onUIReset -= SetToTopValue;
    }

    private void PopulateDropdown()
    {
        m_ThisDropdown.ClearOptions();
        m_ThisDropdown.AddOptions(DataController.Instance.ItemList.GetItemNames());

        //  End of loading process
        PopupState.currentState = PopupState.states.None;
    }

    //  Select the top option
    private void SetToTopValue()
    {
        m_ThisDropdown.value = 0;
    }
}
