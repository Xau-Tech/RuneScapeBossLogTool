using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Buttons used in the dynamic, scrollable drop list to show data - 1 button per drop
//  Can be clicked to bring up a remove button
public class DropListButton : MonoBehaviour
{
    private Button thisButton;
    private Text buttonText;
    private ItemSlot itemSlot;
    [SerializeField] private Button removeDropButton;

    private void Awake()
    {
        thisButton = GetComponent<Button>();

        if (!thisButton)
            throw new System.Exception($"DropListButton.cs is not attached to a button gameobject!");
        else
        {
            thisButton.onClick.AddListener(ShowRemoveDropButton);

            buttonText = thisButton.GetComponentInChildren<Text>();

            if (!buttonText)
                throw new System.Exception($"No text in button for DropListButton.cs script to use!");
        }
    }

    //  Associate this button with its drop
    public void SetDrop(in ItemSlot itemSlot)
    {
        this.itemSlot = itemSlot;
        SetText(itemSlot.ToString());
    }

    //  Set the button's text based on its associated drop
    private void SetText(in string text)
    {
        buttonText.text = text;
    }

    //  OnClick action to show the RemoveDropButton
    private void ShowRemoveDropButton()
    {
        removeDropButton.gameObject.SetActive(true);
        removeDropButton.transform.position = Input.mousePosition;
        removeDropButton.GetComponent<RemoveDropButton>().itemSlot = itemSlot;
    }
}
