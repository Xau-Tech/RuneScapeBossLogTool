using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotView : AbsSetupItemSlotView, IDisplayable<SetupItem>, IPointerClickHandler
{
    public int inventorySlotNumber { private get; set; }

    [SerializeField] private Image itemImage;
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

    public void Display(in SetupItem setupItem)
    {
        base.Display(in setupItem, in itemImage);
    }

    public override Sprite GetDefaultSprite()
    {
        return defaultSprite;
    }
}
