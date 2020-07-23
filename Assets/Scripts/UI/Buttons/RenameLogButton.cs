using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenameLogButton : MonoBehaviour
{
    private Button thisButton;

    private void Awake()
    {
        thisButton = GetComponent<Button>();

        if (!thisButton)
            throw new System.Exception($"RenameLogButton.cs is not attached to a button gameobject!");
        else
            thisButton.onClick.AddListener(RenameLog);
    }

    private void RenameLog()
    {
        //open a window to rename the log
        Debug.Log($"Renaming log: {CacheManager.currentLog}");
    }
}
