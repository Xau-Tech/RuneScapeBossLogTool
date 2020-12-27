using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotView : AbsSetupItemSlotView, IPointerClickHandler
{
    public int inventorySlotNumber { private get; set; }

    [SerializeField] private Image itemImage;
    [SerializeField] private Text quantityText;
    private Sprite defaultSprite;

    public void Init(in int inventorySlotNumber)
    {
        defaultSprite = itemImage.sprite;
        slotCategory = ItemSlotCategories.Inventory;
        itemSlot = new ItemSlot(General.NullItem(), 1);
        this.inventorySlotNumber = inventorySlotNumber;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        base.OnClick(in eventData, inventorySlotNumber);
    }

    public void Display(in SetupItem setupItem, uint quantity)
    {
        //  Display base item sprite
        base.Display(in setupItem, in itemImage);

        //  If stackable, show and update quantity text for item slot
        if (setupItem.isStackable)
        {
            quantityText.enabled = true;
            quantityText.text = quantity + "";
        }
        //  Otherwise disable quantity text
        else
        {
            quantityText.enabled = false;
        }
    }

    public override Sprite GetDefaultSprite()
    {
        return defaultSprite;
    }
}
