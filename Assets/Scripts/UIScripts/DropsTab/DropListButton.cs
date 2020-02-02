using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//  The script used for each button on the drop list; 1 button per unique item name
public class DropListButton : MonoBehaviour
{
    [SerializeField]
    private Text m_Text;


    public void SetText(string _text)
    {
        m_Text.text = _text;
    }



    public void OnClick()
    {
        //  Set the clicked button as the active button; the index of this button will be used to remove that item if remove is clicked
        UIController.Instance.DropListController.ActiveButton = this.gameObject;

        //  Activate the RemoveDropButton and move it to centered on the mouse position
        UIController.Instance.RemoveDropButton.gameObject.SetActive(true);
        UIController.Instance.RemoveDropButton.transform.position = Input.mousePosition;
    }
}
