//  Model for the SetupMVC.cs class
//  Holds data for the Setup tab including player, items, etc
public class Setup
{
    public Setup(string setupName)
    {
        SetupName = setupName;
        combatIntensity = new CombatIntensity(CombatIntensity.CombatIntensityLevels.Low);
        ChargeDrainRate = 0.0f;
        InstanceCost = 0;
        player = new Player(string.Empty);
    }

    public Setup(string setupName, Player player)
    {
        SetupName = setupName;
        combatIntensity = new CombatIntensity(CombatIntensity.CombatIntensityLevels.Low);
        ChargeDrainRate = 0.0f;
        InstanceCost = 0;

        this.player = new Player(player.Username);
        this.player.SmithingLevel = player.SmithingLevel;
        this.player.PrayerLevel = player.PrayerLevel;
    }

    public Setup(SetupSaveGlob saveGlob, Player player)
    {
        //  SetupSaveGlob
        SetupName = saveGlob.setupName;
        combatIntensity = new CombatIntensity((CombatIntensity.CombatIntensityLevels)saveGlob.combatIntensity);
        ChargeDrainRate = saveGlob.chargeDrainRate;
        InstanceCost = saveGlob.instanceCost;

        //  PlayerGlob
        this.player = new Player(player.Username, saveGlob.player);
        this.player.SmithingLevel = player.SmithingLevel;
        this.player.PrayerLevel = player.PrayerLevel;
    }

    public string SetupName { get; set; }
    public float ChargeDrainRate { get; private set; }
    public Player player { get; set; }
    public int TotalCost { get { return player.Inventory.TotalCost + player.Equipment.TotalCost + InstanceCost;} }
    public float DegradePerHour { get { return combatIntensity.degradePerHour; } }
    public float ChargeDrainPerHour { get { return combatIntensity.drainPerHour; } }
    public int InstanceCost { get; set; }
    public CombatIntensity combatIntensity { get; private set; }

    //  Set charge drain rate
    public void SetChargeDrainRate(in float chargeDrainRate)
    {
        this.ChargeDrainRate = chargeDrainRate;
        player.Equipment.DetermineCost();
    }

    public void SetCombatIntensity(in CombatIntensity.CombatIntensityLevels intensityLevel)
    {
        combatIntensity.IntensityLevel = intensityLevel;
        player.Equipment.DetermineCost();
    }
}