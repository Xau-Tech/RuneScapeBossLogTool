using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using GoogleSheetsToUnity;
using System.IO;
using System;
using Newtonsoft.Json.Linq;

public static class SetupItemDictionary
{
    //  Properties & fields

    //  Link to ScriptableObject
    private static SetupItemCollectionSO items;
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

    private static readonly string _SHEETNAME = "SetupItems";
    private static readonly string _STARTCELL = "A1";
    private static readonly string _ENDCELL = "Z200";

    //  Methods

    public static void LoadResource()
    {
        items =  Resources.Load<SetupItemCollectionSO>("SetupItems/SetupItemsDB");
    }

    //  Load all scriptable objects into run-time setup items
    public async static Task<string> LoadItems()
    {
        //  Load food
        foreach (FoodSO foodData in items.foodList)
        {
            //  Make sure this itemID isn't already taken; typos or mistakes in data entry
            if (data.ContainsKey(foodData._itemId))
            {
                SetupItem item;
                data.TryGetValue(foodData._itemId, out item);
                Debug.Log($"Trying to add item [ Name: {foodData._itemName}, ID: {foodData._itemId} ] failed!  Item [ Name: {item.ItemName} ] with that ID already exists!");
                continue;
            }

            Food food = new Food(foodData);
            foodList.Add(new SetupItemStruct(foodData._itemId, foodData._itemName));
            data.Add(food.ItemId, food);
        }

        //  Load potions
        foreach (PotionSO potionData in items.potionList)
        {
            if (data.ContainsKey(potionData._itemId))
            {
                SetupItem item;
                data.TryGetValue(potionData._itemId, out item);
                Debug.Log($"Trying to add item [ Name: {potionData._itemName}, ID: {potionData._itemId} ] failed!  Item [ Name: {item.ItemName} ] with that ID already exists!");
                continue;
            }

            Potion potion = new Potion(potionData);
            potionList.Add(new SetupItemStruct(potionData._itemId, potionData._itemName));
            data.Add(potion.ItemId, potion);
        }

        //  Load pouches
        foreach (SummPouchSO pouchData in items.summoningPouchList)
        {
            if (data.ContainsKey(pouchData._itemId))
            {
                SetupItem item;
                data.TryGetValue(pouchData._itemId, out item);
                Debug.Log($"Trying to add item [ Name: {pouchData._itemName}, ID: {pouchData._itemId} ] failed!  Item [ Name: {item.ItemName} ] with that ID already exists!");
                continue;
            }

            SummoningPouch pouch = new SummoningPouch(pouchData);
            pouchList.Add(new SetupItemStruct(pouchData._itemId, pouchData._itemName));
            data.Add(pouch.ItemId, pouch);
        }

        //  Load scrolls
        foreach (SummScrollSO scrollData in items.summoningScrollList)
        {
            if (data.ContainsKey(scrollData._itemId))
            {
                SetupItem item;
                data.TryGetValue(scrollData._itemId, out item);
                Debug.Log($"Trying to add item [ Name: {scrollData._itemName}, ID: {scrollData._itemId} ] failed!  Item [ Name: {item.ItemName} ] with that ID already exists!");
                continue;
            }

            SummoningScroll scroll = new SummoningScroll(scrollData);
            scrollList.Add(new SetupItemStruct(scrollData._itemId, scrollData._itemName));
            data.Add(scroll.ItemId, scroll);
        }

        //  Load nondegradeable armour
        foreach (NondegradeArmourSO armourData in items.nondegradeArmourList)
        {
            if (data.ContainsKey(armourData._itemId))
            {
                SetupItem item;
                data.TryGetValue(armourData._itemId, out item);
                Debug.Log($"Trying to add item [ Name: {armourData._itemName}, ID: {armourData._itemId} ] failed!  Item [ Name: {item.ItemName} ] with that ID already exists!");
                continue;
            }

            NondegradableArmour armour = new NondegradableArmour(armourData);
            data.Add(armour.ItemId, armour);
            AddToArmourList(new SetupItemStruct(armour.ItemId, armour.ItemName), armour.GetItemCategory());
        }

        //  Load augmented armour
        foreach (AugArmourSO armourData in items.augmentedArmourList)
        {
            if (data.ContainsKey(armourData._itemId))
            {
                SetupItem item;
                data.TryGetValue(armourData._itemId, out item);
                Debug.Log($"Trying to add item [ Name: {armourData._itemName}, ID: {armourData._itemId} ] failed!  Item [ Name: {item.ItemName} ] with that ID already exists!");
                continue;
            }

            AugmentedArmour armour = new AugmentedArmour(armourData);
            data.Add(armour.ItemId, armour);
            AddToArmourList(new SetupItemStruct(armour.ItemId, armour.ItemName), armour.GetItemCategory());
        }

        //  Load degradable armour
        foreach (DegradableArmourSO armourData in items.degradableArmourList)
        {
            if (data.ContainsKey(armourData._itemId))
            {
                SetupItem item;
                data.TryGetValue(armourData._itemId, out item);
                Debug.Log($"Trying to add item [ Name: {armourData._itemName}, ID: {armourData._itemId} ] failed!  Item [ Name: {item.ItemName} ] with that ID already exists!");
                continue;
            }

            DegradableArmour armour = new DegradableArmour(armourData);
            data.Add(armour.ItemId, armour);
            AddToArmourList(new SetupItemStruct(armour.ItemId, armour.ItemName), armour.GetItemCategory());
        }

        //  Load time-degrade armour
        foreach (TimeDegradeArmourSO armourData in items.timeDegradeArmourList)
        {
            if (data.ContainsKey(armourData._itemId))
            {
                SetupItem item;
                data.TryGetValue(armourData._itemId, out item);
                Debug.Log($"Trying to add item [ Name: {armourData._itemName}, ID: {armourData._itemId} ] failed!  Item [ Name: {item.ItemName} ] with that ID already exists!");
                continue;
            }

            //create and add armour
            TimeDegradeArmour armour = new TimeDegradeArmour(armourData);
            data.Add(armour.ItemId, armour);
            AddToArmourList(new SetupItemStruct(armour.ItemId, armour.ItemName), armour.GetItemCategory());
        }

        //  Load nondegradable weapons
        foreach (NondegradeWeaponSO weaponData in items.nondegradeWeaponList)
        {
            if (data.ContainsKey(weaponData._itemId))
            {
                SetupItem item;
                data.TryGetValue(weaponData._itemId, out item);
                Debug.Log($"Trying to add item [ Name: {weaponData._itemName}, ID: {weaponData._itemId} ] failed!  Item [ Name: {item.ItemName} ] with that ID already exists!");
                continue;
            }

            NondegradableWeapon weapon = new NondegradableWeapon(weaponData);
            data.Add(weapon.ItemId, weapon);
            AddToWeaponList(new SetupItemStruct(weapon.ItemId, weapon.ItemName), weapon.GetItemCategory());
        }

        //  Load augmented weapons
        foreach (AugWeaponSO weaponData in items.augmentedWeaponList)
        {
            if (data.ContainsKey(weaponData._itemId))
            {
                SetupItem item;
                data.TryGetValue(weaponData._itemId, out item);
                Debug.Log($"Trying to add item [ Name: {weaponData._itemName}, ID: {weaponData._itemId} ] failed!  Item [ Name: {item.ItemName} ] with that ID already exists!");
                continue;
            }

            AugmentedWeapon weapon = new AugmentedWeapon(weaponData);
            data.Add(weapon.ItemId, weapon);
            AddToWeaponList(new SetupItemStruct(weapon.ItemId, weapon.ItemName), weapon.GetItemCategory());
        }

        //  Load degradable weapons
        foreach (DegradableWeaponSO weaponData in items.degradableWeaponList)
        {
            if (data.ContainsKey(weaponData._itemId))
            {
                SetupItem item;
                data.TryGetValue(weaponData._itemId, out item);
                Debug.Log($"Trying to add item [ Name: {weaponData._itemName}, ID: {weaponData._itemId} ] failed!  Item [ Name: {item.ItemName} ] with that ID already exists!");
                continue;
            }

            DegradableWeapon weapon = new DegradableWeapon(weaponData);
            data.Add(weapon.ItemId, weapon);
            AddToWeaponList(new SetupItemStruct(weapon.ItemId, weapon.ItemName), weapon.GetItemCategory());
        }

        //  Load general items
        foreach (GeneralItemSO generalItemData in items.generalItemList)
        {
            if (data.ContainsKey(generalItemData._itemId))
            {
                SetupItem item;
                data.TryGetValue(generalItemData._itemId, out item);
                Debug.Log($"Trying to add item [ Name: {generalItemData._itemName}, ID: {generalItemData._itemId} ] failed!  Item [ Name: {item.ItemName} ] with that ID already exists!");
                continue;
            }

            General genItem = new General(generalItemData);
            data.Add(genItem.ItemId, genItem);
            generalItemList.Add(new SetupItemStruct(genItem.ItemId, genItem.ItemName));
        }

        //  Sort each list alphabetically
        foreach (List<SetupItemStruct> list in listSetupItemStructLists)
        {
            list.Sort();
        }

        Debug.Log("Done loading setup items from scriptable objects");
        return "";
    }

