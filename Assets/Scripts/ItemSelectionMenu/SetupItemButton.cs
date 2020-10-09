using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//  Button created for SetupItems in the SetupItemMenu
public class SetupItemButton : MonoBehaviour, IPointerClickHandler
{
    private Button thisButton;
    private SetupItemStruct setupItemStruct;

    private void Awake()
    {
        if (!(thisButton = GetComponent<Button>()))
            throw new System.Exception($"ItemCategoryButton.cs is not attached to a button gameobject!");
    }

    //  Set item category and button text
    public void Setup(in SetupItemStruct setupItemStruct)
    {
        this.setupItemStruct = setupItemStruct;
        GetComponentInChildren<Text>().text = setupItemStruct.itemName;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SetupItemMenuController menu = gameObject.GetComponentInParent<SetupItemMenuController>();

        SetupItem setupItem;

        if (SetupItemsDictionary.TryGetItem(in setupItemStruct.itemID, out setupItem))
            CacheManager.SetupTab.Setup.AddSetupItem(in setupItem, menu.ItemSlotCategory, menu.ClickedSlotID);

        //  Close the menu
        menu.OnPointerExit(new PointerEventData(EventSystem.current));
    }
}
