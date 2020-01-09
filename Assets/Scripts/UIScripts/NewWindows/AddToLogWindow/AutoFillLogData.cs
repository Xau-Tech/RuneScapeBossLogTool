using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoFillLogData : MonoBehaviour
{
    [SerializeField]
    private Text m_InfoText;
    [SerializeField]
    private InputField m_KillsField;
    [SerializeField]
    private InputField m_LootField;
    [SerializeField]
    private InputField m_TimeField;
    [SerializeField]
    private TimerScript m_TimerScript;
    [SerializeField]
    private Dropdown m_LogDropdown;
    [SerializeField]
    private KillcountIncrement m_KillcountScript;

    private void OnEnable()
    {
        m_TimeField.text = m_TimerScript.GetTimerMinutes();
        m_KillsField.text = m_KillcountScript.Killcount;
        m_LootField.text = DataController.dataController.DropListClass.GetTotalValue().ToString();
        m_InfoText.text = "Adding to Log: " + m_LogDropdown.options[m_LogDropdown.value].text;
    }
}