    private static void AddToArmourList(SetupItemStruct itemStruct, Enums.SetupItemCategory itemCategory)
    {
        switch (itemCategory)
        {
            case Enums.SetupItemCategory.Head:
                helmList.Add(itemStruct);
                break;
            case Enums.SetupItemCategory.Pocket:
                pocketList.Add(itemStruct);
                break;
            case Enums.SetupItemCategory.Cape:
                capeList.Add(itemStruct);
                break;
            case Enums.SetupItemCategory.Neck:
                neckList.Add(itemStruct);
                break;
            case Enums.SetupItemCategory.Ammunition:
                ammoList.Add(itemStruct);
                break;
            case Enums.SetupItemCategory.Body:
                bodyList.Add(itemStruct);
                break;
            case Enums.SetupItemCategory.Legs:
                legList.Add(itemStruct);
                break;
            case Enums.SetupItemCategory.Gloves:
                gloveList.Add(itemStruct);
                break;
            case Enums.SetupItemCategory.Boots:
                bootList.Add(itemStruct);
                break;
            case Enums.SetupItemCategory.Ring:
                ringList.Add(itemStruct);
                break;
            case Enums.SetupItemCategory.Shield:
                shieldList.Add(itemStruct);
                break;
            default:
                Debug.Log($"Item \"{itemStruct.ItemName}\" with category {itemCategory.ToString()} could not be added because that category list does not exist!");
                break;
        }
    }

