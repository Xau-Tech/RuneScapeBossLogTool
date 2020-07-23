using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Button to add a new BossLog
public class AddLogButton : MonoBehaviour
{
    private Button thisButton;
    [SerializeField] private InputField logInputField;

    private void Awake()
    {
        thisButton = GetComponent<Button>();

        if (!thisButton)
            throw new System.Exception($"AddLogButton.cs is not attached to a gameobject!");
        else
            thisButton.onClick.AddListener(AddButtonListener);

        logInputField.GetComponent<InputFieldOnEndEnter>().endEditAction = AddButtonListener;
    }

    private void AddButtonListener()
    {
        AddLog(logInputField.text);
    }

    private void AddLog(string inputValue)
    {
        //  Check if field is empty
        if(inputValue == "")
        {
            InputWarningWindow.Instance.OpenWindow($"Please enter a value!");
            return;
        }
        //  Field isn't empty
        else
        {
            //  Capitalize the first letter cuz we display stuff naise around here
            if (inputValue.Length == 1)
                inputValue = char.ToUpper(inputValue[0]).ToString();
            else
                inputValue = char.ToUpper(inputValue[0]) + inputValue.Substring(1);

            //  Check if this log already exists (case insensitive)
            if (DataController.Instance.bossLogsDictionary.ContainsLogName(CacheManager.currentBoss, inputValue))
            {
                InputWarningWindow.Instance.OpenWindow($"A log called {inputValue} already exists for {CacheManager.currentBoss}!  Please enter a different value!");
                logInputField.text = "";
                return;
            }
            //  Log name doesn't yet exist for this boss
            else
            {
                //  Add our log, trigger our event, and close the window
                DataController.Instance.bossLogsDictionary.AddLog(CacheManager.currentBoss, in inputValue);
                NewLogWindow.Instance.CloseWindow();
                EventManager.Instance.LogAdded(in inputValue);
            }
        }
    }
}
