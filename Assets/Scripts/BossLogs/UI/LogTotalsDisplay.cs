using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Display data for a singular boss log
/// </summary>
public class LogTotalsDisplay : AbsLogDisplay
{
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
    }
}