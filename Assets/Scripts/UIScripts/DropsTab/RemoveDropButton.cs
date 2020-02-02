using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RemoveDropButton : MonoBehaviour, IPointerExitHandler
{
    private void OnEnable()
    {
        EventManager.Instance.onRemoveDropClicked += RemoveDrop;
    }

    private void OnDisable()
    {
        EventManager.Instance.onRemoveDropClicked -= RemoveDrop;
    }

    private void RemoveDrop()
    {
        //  Remove the drop from the list based on the index of the activebutton which was set upon clicking the drop button
        DataController.Instance.DropList.data.RemoveAt(UIController.Instance.DropListController.DropListButtons.IndexOf(
                UIController.Instance.DropListController.ActiveButton));

        //  Update and print the list
        UIController.Instance.DropListController.GenerateList();

        //  Deactivate this object
        this.gameObject.SetActive(false);
    }


    //  Deactivate this object if the pointer exits the button area
    public void OnPointerExit(PointerEventData eventData)
    {
        this.gameObject.SetActive(false);
    }
}
