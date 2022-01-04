using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Button created SetupItemCategories in the SetupItemMenu
/// </summary>
public class ItemCategoryButton : MonoBehaviour, IPointerEnterHandler
{
    //  Properties & fields

    private Button _thisButton;
    private Enums.SetupItemCategory _itemCategory;

    //  Monobehaviors

    private void Awake()
    {
        _thisButton = gameObject.GetComponent<Button>();
    }

    //  Methods

    public void Setup(Enums.SetupItemCategory setupItemCategory)
    {
        this._itemCategory = setupItemCategory;
        GetComponentInChildren<Text>().text = "\t<-\t" + _itemCategory.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        List<Enums.SetupItemCategory> categories;

        //  Check if this button's Category has children
        //  Add submenu of SetupItemCategory if true; SetupItems if false
        if (SetupItemTypes.TryGetSubcategories(_itemCategory, out categories))
            SetupItemMenuController.Instance.AddSubmenu(categories, gameObject.transform.GetComponentInParent<SetupItemSubMenu>().StackValue, gameObject.transform.position.y);
        else
            SetupItemMenuController.Instance.AddSubmenu(SetupItemDictionary.GetItems(_itemCategory), gameObject.transform.GetComponentInParent<SetupItemSubMenu>().StackValue, gameObject.transform.position.y);
    }
}
