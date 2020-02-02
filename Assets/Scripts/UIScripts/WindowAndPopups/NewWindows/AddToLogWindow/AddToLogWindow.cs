using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddToLogWindow : MonoBehaviour
{
    [SerializeField] private Text m_InfoText;
    [SerializeField] private InputField m_KillsField;
    [SerializeField] private InputField m_LootField;
    [SerializeField] private InputField m_TimeField;
    [SerializeField] private TimerScript m_TimerScript;
    [SerializeField] private Dropdown m_LogDropdown;
    [SerializeField] private KillcountIncrement m_KillcountScript;
    private DefaultSelected m_DefaultSelectedScript;
    private TabJumpInteractables m_TabJumpScript;

    private void Awake()
    {
        m_DefaultSelectedScript = GetComponent<DefaultSelected>();
        m_TabJumpScript = GetComponent<TabJumpInteractables>();
    }

    private void OnEnable()
    {
        //  Set up window UI
        PopulateUI();

        //  Enable the default selector script to select the passed selectable
        m_DefaultSelectedScript.enabled = true;

        //  Enable the script to allow tab to move between selectables
        m_TabJumpScript.enabled = true;
    }

    private void OnDisable()
    {
        m_DefaultSelectedScript.enabled = false;
        m_TabJumpScript.enabled = false;
    }

    private void PopulateUI()
    {
        m_TimeField.text = m_TimerScript.GetTimerMinutes();
        m_KillsField.text = m_KillcountScript.Killcount;
        m_LootField.text = DataController.Instance.DropList.GetTotalValue().ToString();
        m_InfoText.text = "Adding to Log: " + m_LogDropdown.options[m_LogDropdown.value].text;
    }
}
