using UnityEngine;
using UnityEngine.UI;

//  Button to remove the drop that was clicked on
public class RemoveDropButton : MonoBehaviour
{
    public ItemSlot itemSlot { set; private get; }

    private Button thisButton;

    private void Awake()
    {
        thisButton = GetComponent<Button>();

        if (!thisButton)
            throw new System.Exception($"RemoveDropButton.cs is not attached to a button gameobject!");
        else
            thisButton.onClick.AddListener(RemoveDrop);
    }

    //  Remove the cached drop from the data and hide this object
    private void RemoveDrop()
    {
        DataController.Instance.dropList.Remove(itemSlot);
        this.gameObject.SetActive(false);
    }
}
