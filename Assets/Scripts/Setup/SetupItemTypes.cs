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
        new TreeNode(SetupItemCategories.Potion)
        /*new TreeNode(SetupItemCategories.Armour)
        {
            new TreeNode(SetupItemCategories.Helm),
            new TreeNode(SetupItemCategories.Pocket),
            new TreeNode(SetupItemCategories.Cape),
            new TreeNode(SetupItemCategories.Neck),
            new TreeNode(SetupItemCategories.Ammunition),
            new TreeNode(SetupItemCategories.Body),
            new TreeNode(SetupItemCategories.Legs),
            new TreeNode(SetupItemCategories.Gloves),
            new TreeNode(SetupItemCategories.Boots),
            new TreeNode(SetupItemCategories.Ring),
            new TreeNode(SetupItemCategories.Shield)
        },*/
        //new TreeNode(SetupItemCategories.Potion)
        /*new TreeNode(SetupItemCategories.Weapons)
        {
            new TreeNode(SetupItemCategories.Mainhand)
            {
                new TreeNode(SetupItemCategories.DWMain),
                new TreeNode(SetupItemCategories.Twohand)
            },
            new TreeNode(SetupItemCategories.Offhand)
            {
                new TreeNode(SetupItemCategories.DWOff),
                new TreeNode(SetupItemCategories.Shield)
            }
        }*/
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
        switch (itemSlotCategory)
        {
            case ItemSlotCategories.Inventory:
                setupItemCategories = setupItemTypesTree.GetChildren();
                return true;
            case ItemSlotCategories.Food:
                setupItemCategories = null;
                return false;
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
            case ItemSlotCategories.Food:
                return SetupItemCategories.Food;
            default:
                return SetupItemCategories.All;
        }
    }
}

//  Enum Categories
public enum SetupItemCategories { All, General, Food, Potion,  Armour, Helm, Pocket, Cape, Neck, Ammunition, Body, Legs, Gloves, Boots, Ring, Shield};