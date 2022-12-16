using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Display data for a singular boss log
/// </summary>
public class LogTotalsDisplay : AbsLogDisplay
{
    //  Properties & fields

    [SerializeField] private Text _setupNameText;
    [SerializeField] private Text _setupCostText;
    [SerializeField] private Text _totalNetProfitText;
    [SerializeField] private Text _netProfitPerHourText;

    //  Methods

    public void Display(BossLog log)
    {
        if (log.IsEmpty())
            log = new BossLog(-1, "No Log");


        double hours = log.time / 3600d;

        base.DisplayBoss(log.logName);
        base.DisplayKills(log.kills);
        base.DisplayTime(hours);
        base.DisplayLoot(log.loot);
        base.DisplayKillsPerHour(log.kills, hours);
        base.DisplayLootPerKill(log.loot, log.kills);
        base.DisplayLootPerHour(log.loot, hours);
        base.DisplayRares(log.rareItemList, ApplicationController.Instance.BossInfo.GetName(log.bossID));

        //  Get linked setup

        string setupName;
        string setupCost;
        string netProfit;
        string netProfitPerHour;

        if (string.IsNullOrEmpty(log.setupName))
        {
            setupName = "No linked setup";
            setupCost = "0";
            netProfit = "0";
            netProfitPerHour = "0";
        }
        else
        {
            setupName = log.setupName;

            //  Get linked setup
            ApplicationController.Instance.Setups.TryGetValue(log.setupName, out Setup setup);

            setupCost = setup.TotalCost.ToString("N0");
            netProfit = (log.loot - (setup.TotalCost * hours)).ToString("N0");
            netProfitPerHour = ((log.loot / hours) - setup.TotalCost).ToString("N0");
        }

        _setupNameText.text = $"Setup: {setupName}";
        _setupCostText.text = $"Setup Cost: {setupCost} gp/hr";
        _totalNetProfitText.text = $"Net Profit: {netProfit} gp";
        _netProfitPerHourText.text = $"Profit/Hour: {netProfitPerHour} gp/hr";
    }
}