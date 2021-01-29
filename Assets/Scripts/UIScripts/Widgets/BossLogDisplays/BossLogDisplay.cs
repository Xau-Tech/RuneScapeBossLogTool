using UnityEngine;
using UnityEngine.UI;

public class BossLogDisplay : BossLogDictionaryDisplay, IDisplayable<BossLog>
{
    [SerializeField] private Text bossText;
    [SerializeField] private Text killsText;
    [SerializeField] private Text timeText;
    [SerializeField] private Text lootText;
    [SerializeField] private Text killsPerHourText;
    [SerializeField] private Text lootPerKillText;
    [SerializeField] private Text lootPerHourText;
    [SerializeField] private RareItemScrollListController rareListController;

    public void Display(in BossLog value)
    {
        double hours = value.time / 3600d;

        base.DisplayBossText(in bossText, value.logName);
        base.DisplayKillsText(in killsText, value.kills);
        base.DisplayTimeText(in timeText, in hours);
        base.DisplayLootText(in lootText, value.loot);
        base.DisplayKillsPerHourText(in killsPerHourText, value.kills, in hours);
        base.DisplayLootPerKillText(in lootPerKillText, value.loot, value.kills);
        base.DisplayLootPerHourText(in lootPerHourText, value.loot, in hours);

        rareListController.Display(value.rareItemList);
    }
}
