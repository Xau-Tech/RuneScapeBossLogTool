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
        thisDropdown.onValueChanged.AddListener(BossChanged);
        EventManager.Instance.onDataLoaded += FillBossDropdown;
    }

    private void OnDisable()
    {
        thisDropdown.onValueChanged.RemoveListener(BossChanged);
        EventManager.Instance.onDataLoaded -= FillBossDropdown;
    }

    //  Fill the dropdown with from our BossInfoDictionary and set the value to the first item in the list
    private void FillBossDropdown()
    {
        thisDropdown.ClearOptions();
        Debug.Log("Adding boss names to dropdown");
        thisDropdown.AddOptions(DataController.Instance.bossInfoDictionary.GetBossNames());

        if (ProgramState.CurrentState == ProgramState.states.Loading)
        {
            BossInfo currentBoss = DataController.Instance.bossInfoDictionary.GetBossByName(thisDropdown.options[0].text);

            CacheManager.currentBoss = currentBoss;
            CacheManager.SetupTab.CurrentSubBossList = currentBoss.combatData;
        }
    }

    //  Sets the dropdown to passed value and calls the event that this dropdown 
    private void BossChanged(int value)
    {
        //  If in the Drop window and an active DropList ask the user if they really want to switch
        //  As this will reset any data they currently have
        if (ProgramState.CurrentState == ProgramState.states.Drops && DataController.Instance.dropList.Count > 0)
        {
            ConfirmWindow.Instance.NewConfirmWindow(
                $"If you switch bosses, all of the Items you have added will be reset - is that okay?",
                BossChangeConfirm,
                value.ToString());
        }
        else
            UpdateBoss(in value);


        Debug.Log($"BossChanged with value {value}");
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
        else
        {
            thisDropdown.onValueChanged.RemoveListener(BossChanged);
            thisDropdown.value = thisDropdown.options.FindIndex(option => option.text.CompareTo(CacheManager.currentBoss.bossName) == 0);
            thisDropdown.onValueChanged.AddListener(BossChanged);

            Debug.Log(CacheManager.currentBoss);
        }
    }

    //  Set boss from int and call event
    private void UpdateBoss(in int value)
    {
        if (ProgramState.CurrentState == ProgramState.states.Setup)
        {
            CacheManager.SetupTab.CurrentSubBossList = DataController.Instance.bossInfoDictionary.GetBossByName(thisDropdown.options[value].text).combatData;
            CacheManager.SetupTab.currentSubBossIndex = 0;
            EventManager.Instance.UpdateHitChance();
        }
        else
        {
            CacheManager.currentBoss = DataController.Instance.bossInfoDictionary.GetBossByName(thisDropdown.options[value].text);
            Debug.Log(CacheManager.currentBoss);
        }

        EventManager.Instance.BossDropdownValueChanged();
    }
}
