using System.Runtime.Serialization;

[System.Serializable]
public class PlayerGlob
{
    public InventoryGlob inventory { get; set; }
    public EquipmentGlob equipment { get; set; }
    public PrefightItemsGlob prefight { get; set; }
    public BeastOfBurdenGlob beastOfBurden { get; set; }

    public PlayerGlob(in Player player)
    {
        inventory = new InventoryGlob(player.Inventory);
        equipment = new EquipmentGlob(player.Equipment);
        prefight = new PrefightItemsGlob(player.PrefightItems);
        beastOfBurden = new BeastOfBurdenGlob(player.BeastOfBurden);
    }
}