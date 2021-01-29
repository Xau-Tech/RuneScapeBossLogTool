using UnityEngine;
using UnityEngine.UI;

public class RareItemButton : MonoBehaviour, IDisplayable<RareItem>
{
    [SerializeField] private Text itemNameText;
    [SerializeField] private Text itemQuantityText;
    [SerializeField] private Image itemImage;
    [SerializeField] RareItemScrollListController rareListController;

    public void Display(in RareItem value)
    {
        //  Show the item name and quantity
        itemNameText.text = $"{value.GetName()}";
        itemQuantityText.text = value.quantity + "";

        itemImage.sprite = rareListController.GetRareItemSprite(in value);
    }
}
