using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Save button in the menubar
public class SaveButton : MonoBehaviour
{
    private Button thisButton;

    private void Awake()
    {
        thisButton = GetComponent<Button>();

        if (!thisButton)
            throw new System.Exception($"SaveButton.cs is not attached to a button gameobject!");
        else
            thisButton.onClick.AddListener(Save);
    }

    private void Save()
    {
        DataController.Instance.SaveBossLogData();
    }
}
