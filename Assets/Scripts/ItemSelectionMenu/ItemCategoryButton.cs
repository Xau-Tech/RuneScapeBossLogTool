using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//  Button created for SetupItemCategories in the SetupItemMenu
public class ItemCategoryButton : MonoBehaviour, IPointerEnterHandler
{
    private Button thisButton;
    private SetupItemCategories itemCategory;

    private void Awake()
    {
        if (!(thisButton = GetComponent<Button>()))
            throw new System.Exception($"ItemCategoryButton.cs is not attached to a button gameobject!");
    }

    //  Set item category and button text
    public void Setup(in SetupItemCategories setupItemCategory)
    {
        this.itemCategory = setupItemCategory;
        GetComponentInChildren<Text>().text = "\t<-\t" + itemCategory.ToString();
    }

    //  OnPointerEnter event
    public void OnPointerEnter(PointerEventData eventData)
    {
        List<SetupItemCategories> categories;

        //  Check if this button's Category has children
        //  Add submenu of SetupItemCategory if true; SetupItems if false
        if (SetupItemTypes.TryGetSubcategories(in itemCategory, out categories))
        {
            UIController.Instance.SetupItemMenu.AddSubmenu(in categories, 
                gameObject.transform.GetComponentInParent<SetupItemSubMenu>().StackValue, 
                gameObject.transform.position.y);
        }
        else
        {
            UIController.Instance.SetupItemMenu.AddSubmenu(SetupItemsDictionary.GetItems(in itemCategory), 
                gameObject.transform.GetComponentInParent<SetupItemSubMenu>().StackValue, 
                gameObject.transform.position.y);
        }
    }
}
