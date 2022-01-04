using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerGlob
{
    //  Properties & fields

    public InventoryGlob inventory { get; set; }
    public EquipmentGlob equipment { get; set; }
    public PrefightItemsGlob prefight { get; set; }
    public BeastOfBurdenGlob beastOfBurden { get; set; }

    //  Constructor

    public PlayerGlob(Player player)
    {
        inventory = new InventoryGlob(player.Inventory);
        equipment = new EquipmentGlob(player.Equipment);
        prefight = new PrefightItemsGlob(player.Prefight);
        beastOfBurden = new BeastOfBurdenGlob(player.BeastOfBurden);
    }
}
