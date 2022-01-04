using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Display data for all logs belonging to a boss
/// </summary>
public class BossTotalsDisplay : AbsLogDisplay
{
    //  Methods

    public void Display(BossLogList bossLogList)
    {
        base.DisplayBoss(ApplicationController.Instance.BossInfo.GetName(bossLogList.bossID));

        //  Calculate totals
        LogDataStruct bossTotals = bossLogList.CalculateTotals();
        double hours = bossTotals.Time / 3600d;

        base.DisplayKills(bossTotals.Kills);
        base.DisplayTime(hours);
        base.DisplayLoot(bossTotals.Loot);
        base.DisplayKillsPerHour(bossTotals.Kills, hours);
        base.DisplayLootPerKill(bossTotals.Loot, bossTotals.Kills);
        base.DisplayLootPerHour(bossTotals.Loot, hours);
        base.DisplayRares(bossLogList.GetRareItemList(), ApplicationController.Instance.BossInfo.GetName(bossLogList.bossID));
    }
}