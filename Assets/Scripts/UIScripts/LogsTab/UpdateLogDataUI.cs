using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateLogDataUI : MonoBehaviour
{
    [SerializeField]
    private Text m_TotalDisplayText;
    [SerializeField]
    private Text m_TotalKillsText;
    [SerializeField]
    private Text m_TotalTimeText;
    [SerializeField]
    private Text m_TotalLootText;
    [SerializeField]
    private Text m_TotalKillsPerHourText;
    [SerializeField]
    private Text m_TotalLootPerKillText;
    [SerializeField]
    private Text m_TotalLootPerHourText;
    [SerializeField]
    private Dropdown m_LogDropdown;
    [SerializeField]
    private Text m_LogDisplayText;
    [SerializeField]
    private Text m_LogKillsText;
    [SerializeField]
    private Text m_LogTimeText;
    [SerializeField]
    private Text m_LogLootText;
    [SerializeField]
    private Text m_LogKillsPerHourText;
    [SerializeField]
    private Text m_LogLootPerKillText;
    [SerializeField]
    private Text m_LogLootPerHourText;

    private string m_LogName;
    private string m_BossName;
    private Vector3[] m_Data;

    private const string m_WholeFormat = "#,#";
    private const string m_DecimalFormat = "#,#.##";

    private void OnEnable()
    {
        EventManager.manager.onLogsPopulated += UpdateAllUI;
        EventManager.manager.onLogDropdownValueChanged += UpdateLogUI;
    }

    private void OnDisable()
    {
        EventManager.manager.onLogsPopulated -= UpdateAllUI;
        EventManager.manager.onLogDropdownValueChanged -= UpdateLogUI;
    }

    private void GetData()
    {
        //  Get log name
        m_LogName = m_LogDropdown.options[m_LogDropdown.value].text;

        //  Get boss name
        m_BossName = DataController.dataController.CurrentBoss;

        //  Get values for total logs for selected boss
        m_Data = DataController.dataController.BossLogsDictionaryClass.
            GetBossTotalsData(m_BossName);
    }


    private void UpdateAllUI()
    {
        GetData();

        UpdateTotalDataUI();
        UpdateIndividualDataUI();
    }

    private void UpdateLogUI()
    {
        GetData();

        UpdateIndividualDataUI();
    }


    private void UpdateTotalDataUI()
    {
        m_TotalDisplayText.text = m_BossName + " Data:";
        m_TotalKillsText.text = "Total Kills: " + m_Data[0].y.ToString(m_WholeFormat);
        m_TotalTimeText.text = "Total Time: " + (m_Data[0].z / 60.0f).ToString(m_DecimalFormat) + " hours";
        m_TotalLootText.text = "Total Loot: " + m_Data[0].x.ToString(m_WholeFormat) + " gp";
        m_TotalKillsPerHourText.text = "Kills Per Hour: " + m_Data[1].x.ToString(m_DecimalFormat);
        m_TotalLootPerKillText.text = "Loot Per Kill: " + m_Data[1].y.ToString(m_WholeFormat) + " gp";
        m_TotalLootPerHourText.text = "Loot Per Hour: " + m_Data[1].z.ToString(m_WholeFormat) + " gp/hr";
    }

    private void UpdateIndividualDataUI()
    {
        //  Get individual log
        SingleBossLogData m_SingleLog = DataController.dataController.BossLogsDictionaryClass
            .GetBossLogData(m_BossName, m_LogName);

        if(m_SingleLog == null)
        {
            m_SingleLog = new SingleBossLogData("No", "");
        }

        m_LogDisplayText.text = m_SingleLog.LogName + " Data:";
        m_LogKillsText.text = "Kills: " + m_SingleLog.Kills.ToString(m_WholeFormat);
        m_LogTimeText.text = "Time: " + (m_SingleLog.TimeSpent / 60.0f).ToString(m_DecimalFormat) + " hours";
        m_LogLootText.text = "Loot: " + m_SingleLog.LootValue.ToString(m_WholeFormat) + " gp";
        m_LogKillsPerHourText.text = "Kills Per Hour: " + (m_SingleLog.Kills / (m_SingleLog.TimeSpent / 60.0f)).ToString(m_DecimalFormat);
        m_LogLootPerKillText.text = "Loot Per Kill: " + (m_SingleLog.LootValue / m_SingleLog.Kills).ToString(m_WholeFormat) + " gp";
        m_LogLootPerHourText.text = "Loot Per Hour: " + (m_SingleLog.LootValue / (m_SingleLog.TimeSpent / 60.0f)).ToString(m_WholeFormat) + " gp/hr";
    }
}
