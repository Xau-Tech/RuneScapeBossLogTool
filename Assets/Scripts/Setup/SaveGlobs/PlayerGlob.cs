[System.Serializable]
public class PlayerGlob
{
    public InventoryGlob inventory { get; set; }
    public EquipmentGlob equipment { get; set; }

    public PlayerGlob(in Player player)
    {
        inventory = new InventoryGlob(player.Inventory);
        equipment = new EquipmentGlob(player.Equipment);
    }
}
