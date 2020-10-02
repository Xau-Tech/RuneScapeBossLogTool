using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Run-time dictionary of all SetupItems
public static class SetupItemsDictionary
{
    //  Link to ScriptableObject
    private static SetupItemsCollectionSO items = Resources.Load<SetupItemsCollectionSO>("SetupItems/SetupItemsDB");

    //  Lists of SetupItems for each SetupItemCategory
    public static List<SetupItem> Food { get { return items.foodList; } }
    public static List<SetupItem> Potions { get { return items.potionList; } }

    //  Get list of SetupItems from passed SetupItemCategory
    public static List<SetupItem> GetItems(in SetupItemCategories setupItemCategory)
    {
        switch (setupItemCategory)
        {
            case SetupItemCategories.Food:
                return Food;
            case SetupItemCategories.Potion:
                return Potions;
            default:
                return null;
        }
    }
}
