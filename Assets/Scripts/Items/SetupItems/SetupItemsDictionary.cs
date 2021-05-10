using System.Collections.Generic;
using UnityEngine;
using GoogleSheetsToUnity;
using System;
using System.IO;

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
    private static List<SetupItemStruct> generalItemList = new List<SetupItemStruct>();
    private static List<SetupItemStruct> pouchList = new List<SetupItemStruct>();
    private static List<SetupItemStruct> scrollList = new List<SetupItemStruct>();
    private static List<List<SetupItemStruct>> listSetupItemStructLists = new List<List<SetupItemStruct>>() { foodList, potionList, bodyList, legList, helmList, neckList,
        capeList, gloveList, bootList, pocketList, ammoList, ringList, mhWeaponList, ohWeaponList, twoHandWeaponList, shieldList, generalItemList, pouchList, scrollList };

    private static readonly string SHEETNAME = "SetupItems";
    private static readonly string STARTCELL = "A1";
    private static readonly string ENDCELL = "Z147";

    //  Combine all lists into a single dictionary<int itemID, SetupItem item>
    public static void Setup(string sheetID)
    {
        //  Load food
        foreach(FoodSO foodData in items.foodList)
        {
            //  Make sure this itemID isn't already taken; typos or mistakes in data entry
            if (data.ContainsKey(foodData.itemID))
            {
                SetupItem item;
                data.TryGetValue(foodData.itemID, out item);
                Debug.Log($"Trying to add item [ Name: {foodData.itemName}, ID: {foodData.itemID} ] failed!  Item [ Name: {item.itemName} ] with that ID already exists!");
                continue;
            }

            Food food = new Food(foodData);
            foodList.Add(new SetupItemStruct(foodData.itemID, foodData.itemName));
            data.Add(food.itemID, food);
        }

        //  Load potions
        foreach(PotionSO potionData in items.potionList)
        {
            if (data.ContainsKey(potionData.itemID))
            {
                SetupItem item;
                data.TryGetValue(potionData.itemID, out item);
                Debug.Log($"Trying to add item [ Name: {potionData.itemName}, ID: {potionData.itemID} ] failed!  Item [ Name: {item.itemName} ] with that ID already exists!");
                continue;
            }

            Potion potion = new Potion(potionData);
            potionList.Add(new SetupItemStruct(potionData.itemID, potionData.itemName));
            data.Add(potion.itemID, potion);
        }

        //  Load pouches
        foreach(SummoningPouchSO pouchData in items.summoningPouchList)
        {
            if (data.ContainsKey(pouchData.itemID))
            {
                SetupItem item;
                data.TryGetValue(pouchData.itemID, out item);
                Debug.Log($"Trying to add item [ Name: {pouchData.itemName}, ID: {pouchData.itemID} ] failed!  Item [ Name: {item.itemName} ] with that ID already exists!");
                continue;
            }

            SummoningPouch pouch = new SummoningPouch(pouchData);
            pouchList.Add(new SetupItemStruct(pouchData.itemID, pouchData.itemName));
            data.Add(pouch.itemID, pouch);
        }

        //  Load scrolls
        foreach(SummoningScrollSO scrollData in items.summoningScrollList)
        {
            if (data.ContainsKey(scrollData.itemID))
            {
                SetupItem item;
                data.TryGetValue(scrollData.itemID, out item);
                Debug.Log($"Trying to add item [ Name: {scrollData.itemName}, ID: {scrollData.itemID} ] failed!  Item [ Name: {item.itemName} ] with that ID already exists!");
                continue;
            }

            SummoningScroll scroll = new SummoningScroll(scrollData);
            scrollList.Add(new SetupItemStruct(scrollData.itemID, scrollData.itemName));
            data.Add(scroll.itemID, scroll);
        }

        //  Load nondegradeable armour
        foreach(NondegradeArmourSO armourData in items.nondegradeArmourList)
        {
            if (data.ContainsKey(armourData.itemID))
            {
                SetupItem item;
                data.TryGetValue(armourData.itemID, out item);
                Debug.Log($"Trying to add item [ Name: {armourData.itemName}, ID: {armourData.itemID} ] failed!  Item [ Name: {item.itemName} ] with that ID already exists!");
                continue;
            }

            NondegradableArmour armour = new NondegradableArmour(armourData);
            data.Add(armour.itemID, armour);
            AddToArmourList(new SetupItemStruct(armour.itemID, armour.itemName), armour.GetItemCategory());
        }

        //  Load augmented armour
        foreach (AugArmourSO armourData in items.augmentedArmourList)
        {
            if (data.ContainsKey(armourData.itemID))
            {
                SetupItem item;
                data.TryGetValue(armourData.itemID, out item);
                Debug.Log($"Trying to add item [ Name: {armourData.itemName}, ID: {armourData.itemID} ] failed!  Item [ Name: {item.itemName} ] with that ID already exists!");
                continue;
            }

            AugmentedArmour armour = new AugmentedArmour(armourData);
            data.Add(armour.itemID, armour);
            AddToArmourList(new SetupItemStruct(armour.itemID, armour.itemName), armour.GetItemCategory());
        }

        //  Load degradable armour
        foreach (DegradableArmourSO armourData in items.degradableArmourList)
        {
            if (data.ContainsKey(armourData.itemID))
            {
                SetupItem item;
                data.TryGetValue(armourData.itemID, out item);
                Debug.Log($"Trying to add item [ Name: {armourData.itemName}, ID: {armourData.itemID} ] failed!  Item [ Name: {item.itemName} ] with that ID already exists!");
                continue;
            }

            DegradableArmour armour = new DegradableArmour(armourData);
            data.Add(armour.itemID, armour);
            AddToArmourList(new SetupItemStruct(armour.itemID, armour.itemName), armour.GetItemCategory());
        }

        //  Load time-degrade armour
        foreach(TimeDegradeArmourSO armourData in items.timeDegradeArmourList)
        {
            if (data.ContainsKey(armourData.itemID))
            {
                SetupItem item;
                data.TryGetValue(armourData.itemID, out item);
                Debug.Log($"Trying to add item [ Name: {armourData.itemName}, ID: {armourData.itemID} ] failed!  Item [ Name: {item.itemName} ] with that ID already exists!");
                continue;
            }

            //create and add armour
            TimeDegradeArmour armour = new TimeDegradeArmour(armourData);
            data.Add(armour.itemID, armour);
            AddToArmourList(new SetupItemStruct(armour.itemID, armour.itemName), armour.GetItemCategory());
        }

        //  Load nondegradable weapons
        foreach(NondegradeWeaponSO weaponData in items.nondegradeWeaponList)
        {
            if (data.ContainsKey(weaponData.itemID))
            {
                SetupItem item;
                data.TryGetValue(weaponData.itemID, out item);
                Debug.Log($"Trying to add item [ Name: {weaponData.itemName}, ID: {weaponData.itemID} ] failed!  Item [ Name: {item.itemName} ] with that ID already exists!");
                continue;
            }

            NondegradableWeapon weapon = new NondegradableWeapon(weaponData);
            data.Add(weapon.itemID, weapon);
            AddToWeaponList(new SetupItemStruct(weapon.itemID, weapon.itemName), weapon.GetItemCategory());
        }

        //  Load augmented weapons
        foreach(AugWeaponSO weaponData in items.augmentedWeaponList)
        {
            if (data.ContainsKey(weaponData.itemID))
            {
                SetupItem item;
                data.TryGetValue(weaponData.itemID, out item);
                Debug.Log($"Trying to add item [ Name: {weaponData.itemName}, ID: {weaponData.itemID} ] failed!  Item [ Name: {item.itemName} ] with that ID already exists!");
                continue;
            }

            AugmentedWeapon weapon = new AugmentedWeapon(weaponData);
            data.Add(weapon.itemID, weapon);
            AddToWeaponList(new SetupItemStruct(weapon.itemID, weapon.itemName), weapon.GetItemCategory());
        }

        //  Load degradable weapons
        foreach(DegradableWeaponSO weaponData in items.degradableWeaponList)
        {
            if (data.ContainsKey(weaponData.itemID))
            {
                SetupItem item;
                data.TryGetValue(weaponData.itemID, out item);
                Debug.Log($"Trying to add item [ Name: {weaponData.itemName}, ID: {weaponData.itemID} ] failed!  Item [ Name: {item.itemName} ] with that ID already exists!");
                continue;
            }

            DegradableWeapon weapon = new DegradableWeapon(weaponData);
            data.Add(weapon.itemID, weapon);
            AddToWeaponList(new SetupItemStruct(weapon.itemID, weapon.itemName), weapon.GetItemCategory());
        }

        //  Load general items
        foreach(GeneralItemSO generalItemData in items.generalItemList)
        {
            if (data.ContainsKey(generalItemData.itemID))
            {
                SetupItem item;
                data.TryGetValue(generalItemData.itemID, out item);
                Debug.Log($"Trying to add item [ Name: {generalItemData.itemName}, ID: {generalItemData.itemID} ] failed!  Item [ Name: {item.itemName} ] with that ID already exists!");
                continue;
            }

            General genItem = new General(generalItemData);
            data.Add(genItem.itemID, genItem);
            generalItemList.Add(new SetupItemStruct(genItem.itemID, genItem.itemName));
        }

        //  Unload the ScriptableObjects collection
        Resources.UnloadAsset(items);

        //  Sort each list alphabetically
        foreach(List<SetupItemStruct> list in listSetupItemStructLists)
        {
            list.Sort();
        }

        //  Load prices from GDoc
        SpreadsheetManager.ReadPublicSpreadsheet(new GSTU_Search(sheetID, SHEETNAME, STARTCELL, ENDCELL), LoadPrices);
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

        //  Print all items to file if in editor
        if (Application.isEditor)
        {
            PrintDictionary();
            PrintToFile();
        }

        //  Dictionary done loading event
        EventManager.Instance.SetupItemDictionaryLoaded();
    }

    //  Get list of SetupItems from passed SetupItemCategory
    public static List<SetupItemStruct> GetItems(in SetupItemCategories setupItemCategory)
    {
        switch (setupItemCategory)
        {
            case SetupItemCategories.General:
                return generalItemList;
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
            case SetupItemCategories.Familiars:
                return pouchList;
            case SetupItemCategories.Scrolls:
                return scrollList;
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

    private static string PrintByType()
    {
        string info = "";

        info += $"General Items:\r\n{PrintAllOfType(in generalItemList)}";
        info += $"\r\nFood:\r\n{PrintAllOfType(in foodList)}";
        info += $"\r\nPotions:\r\n{PrintAllOfType(in potionList)}";
        info += $"\r\nHelmets:\r\n{PrintAllOfType(in helmList)}";
        info += $"\r\nPocket Items:\r\n{PrintAllOfType(in pocketList)}";
        info += $"\r\nCapes:\r\n{PrintAllOfType(in capeList)}";
        info += $"\r\nNecklaces and Amulets:\r\n{PrintAllOfType(in neckList)}";
        info += $"\r\nQuiver:\r\n{PrintAllOfType(in ammoList)}";
        info += $"\r\nBodies:\r\n{PrintAllOfType(in bodyList)}";
        info += $"\r\nLegs:\r\n{PrintAllOfType(in legList)}";
        info += $"\r\nGloves:\r\n{PrintAllOfType(in gloveList)}";
        info += $"\r\nBoots:\r\n{PrintAllOfType(in bootList)}";
        info += $"\r\nRing:\r\n{PrintAllOfType(in ringList)}";
        info += $"\r\nShield:\r\n{PrintAllOfType(in shieldList)}";
        info += $"\r\nMainhand:\r\n{PrintAllOfType(in mhWeaponList)}";
        info += $"\r\nOffhand:\r\n{PrintAllOfType(in ohWeaponList)}";
        info += $"\r\n2Hand:\r\n{PrintAllOfType(in twoHandWeaponList)}";
        info += $"\r\nSummoning Pouches:\r\n{PrintAllOfType(in pouchList)}";
        info += $"\r\nSummoning Scrolls:\r\n{PrintAllOfType(in scrollList)}";

        return info;
    }

    private static string PrintAllOfType(in List<SetupItemStruct> setupStructList)
    {
        string info = "";

        foreach (SetupItemStruct itemStruct in setupStructList)
            info += $"{itemStruct.itemName}\r\n";

        return info;
    }

    private static void PrintToFile()
    {
        try
        {
            FileStream fs = File.Create("E:\\setupItems.txt");
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.Write(PrintByType());
            }
        }
        catch(Exception e)
        {
            Debug.Log(e.StackTrace);
        }
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
