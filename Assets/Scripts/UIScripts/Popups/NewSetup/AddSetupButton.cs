using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Button to add a new Setup
public class AddSetupButton : MonoBehaviour
{
    private Button thisButton;
    [SerializeField] private InputField setupInputField;

    private void Awake()
    {
        thisButton = GetComponent<Button>();

        if (!thisButton)
            throw new System.Exception($"AddLogButton.cs is not attached to a gameobject!");
        else
            thisButton.onClick.AddListener(AddButtonListener);

        setupInputField.GetComponent<InputFieldOnEndEnter>().endEditAction = AddButtonListener;
    }

    private void AddButtonListener()
    {
        AddSetup(setupInputField.text);
    }

    private void AddSetup(string inputValue)
    {
        //  Check if field is empty
        if (inputValue == "")
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

            //  Check if this setup already exists
            if (DataController.Instance.setupDictionary.ContainsKey(inputValue))
            {
                InputWarningWindow.Instance.OpenWindow($"A setup called {inputValue} already exists!  Please enter a different value!");
                setupInputField.text = "";
                return;
            }
            //  Setup name doesn't yet exist
            else
            {
                //  Add our setup, trigger our event, and close the window
                Player player = CacheManager.SetupTab.Setup.Player;
                DataController.Instance.setupDictionary.Add(inputValue, new Setup(inputValue, player));
                NewSetupWindow.Instance.CloseWindow();
                EventManager.Instance.SetupAdded(in inputValue);
            }
        }
    }
}
