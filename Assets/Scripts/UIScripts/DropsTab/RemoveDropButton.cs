using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RemoveDropButton : MonoBehaviour, IPointerExitHandler
{
    private void OnEnable()
    {
        EventManager.manager.onRemoveDropClicked += RemoveDrop;
    }

    private void OnDisable()
    {
        EventManager.manager.onRemoveDropClicked -= RemoveDrop;
    }

    private void RemoveDrop()
    {
        //  Remove the drop from the list based on the index of the activebutton which was set upon clicking the drop button
        DataController.dataController.DropListClass.DropList.RemoveAt(
            UIController.uicontroller.DropListController.DropListButtons.IndexOf(
                UIController.uicontroller.DropListController.ActiveButton));

        //  Update and print the list
        UIController.uicontroller.DropListController.GenerateList();

        //  Deactivate this object
        this.gameObject.SetActive(false);
    }


    //  Deactivate this object if the pointer exits the button area
    public void OnPointerExit(PointerEventData eventData)
    {
        this.gameObject.SetActive(false);
    }
}