    private static void AddToWeaponList(SetupItemStruct itemStruct, Enums.SetupItemCategory itemCategory)
    {
        switch (itemCategory)
        {
            case Enums.SetupItemCategory.Mainhand:
                mhWeaponList.Add(itemStruct);
                break;
            case Enums.SetupItemCategory.TwoHand:
                twoHandWeaponList.Add(itemStruct);
                break;
            case Enums.SetupItemCategory.Offhand:
                ohWeaponList.Add(itemStruct);
                break;
            default:
                break;
        }
    }

    public static void LoadPrices(string mongoResponse)
    {
        var itemArr = JArray.Parse(mongoResponse);
        SetupItem setupItem;

        foreach(var mongoItem in itemArr)
        {
            int itemId = Convert.ToInt32(mongoItem["itemId"]);

            if(!data.TryGetValue(itemId, out setupItem))
            {
                Debug.Log($"No match for itemid {itemId} in the SetupItemsDictionary!");
                continue;
            }

            setupItem.Price = Convert.ToUInt32(mongoItem["price"]);
        }

        //  Print all items to file if in editor
        if (Application.isEditor)
        {
            PrintDictionary();
            PrintToFile();
        }
    }

    //  Get list of SetupItems from passed SetupItemCategory
    public static List<SetupItemStruct> GetItems(in Enums.SetupItemCategory setupItemCategory)
    {
        switch (setupItemCategory)
        {
            case Enums.SetupItemCategory.General:
                return generalItemList;
            case Enums.SetupItemCategory.Food:
                return foodList;
            case Enums.SetupItemCategory.Potion:
                return potionList;
            case Enums.SetupItemCategory.Body:
                return bodyList;
            case Enums.SetupItemCategory.Legs:
                return legList;
            case Enums.SetupItemCategory.Head:
                return helmList;
            case Enums.SetupItemCategory.Neck:
                return neckList;
            case Enums.SetupItemCategory.Cape:
                return capeList;
            case Enums.SetupItemCategory.Gloves:
                return gloveList;
            case Enums.SetupItemCategory.Boots:
                return bootList;
            case Enums.SetupItemCategory.Pocket:
                return pocketList;
            case Enums.SetupItemCategory.Ammunition:
                return ammoList;
            case Enums.SetupItemCategory.Ring:
                return ringList;
            case Enums.SetupItemCategory.Mainhand:
                return mhWeaponList;
            case Enums.SetupItemCategory.Offhand:
                return ohWeaponList;
            case Enums.SetupItemCategory.Shield:
                return shieldList;
            case Enums.SetupItemCategory.TwoHand:
                return twoHandWeaponList;
            case Enums.SetupItemCategory.Familiars:
                return pouchList;
            case Enums.SetupItemCategory.Scrolls:
                return scrollList;
            default:
                return null;
        }
    }

