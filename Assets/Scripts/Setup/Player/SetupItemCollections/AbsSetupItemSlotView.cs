using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class AbsSetupItemSlotView : MonoBehaviour
{
    protected ItemSlot item;
    protected ItemSlotCategories slotCategory;

    protected void OnClick(in PointerEventData eventData, in int slotID)
    {
        Debug.Log($"Base OnClick [ SlotType = {slotCategory}, SlotID: {slotID}");

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if(item == null)
            {
                List<SetupItemCategories> categories;

                if(SetupItemTypes.TryGetSubcategories(in slotCategory, out categories))
                    UIController.Instance.SetupItemMenu.NewMenu(in slotCategory, in slotID, in categories);
                else
                {
                    //TODO: ITEM SELECTION MENU WITH ITEMS
                }
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if(item != null)
            {
                //  TODO: REMOVE ITEM CODE
            }
        }
    }
}

public enum ItemSlotCategories { Inventory, Food };
