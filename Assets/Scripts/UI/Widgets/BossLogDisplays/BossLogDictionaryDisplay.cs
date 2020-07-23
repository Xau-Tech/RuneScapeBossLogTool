using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  This is a base class to display BossLogList or BossLog data because their data is displayed the same way
public abstract class BossLogDictionaryDisplay : MonoBehaviour
{
    protected void DisplayBossText(in Text textObject, in string s)
    {
        textObject.text = $"{s} Data:";
    }

    protected void DisplayKillsText(in Text textObject, in uint kills)
    {
        textObject.text = $"Kills: {kills.ToString("N0")}";
    }

    protected void DisplayTimeText(in Text textObject, in double hours)
    {
        textObject.text = $"Time (hrs): {hours.ToString("N2")}";
    }

    protected void DisplayLootText(in Text textObject, in ulong loot)
    {
        textObject.text = $"Loot: {loot.ToString("N0")} gp";
    }

    protected void DisplayKillsPerHourText(in Text textObject, in uint kills, in double hours)
    {
        if (hours == 0d)
            textObject.text = $"Kills/Hour: 0";
        else
        {
            textObject.text = $"Kills/Hour: {(kills / hours).ToString("N2")}";
        }
    }

    protected void DisplayLootPerKillText(in Text textObject, in ulong loot, in uint kills)
    {
        if (kills == 0u)
            textObject.text = $"Loot/Kill: 0 gp/kill";
        else
            textObject.text = $"Loot/Kill: {(loot / kills).ToString("N0")} gp/kill";
    }

    protected void DisplayLootPerHourText(in Text textObject, in ulong loot, in double hours)
    {
        if (hours == 0d)
            textObject.text = $"Loot/Hour: 0 gp/hr";
        else
            textObject.text = $"Loot/Hour: {(loot / hours).ToString("N0")} gp/hr";
    }
}