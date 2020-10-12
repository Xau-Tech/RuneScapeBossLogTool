using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//  Button to remove an item from setup when right-clicked
public class RemoveSetupItemButton : MonoBehaviour, IPointerExitHandler
{
    private Button thisButton;
    private ItemSlotCategories slotCategory;
    private int slotID;
    private Sprite defaultSlotSprite;

    private void Awake()
    {
        if (!(thisButton = GetComponent<Button>()))
            throw new System.Exception($"RemoveSetupItemButton.cs is not attached to a button gameobject!");
        else
            thisButton.onClick.AddListener(OnClick);
    }

    //  Setup and show button at proper location
    public void Show(ItemSlotCategories slotCategory, int slotID, in Sprite defaultSlotSprite)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        this.slotCategory = slotCategory;
        this.slotID = slotID;
        this.defaultSlotSprite = defaultSlotSprite;
    }

    //  Remove the selected item on click
    private void OnClick()
    {
        // Null the item at that slot category + id
        SetupItem item = General.NullItem();
        item.itemSprite = defaultSlotSprite;

        CacheManager.SetupTab.Setup.AddSetupItem(item, in slotCategory, in slotID);
        gameObject.SetActive(false);
    }

    //  Set inactive when pointer exits
    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.SetActive(false);
    }
}
