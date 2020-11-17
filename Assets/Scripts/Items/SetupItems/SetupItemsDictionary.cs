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
    private static List<SetupItemStruct> legList = new List<SetupItemStruct>();
    private static List<SetupItemStruct> helmList = new List<SetupItemStruct>();
    private static List<SetupItemStruct> neckList = new List<SetupItemStruct>();
    private static List<SetupItemStruct> capeList = new List<SetupItemStruct>();
    private static List<SetupItemStruct> gloveList = new List<SetupItemStruct>();
    private static List<SetupItemStruct> bootList = new List<SetupItemStruct>();
    private static List<SetupItemStruct> pocketList = new List<SetupItemStruct>();
    private static List<SetupItemStruct> ammoList = new List<SetupItemStruct>();
    private static List<SetupItemStruct> ringList = new List<SetupItemStruct>();
    private static List<SetupItemStruct> mhWeaponList = new List<SetupItemStruct>();
    private static List<SetupItemStruct> ohWeaponList = new List<SetupItemStruct>();
    private static List<SetupItemStruct> twoHandWeaponList = new List<SetupItemStruct>();
    private static List<SetupItemStruct> shieldList = new List<SetupItemStruct>();
    private static List<List<SetupItemStruct>> listSetupItemStructLists = new List<List<SetupItemStruct>>() { foodList, potionList, bodyList, legList, helmList, neckList,
        capeList, gloveList, bootList, pocketList, ammoList, ringList, mhWeaponList, ohWeaponList, twoHandWeaponList, shieldList };

    private static readonly string SHEETNAME = "SetupItems";

    //  Combine all lists into a single dictionary<int itemID, SetupItem item>
    public static void Setup(string sheetID)
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
            NondegradableArmour armour = new NondegradableArmour(armourData);
            data.Add(armour.itemID, armour);
            AddToArmourList(new SetupItemStruct(armour.itemID, armour.itemName), armour.GetItemCategory());
        }

        //  Load augmented armour
        foreach (AugArmourSO armourData in items.augmentedArmourList)
        {
            AugmentedArmour armour = new AugmentedArmour(armourData);
            data.Add(armour.itemID, armour);
            AddToArmourList(new SetupItemStruct(armour.itemID, armour.itemName), armour.GetItemCategory());
        }

        //  Load degradable armour
        foreach (DegradableArmourSO armourData in items.degradableArmourList)
        {
            DegradableArmour armour = new DegradableArmour(armourData);
            data.Add(armour.itemID, armour);
            AddToArmourList(new SetupItemStruct(armour.itemID, armour.itemName), armour.GetItemCategory());
        }

        //  Load nondegradable weapons
        foreach(NondegradeWeaponSO weaponData in items.nondegradeWeaponList)
        {
            NondegradableWeapon weapon = new NondegradableWeapon(weaponData);
            data.Add(weapon.itemID, weapon);
            AddToWeaponList(new SetupItemStruct(weapon.itemID, weapon.itemName), weapon.GetItemCategory());
        }

        //  Load augmented weapons
        foreach(AugWeaponSO weaponData in items.augmentedWeaponList)
        {
            AugmentedWeapon weapon = new AugmentedWeapon(weaponData);
            data.Add(weapon.itemID, weapon);
            AddToWeaponList(new SetupItemStruct(weapon.itemID, weapon.itemName), weapon.GetItemCategory());
        }

        //  Load degradable weapons
        foreach(DegradableWeaponSO weaponData in items.degradableWeaponList)
        {
            DegradableWeapon weapon = new DegradableWeapon(weaponData);
            data.Add(weapon.itemID, weapon);
            AddToWeaponList(new SetupItemStruct(weapon.itemID, weapon.itemName), weapon.GetItemCategory());
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

    //  Add the SetupItemStruct for a newly created armour piece to its proper list
    private static void AddToArmourList(in SetupItemStruct itemStruct, SetupItemCategories itemCategory)
    {
        switch (itemCategory)
        {
            case SetupItemCategories.Head:
                helmList.Add(itemStruct);
                break;
            case SetupItemCategories.Pocket:
                pocketList.Add(itemStruct);
                break;
            case SetupItemCategories.Cape:
                capeList.Add(itemStruct);
                break;
            case SetupItemCategories.Neck:
                neckList.Add(itemStruct);
                break;
            case SetupItemCategories.Ammunition:
                ammoList.Add(itemStruct);
                break;
            case SetupItemCategories.Body:
                bodyList.Add(itemStruct);
                break;
            case SetupItemCategories.Legs:
                legList.Add(itemStruct);
                break;
            case SetupItemCategories.Gloves:
                gloveList.Add(itemStruct);
                break;
            case SetupItemCategories.Boots:
                bootList.Add(itemStruct);
                break;
            case SetupItemCategories.Ring:
                ringList.Add(itemStruct);
                break;
            case SetupItemCategories.Shield:
                shieldList.Add(itemStruct);
                break;
            default:
                Debug.Log($"Item \"{itemStruct.itemName}\" with category {itemCategory.ToString()} could not be added because that category list does not exist!");
                break;
        }
    }

    //  Add the SetupItemStruct for a newly created weapon to its proper list
    private static void AddToWeaponList(in SetupItemStruct itemStruct, SetupItemCategories itemCategory)
    {
        switch (itemCategory)
        {
            case SetupItemCategories.Mainhand:
                mhWeaponList.Add(itemStruct);
                break;
            case SetupItemCategories.TwoHand:
                twoHandWeaponList.Add(itemStruct);
                break;
            case SetupItemCategories.Offhand:
                ohWeaponList.Add(itemStruct);
                break;
            default:
                break;
        }
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

        //  Dictionary done loading event
        EventManager.Instance.SetupItemDictionaryLoaded();
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
            case SetupItemCategories.Legs:
                return legList;
            case SetupItemCategories.Head:
                return helmList;
            case SetupItemCategories.Neck:
                return neckList;
            case SetupItemCategories.Cape:
                return capeList;
            case SetupItemCategories.Gloves:
                return gloveList;
            case SetupItemCategories.Boots:
                return bootList;
            case SetupItemCategories.Pocket:
                return pocketList;
            case SetupItemCategories.Ammunition:
                return ammoList;
            case SetupItemCategories.Ring:
                return ringList;
            case SetupItemCategories.Mainhand:
                return mhWeaponList;
            case SetupItemCategories.Offhand:
                return ohWeaponList;
            case SetupItemCategories.Shield:
                return shieldList;
            case SetupItemCategories.TwoHand:
                return twoHandWeaponList;
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
            setupItem = General.NullItem();
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
