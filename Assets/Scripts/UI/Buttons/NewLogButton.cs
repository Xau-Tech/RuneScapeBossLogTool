using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Button to open the window used for adding new BossLogs
public class NewLogButton : MonoBehaviour
{
    private Button thisButton;

    private void Awake()
    {
        thisButton = GetComponent<Button>();

        if (!thisButton)
            throw new System.Exception($"NewLog.cs is not attached to a button gameobject!");
        else
            thisButton.onClick.AddListener(OpenNewLogWindow);
    }

    private void OpenNewLogWindow()
    {
        NewLogWindow.Instance.OpenWindow(ProgramState.CurrentState);
    }
}
