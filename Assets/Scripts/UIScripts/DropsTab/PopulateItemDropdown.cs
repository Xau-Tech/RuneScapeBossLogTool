using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateItemDropdown : MonoBehaviour
{
    private Dropdown m_ThisDropdown;

    private void Awake()
    {
        if ((m_ThisDropdown = this.gameObject.GetComponent<Dropdown>()) == null)
            Debug.Log("ERROR! You have put this script on an object that does not have a dropdown component!");
    }

    private void OnEnable()
    {
        EventManager.manager.onItemsLoaded += PopulateDropdown;
    }


    private void OnDisable()
    {
        EventManager.manager.onItemsLoaded -= PopulateDropdown;
    }

    private void PopulateDropdown()
    {
        m_ThisDropdown.ClearOptions();

        m_ThisDropdown.AddOptions(DataController.dataController.ItemListClass.GetItemNames());
    }
}
