using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//  Class for each submenu of the SetupItemMenu
public class SetupItemSubMenu : MonoBehaviour
{
    public int StackValue { get; private set; }
    
    private List<GameObject> buttonList = new List<GameObject>();
    [SerializeField] private GameObject itemCategoryButtonTemplate;
    [SerializeField] private GameObject setupItemButtonTemplate;

    //  Clear done before Setup
    private void Clear()
    {
        for (int i = 0; i < buttonList.Count; ++i)
            Destroy(buttonList[i]);

        buttonList.Clear();
    }

    //  Setup using passed list of SetupItemCategories
    public void Setup(in List<SetupItemCategories> itemCategories, in int stackValue)
    {
        this.StackValue = stackValue;
        Clear();

        //  Create and setup Button & text for each value
        for(int i = 0; i < itemCategories.Count; ++i)
        {
            GameObject button = Instantiate(itemCategoryButtonTemplate) as GameObject;
            buttonList.Add(button);
            button.SetActive(true);
            button.GetComponent<ItemCategoryButton>().Setup(itemCategories[i]);

            button.transform.SetParent(itemCategoryButtonTemplate.transform.parent, false);
        }
        
        //  Set scrollbar to be at the top
        gameObject.GetComponent<ScrollRect>().verticalScrollbar.value = 1.0f;
    }

    //  Setup using passed list of SetupItems
    public void Setup(in List<SetupItem> items, in int stackValue)
    {
        this.StackValue = stackValue;
        Clear();

        //  Create and setup Button & text for each value
        for (int i = 0; i < items.Count; ++i)
        {
            GameObject button = Instantiate(setupItemButtonTemplate) as GameObject;
            buttonList.Add(button);
            button.SetActive(true);
            button.GetComponentInChildren<Text>().text = items[i].ItemName;

            button.transform.SetParent(setupItemButtonTemplate.transform.parent, false);
        }

        //  Set scrollbar to be at the top
        gameObject.GetComponent<ScrollRect>().verticalScrollbar.value = 1.0f;
    }
}
