﻿using UnityEngine;
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
        RenameLogWindow.Instance.OpenWindow();
    }
}