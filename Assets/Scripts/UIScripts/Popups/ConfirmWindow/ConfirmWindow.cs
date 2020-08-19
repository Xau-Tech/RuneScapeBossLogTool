using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ConfirmWindow : MonoBehaviour
{
    public static ConfirmWindow Instance;

    [SerializeField] private GameObject confirmWindow;
    [SerializeField] private Text promptText;
    private Action<UserResponse, string> callback;
    private string passedData;
    private bool hasUserClickedOption, userChoice;

    public ConfirmWindow()
    {
        Instance = this;
    }

    public void NewConfirmWindow(in string message, in Action<UserResponse, string> callback, in string passedData = "")
    {
        this.passedData = passedData;
        PopupState.CurrentState = PopupState.states.Confirm;
        confirmWindow.SetActive(true);
        this.callback = callback;
        StartCoroutine(ShowNewConfirmWindow(message));
    }

    private IEnumerator ShowNewConfirmWindow(string message)
    {
        promptText.text = message;

        hasUserClickedOption = false;
        userChoice = false;

        while (!hasUserClickedOption)
            yield return 0;

        callback(new UserResponse(userChoice), passedData);
        HidePopUp();
    }

    private void HidePopUp()
    {
        confirmWindow.SetActive(false);
        PopupState.CurrentState = PopupState.states.None;
    }

    public void Yes()
    {
        hasUserClickedOption = true;
        userChoice = true;
    }

    public void No()
    {
        hasUserClickedOption = true;
        userChoice = false;
    }
}

public class UserResponse
{
    public bool userResponse { get; private set; }

    public UserResponse(bool userResponse)
    {
        this.userResponse = userResponse;
    }
}
