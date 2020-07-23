using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddToLogButton : MonoBehaviour
{
    private Button thisButton;
    [SerializeField] InputField killsInputField;
    [SerializeField] InputField lootInputField;
    [SerializeField] InputField timeInputField;
    [SerializeField] Dropdown timeDropdown;

    private void Awake()
    {
        thisButton = GetComponent<Button>();

        if (!thisButton)
            throw new System.Exception($"AddToLogButton.cs is not attached to a button gameobject!");
        else
        {
            //  Add listeners for button and inputfields (on enter key) to trigger AddToLog
            thisButton.onClick.AddListener(AddToLog);
            killsInputField.GetComponent<InputFieldOnEndEnter>().endEditAction = AddToLog;
            lootInputField.GetComponent<InputFieldOnEndEnter>().endEditAction = AddToLog;
            timeInputField.GetComponent<InputFieldOnEndEnter>().endEditAction = AddToLog;
        }
    }

    private void AddToLog()
    {
        //  Make sure none of the input fields is empty
        if(killsInputField.text == "" || lootInputField.text == "" || timeInputField.text == "")
        {
            InputWarningWindow.Instance.OpenWindow($"You must enter some integer value in each field!");
        }
        else
        {
            //  Make sure each value is within proper ranges for its data type
            uint kills;
            if(!uint.TryParse(killsInputField.text, out kills))
            {
                InputWarningWindow.Instance.OpenWindow($"Kills must be a value from 0 to {uint.MaxValue}!");
                return;
            }

            ulong loot;
            if(!ulong.TryParse(lootInputField.text, out loot))
            {
                InputWarningWindow.Instance.OpenWindow($"Loot must be a value from 0 to {ulong.MaxValue}!");
                return;
            }

            uint time;
            if(!uint.TryParse(timeInputField.text, out time))
            {
                InputWarningWindow.Instance.OpenWindow($"Time must be a value from 0 to {uint.MaxValue}!");
            }

            //  Convert from minutes to seconds if needed
            if(timeDropdown.value == 1)
            {
                time *= 60;
            }

            //  Populate a new BossLog object of our new data
            BossLog newData = new BossLog(CacheManager.currentBoss, CacheManager.DropsTab.currentLog, kills, loot, time);

            //  Add this to the selected log
            DataController.Instance.bossLogsDictionary.AddToLog(CacheManager.currentBoss, CacheManager.DropsTab.currentLog, in newData
                , DataController.Instance.dropList);

            //  Close the window
            AddToLogWindow.Instance.CloseWindow();
            EventManager.Instance.LogUpdated();
        }
    }
}
