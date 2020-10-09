using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using GoogleSheetsToUnity;
using System;

//  Run-time dictionary of all SetupItems
public static class SetupItemsDictionary
{
    //  Link to ScriptableObject
    private static SetupItemsCollectionSO items = Resources.Load<SetupItemsCollectionSO>("SetupItems/SetupItemsDB");
    private static Dictionary<int, SetupItem> data = new Dictionary<int, SetupItem>();
    //  Lists of struct data for each category
    private static List<SetupItemStruct> foodList = new List<SetupItemStruct>();
    private static List<SetupItemStruct> potionList = new List<SetupItemStruct>();
    private static List<SetupItemStruct> bodyList = new List<SetupItemStruct>();
    private static List<List<SetupItemStruct>> listSetupItemStructLists = new List<List<SetupItemStruct>>() { foodList, potionList, bodyList };

    private static readonly string SHEETNAME = "SetupItems";

    //  Combine all lists into a single dictionary<int itemID, SetupItem item>
    public static void Setup(in string sheetID)
    {
        //  Load food
        foreach(FoodSO foodData in items.foodList)
        {
            Food food = new Food(foodData);
            foodList.Add(new SetupItemStruct(foodData.itemID, foodData.itemName));
            data.Add(food.itemID, food);
        }

        //  Load potions
        foreach(PotionSO potionData in items.potionList)
        {
            Potion potion = new Potion(potionData);
            potionList.Add(new SetupItemStruct(potionData.itemID, potionData.itemName));
            data.Add(potion.itemID, potion);
        }

        //  Load nondegradeable armour
        foreach(NondegradeArmourSO armourData in items.nondegradeArmourList)
        {
            NondegradeArmour armour = new NondegradeArmour(armourData);
            data.Add(armour.itemID, armour);

            SetupItemCategories itemCategory = armour.itemCategory;

            switch (itemCategory)
            {
                case SetupItemCategories.Body:
                    bodyList.Add(new SetupItemStruct(armour.itemID, armour.itemName));
                    continue;
                default:
                    Debug.Log($"Item \"{armour.itemName}\" with category {armour.itemCategory.ToString()} could not be added because that category list does not exist!");
                    continue;
            }
        }

        //  Load nondegradable armour
        foreach (AugArmourSO armourData in items.augmentedArmourList)
        {
            AugmentedArmour armour = new AugmentedArmour(armourData);
            data.Add(armour.itemID, armour);

            SetupItemCategories itemCategory = armour.itemCategory;

            switch (itemCategory)
            {
                case SetupItemCategories.Body:
                    bodyList.Add(new SetupItemStruct(armour.itemID, armour.itemName));
                    continue;
                default:
                    Debug.Log($"Item \"{armour.itemName}\" with category {armour.itemCategory.ToString()} could not be added because that category list does not exist!");
                    continue;
            }
        }

        //  Load degradable armour
        foreach (DegradableArmourSO armourData in items.degradableArmourList)
        {
            DegradableArmour armour = new DegradableArmour(armourData);
            data.Add(armour.itemID, armour);

            SetupItemCategories itemCategory = armour.itemCategory;

            switch (itemCategory)
            {
                case SetupItemCategories.Body:
                    bodyList.Add(new SetupItemStruct(armour.itemID, armour.itemName));
                    continue;
                default:
                    Debug.Log($"Item \"{armour.itemName}\" with category {armour.itemCategory.ToString()} could not be added because that category list does not exist!");
                    continue;
            }
        }

        //  Unload the ScriptableObjects collection
        Resources.UnloadAsset(items);

        //  Sort each list alphabetically
        foreach(List<SetupItemStruct> list in listSetupItemStructLists)
        {
            list.Sort();
        }

        //  Load prices from GDoc
        SpreadsheetManager.ReadPublicSpreadsheet(new GSTU_Search(sheetID, SHEETNAME), LoadPrices);
    }

    //  Load prices from GDoc
    private static void LoadPrices(GstuSpreadSheet ss)
    {
        SetupItem item;
        int itemID;
        uint price;

        for(int i = 2; i < (ss.rows.primaryDictionary.Count + 1); ++i)
        {
            //  Check for itemID in GDoc
            if (!int.TryParse(ss["A" + i].value, out itemID))
            {
                Debug.Log($"No itemID found on row {(i + 2)}");
                continue;
            }

            //  Check for SetupItem in dictionary
            if (!data.TryGetValue(itemID, out item))
            {
                Debug.Log($"No match for itemID {itemID} in the SetupItemsDictionary!");
                continue;
            }

            //  Check for price in GDoc
            if (!uint.TryParse(ss["C" + i].value, out price))
            {
                Debug.Log($"No price found on row {(i + 2)}");
                continue;
            }

            item.price = price;
        }

        //  debug printing all items in dictionary
        PrintDictionary();
    }

    //  Get list of SetupItems from passed SetupItemCategory
    public static List<SetupItemStruct> GetItems(in SetupItemCategories setupItemCategory)
    {
        switch (setupItemCategory)
        {
            case SetupItemCategories.Food:
                return foodList;
            case SetupItemCategories.Potion:
                return potionList;
            case SetupItemCategories.Body:
                return bodyList;
            default:
                return null;
        }
    }

    //  Dictionary.TryGetValue wrapper that returns a copy of the item matching itemID
    public static bool TryGetItem(in int itemID, out SetupItem setupItem)
    {
        SetupItem itemToCopy;

        if (data.TryGetValue(itemID, out itemToCopy))
        {
            setupItem = itemToCopy.Clone() as SetupItem;
            return true;
        }
        else
        {
            setupItem = null;
            return false;
        }
    }

    private static void PrintDictionary()
    {
        string items = "SetupItemsDictionary:";
        foreach (var kvp in data)
        {
            items += $"\n{kvp.Value.ToString()}";
        }
        Debug.Log(items);
    }
}

//  Struct for holding SetupItem name and itemID
public struct SetupItemStruct : IComparable<SetupItemStruct>
{
    public SetupItemStruct(int itemID, string itemName)
    {
        this.itemID = itemID;
        this.itemName = itemName;
    }

    public int itemID;
    public string itemName;

    public int CompareTo(SetupItemStruct other)
    {
        return itemName.CompareTo(other.itemName);
    }
}
