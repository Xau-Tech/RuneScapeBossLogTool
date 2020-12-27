using UnityEngine.UI;
using UnityEngine;

//  Button to confirm adding quantity of a setupitem
public class AddQuantityButton : MonoBehaviour
{
    public AddedItemData itemToAddData { private get; set; }

    private Button thisButton;
    [SerializeField] private InputField quantityInputField;
    [SerializeField] private AddQuantityWindow addQuantityWindow;

    private void Awake()
    {
        if (!(thisButton = GetComponent<Button>()))
            Debug.Log($"AddQuantityButton.cs is not attached a button gameobject!");
        else
            thisButton.onClick.AddListener(AddQuantity);

        quantityInputField.GetComponent<InputFieldOnEndEnter>().endEditAction = AddQuantity;
    }

    //  Onclick event
    private void AddQuantity()
    {
        int quantity = int.Parse(quantityInputField.text);

        //  Make sure value is a positive integer
        if(quantity < 1)
        {
            InputWarningWindow.Instance.OpenWindow($"Quantity must be an integer value greater than 0!");
            quantityInputField.text = "";
            return;
        }

        CacheManager.SetupTab.Setup.AddQuantityOfSetupItem(itemToAddData.item, (uint)quantity, itemToAddData.itemSlotCategory, itemToAddData.slotIndex);

        addQuantityWindow.CloseWindow();
    }
}

public struct AddedItemData
{
    public SetupItem item;
    public ItemSlotCategories itemSlotCategory;
    public int slotIndex;

    public AddedItemData(SetupItem item, ItemSlotCategories itemSlotCategory, int slotIndex)
    {
        this.item = item;
        this.itemSlotCategory = itemSlotCategory;
        this.slotIndex = slotIndex;
    }
}
