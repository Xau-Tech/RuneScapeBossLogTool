using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Script for Dropdowns used to show all available bosses
public class BossDropdown : MonoBehaviour
{
    private Dropdown thisDropdown;

    private void Awake()
    {
        thisDropdown = GetComponent<Dropdown>();
        if (!thisDropdown)
            throw new System.Exception($"BossDropdown.cs not attached to a dropdown");
    }

    private void OnEnable()
    {
        EventManager.Instance.onBossInfoLoaded += FillBossDropdown;
        thisDropdown.onValueChanged.AddListener(BossChanged);
    }

    private void OnDisable()
    {
        EventManager.Instance.onBossInfoLoaded -= FillBossDropdown;
        thisDropdown.onValueChanged.RemoveListener(BossChanged);
    }

    //  Fill the dropdown with from our BossInfoDictionary and set the value to the first item in the list
    private void FillBossDropdown()
    {
        thisDropdown.ClearOptions();
        Debug.Log("Adding boss names to dropdown");
        thisDropdown.AddOptions(DataController.Instance.bossInfoDictionary.GetBossNames());

        if (ProgramState.CurrentState == ProgramState.states.Loading)
        {
            CacheManager.currentBoss = thisDropdown.options[0].text;
            thisDropdown.onValueChanged.AddListener(BossChanged);
            return;
        }
    }

    //  Sets the dropdown to passed value and calls the event that this dropdown 
    private void BossChanged(int value)
    {
        //  If in the Drop window and an active DropList ask the user if they really want to switch
        //  As this will reset any data they currently have
        if (ProgramState.CurrentState == ProgramState.states.Drops && DataController.Instance.dropList.Count > 0)
        {
            //  Remove the listener until the end of this process or it else setting its value will re-run this function
            thisDropdown.onValueChanged.RemoveListener(BossChanged);

            //  Retain the previous choice in case the user decides clicks cancel in the ConfirmWindow
            thisDropdown.value = thisDropdown.options.FindIndex(option => option.text.CompareTo(CacheManager.currentBoss) == 0);

            ConfirmWindow.Instance.NewConfirmWindow(
                $"If you switch bosses, all of the Items you have added will be reset - is that okay?",
                BossChangeConfirm,
                value.ToString());
        }
        else
        {
            UpdateBoss(in value);
        }
    }

    //  Callback function for asking a user
    private void BossChangeConfirm(UserResponse userResponse, string returnedData)
    {
        //  If user confirmed, update the boss accordingly
        if (userResponse.userResponse)
        {
            int returnedInt = int.Parse(returnedData);
            UpdateBoss(in returnedInt);
        }

        //  End of the confirmation process - add the listener back
        thisDropdown.onValueChanged.AddListener(BossChanged);
    }

    //  Set boss from int and call event
    private void UpdateBoss(in int value)
    {
        thisDropdown.value = value;
        CacheManager.currentBoss = thisDropdown.options[value].text;
        Debug.Log(CacheManager.currentBoss);
        EventManager.Instance.BossDropdownValueChanged();
        EventManager.Instance.ChangeBossDisplayHandler();
    }
}
