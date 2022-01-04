using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps track of how all setup item categories and item slot categories relate
/// </summary>
public static class SetupItemTypes
{
    //  Properties & fields

    private static TreeNode _setupItemTypesTree = new TreeNode(Enums.SetupItemCategory.All)
    {
        new TreeNode(Enums.SetupItemCategory.General),
        new TreeNode(Enums.SetupItemCategory.Summoning)
        {
            new TreeNode(Enums.SetupItemCategory.Familiars),
            new TreeNode(Enums.SetupItemCategory.Scrolls)
        },
        new TreeNode(Enums.SetupItemCategory.Food),
        new TreeNode(Enums.SetupItemCategory.Potion),
        new TreeNode(Enums.SetupItemCategory.Armour)
        {
            new TreeNode(Enums.SetupItemCategory.Body),
            new TreeNode(Enums.SetupItemCategory.Head),
            new TreeNode(Enums.SetupItemCategory.Legs),
            new TreeNode(Enums.SetupItemCategory.Neck),
            new TreeNode(Enums.SetupItemCategory.Pocket),
            new TreeNode(Enums.SetupItemCategory.Cape),
            new TreeNode(Enums.SetupItemCategory.Ammunition),
            new TreeNode(Enums.SetupItemCategory.Gloves),
            new TreeNode(Enums.SetupItemCategory.Boots),
            new TreeNode(Enums.SetupItemCategory.Ring),
            new TreeNode(Enums.SetupItemCategory.Shield)
        },
        new TreeNode(Enums.SetupItemCategory.Weapon)
        {
            new TreeNode(Enums.SetupItemCategory.Mainhand),
            new TreeNode(Enums.SetupItemCategory.Offhand),
            new TreeNode(Enums.SetupItemCategory.TwoHand)
        }
    };

    //  Methods

    //  Find children of a node from SetupItemCategory
    public static bool TryGetSubcategories(Enums.SetupItemCategory category, out List<Enums.SetupItemCategory> setupItemCategories)
    {
        if (category == Enums.SetupItemCategory.All)
        {
            setupItemCategories = _setupItemTypesTree.GetChildren();
            return true;
        }
        else
        {
            foreach(TreeNode node in _setupItemTypesTree)
            {
                if(node.ItemType == category)
                {
                    setupItemCategories = node.GetChildren();
                    return setupItemCategories.Count != 0;
                }
            }
        }

        setupItemCategories = null;
        return false;
    }

    //  Find children of a node from ItemSlotCategory
    public static bool TryGetSubcategories(Enums.ItemSlotCategory category, out List<Enums.SetupItemCategory> setupItemCategories)
    {
        setupItemCategories = new List<Enums.SetupItemCategory>();

        switch (category)
        {
            case Enums.ItemSlotCategory.Inventory:
                setupItemCategories = _setupItemTypesTree.GetChildren();
                return true;
            case Enums.ItemSlotCategory.Mainhand:
                setupItemCategories.Add(Enums.SetupItemCategory.Mainhand);
                setupItemCategories.Add(Enums.SetupItemCategory.TwoHand);
                return true;
            case Enums.ItemSlotCategory.Offhand:
                setupItemCategories.Add(Enums.SetupItemCategory.Offhand);
                setupItemCategories.Add(Enums.SetupItemCategory.Shield);
                return true;
            default:
                setupItemCategories = null;
                return false;
        }
    }

    //  Get SetupItemCategory from ItemSlotCategory
    public static Enums.SetupItemCategory GetSetupItemCategory(Enums.ItemSlotCategory category)
    {
        switch (category)
        {
            case Enums.ItemSlotCategory.Inventory:
                return Enums.SetupItemCategory.All;
            case Enums.ItemSlotCategory.Head:
                return Enums.SetupItemCategory.Head;
            case Enums.ItemSlotCategory.Pocket:
                return Enums.SetupItemCategory.Pocket;
            case Enums.ItemSlotCategory.Cape:
                return Enums.SetupItemCategory.Cape;
            case Enums.ItemSlotCategory.Necklace:
                return Enums.SetupItemCategory.Neck;
            case Enums.ItemSlotCategory.Ammunition:
                return Enums.SetupItemCategory.Ammunition;
            case Enums.ItemSlotCategory.Body:
                return Enums.SetupItemCategory.Body;
            case Enums.ItemSlotCategory.Legs:
                return Enums.SetupItemCategory.Legs;
            case Enums.ItemSlotCategory.Gloves:
                return Enums.SetupItemCategory.Gloves;
            case Enums.ItemSlotCategory.Boots:
                return Enums.SetupItemCategory.Boots;
            case Enums.ItemSlotCategory.Ring:
                return Enums.SetupItemCategory.Ring;
            case Enums.ItemSlotCategory.Familiar:
                return Enums.SetupItemCategory.Familiars;
            case Enums.ItemSlotCategory.Scroll:
                return Enums.SetupItemCategory.Scrolls;
            default:
                return Enums.SetupItemCategory.None;
        }
    }
}
