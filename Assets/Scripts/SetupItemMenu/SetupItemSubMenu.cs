using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for each submenu of the SetupItemMenu
/// </summary>
public class SetupItemSubMenu : MonoBehaviour
{
    //  Properties & fields
    
    public int StackValue { get; private set; }

    private List<GameObject> _buttonList = new List<GameObject>();
    [SerializeField] private GameObject _itemCategoryButtonTemplate;
    [SerializeField] private GameObject _setupItemButtonTemplate;

    //  Methods

    private void Clear()
    {
        for (int i = 0; i < _buttonList.Count; ++i)
        {
            Destroy(_buttonList[i]);
        }

        _buttonList.Clear();
    }

    //  Setup using passed list of SetupItemCategories
    public void Setup(List<Enums.SetupItemCategory> itemCategories, int stackValue)
    {
        this.StackValue = stackValue;
        Clear();

        //  Create and setup button & text for each value
        for (int i = 0; i < itemCategories.Count; ++i)
        {
            GameObject button = Instantiate(_itemCategoryButtonTemplate) as GameObject;
            _buttonList.Add(button);
            button.SetActive(true);
            button.GetComponent<ItemCategoryButton>().Setup(itemCategories[i]);
            button.transform.SetParent(_itemCategoryButtonTemplate.transform.parent, false);
        }

        //  Set scroll to be at the top
        gameObject.GetComponent<ScrollRect>().verticalScrollbar.value = 1.0f;
    }

    //  Setup using passed list of SetupItems
    public void Setup(List<SetupItemStruct> itemStructs, int stackValue)
    {
        this.StackValue = stackValue;
        Clear();

        //  Create and setup button & text for each value
        for (int i = 0; i < itemStructs.Count; ++i)
        {
            GameObject button = Instantiate(_setupItemButtonTemplate) as GameObject;
            _buttonList.Add(button);
            button.SetActive(true);
            button.GetComponentInChildren<SetupItemButton>().Setup(itemStructs[i]);
            button.transform.SetParent(_setupItemButtonTemplate.transform.parent, false);
        }

        //  Set scrollbar to be at the top
        gameObject.GetComponent<ScrollRect>().verticalScrollbar.value = 1.0f;
    }
}
