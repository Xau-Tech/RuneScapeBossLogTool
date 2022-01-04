using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Display a RareItem to a button UI
/// </summary>
public class RareItemButton : MonoBehaviour
{
    //  Properties & fields

    [SerializeField] private Text _itemNameText;
    [SerializeField] private Text _itemQuantityText;
    [SerializeField] private Image _itemImage;

    //  Methods

    public void Display(RareItem rareItem, Sprite rareItemImage, string bossName)
    {
        //  Show the item name and quantity
        _itemNameText.text = $"{rareItem.GetName(bossName)}";
        _itemQuantityText.text = rareItem.quantity + "";
        _itemImage.sprite = rareItemImage;
    }
}
