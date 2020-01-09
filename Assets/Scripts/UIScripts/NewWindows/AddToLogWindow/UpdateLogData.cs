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
    private int m_Kills, m_Time, m_Loot;
    [SerializeField]
    private GameObject m_ThisWindow;

    public void OnClick()
    {
        GetValues();
    }

    private void GetValues()
    {
        //  Make sure input fields have a text value
        if (m_KillsField.text == "" || m_LootField.text == "" || m_TimeField.text == "")
        {
            //  TO DO Input field(s) are empty error
        }
        else
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
        {
            //  TO DO input error box
            Debug.Log("error");
        }
        else
        {
            UpdateLog();
        }
    }

    private void UpdateLog()
    {
        //  Get log name from ui
        string logName = m_LogDropdown.options[m_LogDropdown.value].text;

        //  Get our current data and create a new object for our input data
        SingleBossLogData origData = DataController.dataController.BossLogsDictionaryClass.GetBossLogData(UIController.uicontroller.GetCurrentBoss(),
            logName);
        SingleBossLogData inputData = new SingleBossLogData(logName, UIController.uicontroller.GetCurrentBoss(), m_Kills, m_Loot, m_Time);

        //  Add the two
        origData += inputData;

        //  Update the log
        DataController.dataController.BossLogsDictionaryClass.SetLog(UIController.uicontroller.GetCurrentBoss(),
            logName, origData);


        //  Call event to reset our ui
        EventManager.manager.BossDropdownValueChanged();

        //  Close this window and the click blocker
        UIController.uicontroller.m_ClickBlocker.SetActive(false);
        m_ThisWindow.SetActive(false);
    }
}
