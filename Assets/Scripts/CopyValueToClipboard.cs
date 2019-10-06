using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Simple copy to clipboard script to make users' life a bit simpler for data input
public class CopyValueToClipboard : MonoBehaviour
{
    private TextEditor te;

    public void OnClick()
    {
        te = new TextEditor();
        te.text = DataController.dataController.DropListClass.GetTotalValue().ToString();
        te.SelectAll();
        te.Copy();
    }
}
