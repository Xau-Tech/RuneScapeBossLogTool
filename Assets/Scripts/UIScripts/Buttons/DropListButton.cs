using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//  Buttons used in the dynamic, scrollable drop list to show data - 1 button per drop
//  Can be clicked to bring up a remove button
public class DropListButton : MonoBehaviour, IPointerClickHandler
{
    private Button thisButton;
    private Text buttonText;
    private ItemSlot itemSlot;
    [SerializeField] private Button removeDropButton;
    [SerializeField] private GameObject lastAddedNote;

    private void Awake()
    {
        thisButton = GetComponent<Button>();

        if (!thisButton)
            throw new System.Exception($"DropListButton.cs is not attached to a button gameobject!");
        else
        {
            ((IPointerClickHandler)thisButton).OnPointerClick(new PointerEventData(EventSystem.current));

            buttonText = thisButton.GetComponentInChildren<Text>();

            if (!buttonText)
                throw new System.Exception($"No text in button for DropListButton.cs script to use!");
        }
    }

    //  Associate this button with its drop
    public void SetDrop(in ItemSlot itemSlot, bool lastAddition)
    {
        this.itemSlot = itemSlot;
        SetText(itemSlot.ToString());

        //  Show note about this being the just added/updated item
        if (lastAddition)
            lastAddedNote.SetActive(true);
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

    public void OnPointerClick(PointerEventData eventData)
    {
        //  Show remove drop button on right click
        if (eventData.button == PointerEventData.InputButton.Right)
            ShowRemoveDropButton();
    }
}