    //  Return a copy of the item
    public static bool TryGetItem(int itemId, out SetupItem setupItem)
    {
        SetupItem itemToCopy;

        if (data.TryGetValue(itemId, out itemToCopy))
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

        info += $"General Items:\r\n{PrintAllOfType(generalItemList)}";
        info += $"\r\nFood:\r\n{PrintAllOfType(foodList)}";
        info += $"\r\nPotions:\r\n{PrintAllOfType(potionList)}";
        info += $"\r\nHelmets:\r\n{PrintAllOfType(helmList)}";
        info += $"\r\nPocket Items:\r\n{PrintAllOfType(pocketList)}";
        info += $"\r\nCapes:\r\n{PrintAllOfType(capeList)}";
        info += $"\r\nNecklaces and Amulets:\r\n{PrintAllOfType(neckList)}";
        info += $"\r\nQuiver:\r\n{PrintAllOfType(ammoList)}";
        info += $"\r\nBodies:\r\n{PrintAllOfType(bodyList)}";
        info += $"\r\nLegs:\r\n{PrintAllOfType(legList)}";
        info += $"\r\nGloves:\r\n{PrintAllOfType(gloveList)}";
        info += $"\r\nBoots:\r\n{PrintAllOfType(bootList)}";
        info += $"\r\nRing:\r\n{PrintAllOfType(ringList)}";
        info += $"\r\nShield:\r\n{PrintAllOfType(shieldList)}";
        info += $"\r\nMainhand:\r\n{PrintAllOfType(mhWeaponList)}";
        info += $"\r\nOffhand:\r\n{PrintAllOfType(ohWeaponList)}";
        info += $"\r\n2Hand:\r\n{PrintAllOfType(twoHandWeaponList)}";
        info += $"\r\nSummoning Pouches:\r\n{PrintAllOfType(pouchList)}";
        info += $"\r\nSummoning Scrolls:\r\n{PrintAllOfType(scrollList)}";

        return info;
    }

    private static string PrintAllOfType(List<SetupItemStruct> setupStructList)
    {
        string info = "";

        foreach (SetupItemStruct itemStruct in setupStructList)
            info += $"{itemStruct.ItemName}\r\n";

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
        catch (Exception e)
        {
            Debug.Log(e.StackTrace);
        }
    }
}
