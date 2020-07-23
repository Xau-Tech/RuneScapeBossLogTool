using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Button to add an item to the drop list
public class AddItemButton : MonoBehaviour
{
    private Button thisButton;
    [SerializeField] private Dropdown itemDropdown;
    [SerializeField] private InputField itemAmountInputField;

    private void Awake()
    {
        thisButton = GetComponent<Button>();

        if (!thisButton)
            throw new System.Exception($"AddItemButton.cs is not attached to a button gameobject!");
        else
            thisButton.onClick.AddListener(AddItem);

        //  Set our input field's action on enter press
        itemAmountInputField.GetComponent<InputFieldOnEndEnter>().endEditAction = AddItem;
    }

    public void AddItem()
    {
        ushort itemAmount;

        //  Check to ensure proper input
        if(!ushort.TryParse(itemAmountInputField.text, out itemAmount))
        {
            InputWarningWindow.Instance.OpenWindow($"You must enter a value from 0 to {ushort.MaxValue}!");
            itemAmountInputField.text = "";
            return;
        }

        //  Get a reference to the selected item
        Item item = DataController.Instance.itemList.AtIndex(itemDropdown.value);

        if((ulong)itemAmount * item.price > ulong.MaxValue)
        {
            InputWarningWindow.Instance.OpenWindow($"Cannot add {itemAmount} {item.name}!\nMax loot value is {ulong.MaxValue}.");
            return;
        }

        //  Item is already in the droplist so update our drop object
        if (DataController.Instance.dropList.Exists(item.name))
        {
            DataController.Instance.dropList.AddToDrop(item.name, in itemAmount);
        }
        //  Item is not yet in the drop list so add it as a new drop
        else
        {
            DataController.Instance.dropList.Add(new Drop(item, itemAmount));
        }

        itemAmountInputField.text = "";
        //  Jump back to itemdropdown - QoL to make adding lots of items at once easier and accessible using only the keyboard
        itemDropdown.Select();
        DataController.Instance.dropList.Print();
    }
}
