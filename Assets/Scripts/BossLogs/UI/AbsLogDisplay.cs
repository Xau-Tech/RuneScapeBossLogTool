using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Abstract class for displaying logged data
/// </summary>
public abstract class AbsLogDisplay : MonoBehaviour
{
    //  Properties & fields

    [SerializeField] private Text _bossText;
    [SerializeField] private Text _killsText;
    [SerializeField] private Text _timeText;
    [SerializeField] private Text _lootText;
    [SerializeField] private Text _kphText;
    [SerializeField] private Text _lpkText;
    [SerializeField] private Text _lphText;
    [SerializeField] private RareItemScrollListContainer _rareItemContainer;

    //  Methods

    public void LoadSprites(BossInfo boss)
    {
        _rareItemContainer.LoadSprites(boss);
    }

    protected void DisplayBoss(string bossName)
    {
        _bossText.text = $"{bossName} Data:";
    }

    protected void DisplayKills(uint kills)
    {
        _killsText.text = $"Kills: {kills.ToString("N0")}";
    }

    protected void DisplayTime(double hours)
    {
        _timeText.text = $"Time (hrs): {hours.ToString("N2")}";
    }

    protected void DisplayLoot(ulong loot)
    {
        _lootText.text = $"Loot: {loot.ToString("N0")} gp";
    }

    protected void DisplayKillsPerHour(uint kills, double hours)
    {
        if (hours == 0d)
            _kphText.text = $"Kills/Hour: 0";
        else
            _kphText.text = $"Kills/Hour: {(kills / hours).ToString("N2")}";
    }

    protected void DisplayLootPerKill(ulong loot, uint kills)
    {
        if (kills == 0u)
            _lpkText.text = $"Loot/Kill: 0 gp/kill";
        else
            _lpkText.text = $"Loot/Kill: {(loot / kills).ToString("N0")} gp/kill";
    }

    protected void DisplayLootPerHour(ulong loot, double hours)
    {
        if (hours == 0d)
            _lphText.text = $"Loot/Hour: 0 gp/hr";
        else
            _lphText.text = $"Loot/Hour: {(loot / hours).ToString("N0")} gp/hr";
    }

    protected void DisplayRares(RareItemList rareItemList, string bossName)
    {
        _rareItemContainer.Display(rareItemList, bossName);
    }
}
