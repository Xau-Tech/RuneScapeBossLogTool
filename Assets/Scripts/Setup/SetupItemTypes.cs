using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Class for the tree of SetupItemCategories
public static class SetupItemTypes
{
    //  Tree of SetupItemCategories
    private static TreeNode setupItemTypesTree = new TreeNode(SetupItemCategories.All)
    {
        new TreeNode(SetupItemCategories.Food),
        new TreeNode(SetupItemCategories.Potion),
        new TreeNode(SetupItemCategories.Armour)
        {
            new TreeNode(SetupItemCategories.Body),
            new TreeNode(SetupItemCategories.Head),
            new TreeNode(SetupItemCategories.Legs),
            new TreeNode(SetupItemCategories.Neck),
            new TreeNode(SetupItemCategories.Pocket),
            new TreeNode(SetupItemCategories.Cape),
            new TreeNode(SetupItemCategories.Ammunition),
            new TreeNode(SetupItemCategories.Gloves),
            new TreeNode(SetupItemCategories.Boots),
            new TreeNode(SetupItemCategories.Ring),
            new TreeNode(SetupItemCategories.Shield)
        },
        new TreeNode(SetupItemCategories.Weapon)
        {
            new TreeNode(SetupItemCategories.Mainhand),
            new TreeNode(SetupItemCategories.Offhand),
            new TreeNode(SetupItemCategories.TwoHand)
        }
    };

    //  Find children of a node from passed SetupItemCategory
    public static bool TryGetSubcategories(in SetupItemCategories category, out List<SetupItemCategories> setupItemCategories)
    {
        if(category == SetupItemCategories.All)
        {
            setupItemCategories = setupItemTypesTree.GetChildren();
            return true;
        }
        else
        {
            foreach(TreeNode node in setupItemTypesTree)
            {
                if(node.ItemType == category)
                {
                    setupItemCategories = node.GetChildren();

                    if (setupItemCategories.Count == 0)
                        return false;
                    else
                        return true;
                }
            }
        }

        setupItemCategories = null;
        return false;
    }

    //  Find children of a node from passed ItemSlotCategory
    public static bool TryGetSubcategories(in ItemSlotCategories itemSlotCategory, out List<SetupItemCategories> setupItemCategories)
    {
        setupItemCategories = new List<SetupItemCategories>();

        switch (itemSlotCategory)
        {
            case ItemSlotCategories.Inventory:
                setupItemCategories = setupItemTypesTree.GetChildren();
                return true;
            case ItemSlotCategories.Mainhand:
                setupItemCategories.Add(SetupItemCategories.Mainhand);
                setupItemCategories.Add(SetupItemCategories.TwoHand);
                return true;
            case ItemSlotCategories.Offhand:
                setupItemCategories.Add(SetupItemCategories.Offhand);
                setupItemCategories.Add(SetupItemCategories.Shield);
                return true;
            default:
                setupItemCategories = null;
                return false;
        }
    }
        
    //  Swap from ItemSlotCategory to SetupItemCategory
    public static SetupItemCategories GetCategoryFromSlotType(in ItemSlotCategories itemSlotCategory)
    {
        switch (itemSlotCategory)
        {
            case ItemSlotCategories.Inventory:
                return SetupItemCategories.All;
            case ItemSlotCategories.Head:
                return SetupItemCategories.Head;
            case ItemSlotCategories.Pocket:
                return SetupItemCategories.Pocket;
            case ItemSlotCategories.Cape:
                return SetupItemCategories.Cape;
            case ItemSlotCategories.Necklace:
                return SetupItemCategories.Neck;
            case ItemSlotCategories.Ammunition:
                return SetupItemCategories.Ammunition;
            case ItemSlotCategories.Body:
                return SetupItemCategories.Body;
            case ItemSlotCategories.Legs:
                return SetupItemCategories.Legs;
            case ItemSlotCategories.Gloves:
                return SetupItemCategories.Gloves;
            case ItemSlotCategories.Boots:
                return SetupItemCategories.Boots;
            case ItemSlotCategories.Ring:
                return SetupItemCategories.Ring;
            default:
                return SetupItemCategories.None;
        }
    }
}

//  Enum Categories
public enum SetupItemCategories { All, General, Food, Potion,  Armour, Head, Pocket, Cape, Neck, Ammunition, Body, Legs, Gloves, Boots, Ring, Shield, Weapon, Mainhand, TwoHand, Offhand, None};