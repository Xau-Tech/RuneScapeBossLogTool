using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupItemTypes
{
    public enum Values { All, Food, Potion, Weapons, Mainhand, DWMain, Twohand, Offhand, DWOff, Shield };

    private TreeNode setupItemTypesTree = new TreeNode(Values.All)
    {
        new TreeNode(Values.Food),
        new TreeNode(Values.Potion),
        new TreeNode(Values.Weapons)
        {
            new TreeNode(Values.Mainhand)
            {
                new TreeNode(Values.DWMain),
                new TreeNode(Values.Twohand)
            },
            new TreeNode(Values.Offhand)
            {
                new TreeNode(Values.DWOff),
                new TreeNode(Values.Shield)
            }
        }
    };

    public List<Values> GetSubcategories(Values value)
    {
        if(value == Values.All)
            return setupItemTypesTree.GetChildren();
        else
        {
            foreach(TreeNode node in setupItemTypesTree)
            {
                if (node.ItemType == value)
                    return node.GetChildren();
            }
        }

        return new List<Values>();
    }
}
