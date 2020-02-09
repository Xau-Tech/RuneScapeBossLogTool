using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateLogData : MonoBehaviour
{
    [SerializeField]
    private InputField m_KillsField;
    [SerializeField]
    private InputField m_LootField;
    [SerializeField]
    private InputField m_TimeField;
    [SerializeField]
    private Dropdown m_LogDropdown;
    [SerializeField]
    private Dropdown m_TimeDropdown;
    private int m_Kills, m_Time, m_Loot;
    private CloseWindow m_CloseWindowScript;

    private void Awake()
    {
        m_CloseWindowScript = GetComponentInParent<CloseWindow>();
    }

    public void EnterPressed()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            OnClick();
    }

    public void OnClick()
    {
        GetValues();
    }

    private void GetValues()
    {
        //  Make sure input fields have a text value
        if (m_KillsField.text == "" || m_LootField.text == "" || m_TimeField.text == "")
            EventManager.Instance.InputWarningOpen("You must enter a value in each text box!");
        else
        //  The input fields all have SOME DATA
        {
            int.TryParse(m_KillsField.text, out m_Kills);
            int.TryParse(m_LootField.text, out m_Loot);
            int.TryParse(m_TimeField.text, out m_Time);

            InputCheck();
        }
    }

    //  Make sure all inputs are valid
    private void InputCheck()
    {
        //  One of the values is negative
        if(m_Kills < 0 || m_Time < 0 || m_Loot < 0)
            EventManager.Instance.InputWarningOpen("Values cannot be negative!");
        else
            UpdateLog();
    }

    private void UpdateLog()
    {
        //  Get log name from ui and make sure time is in correct format to apply update
        string logName = m_LogDropdown.options[m_LogDropdown.value].text;
        ParseTimeData();


        //  Get our current data and create a new object for our input data
        BossLog origData = DataController.Instance.BossLogsDictionary.GetBossLogData(DataController.Instance.CurrentBoss, logName);
        BossLog inputData = new BossLog(logName, DataController.Instance.CurrentBoss,
            (uint)m_Kills, (ulong)m_Loot, (uint)m_Time);

        //  Add the two
        origData += inputData;

        //  Update the log
        DataController.Instance.BossLogsDictionary.SetLog(DataController.Instance.CurrentBoss, logName, origData);
        DataController.Instance.HasUnsavedData = true;

        //  Call event to reset our ui
        EventManager.Instance.BossDropdownValueChanged();

        //  Close this window and the click blocker
        m_CloseWindowScript.Close();
    }

    //  Convert time value to seconds if need be
    private void ParseTimeData()
    {
    //  Minutes is selected in dropdown
    if(m_TimeDropdown.value == 1)
            m_Time *= 60;
    }
}
