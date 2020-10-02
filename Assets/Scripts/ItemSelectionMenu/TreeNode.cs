using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Quick and dirty node to create a tree of SetupItemCategories
public class TreeNode : IEnumerable<TreeNode>
{
    private readonly List<TreeNode> children = new List<TreeNode>();

    public readonly SetupItemCategories ItemType;
    public TreeNode Parent { get; private set; }

    public TreeNode(SetupItemCategories itemType)
    {
        this.ItemType = itemType;
    }

    //  Check if this node has children
    public bool HasChildren()
    {
        if (children.Count == 0)
            return false;
        else
            return true;
    }

    //  Return a list of this node's children
    public List<SetupItemCategories> GetChildren()
    {
        List<SetupItemCategories> valueList = new List<SetupItemCategories>();

        for(int i = 0; i < children.Count; ++i)
        {
            valueList.Add(children[i].ItemType);
        }

        return valueList;
    }

    //  Add a passed node as child to this
    public void Add(TreeNode node)
    {
        if(node.Parent != null)
        {
            node.Parent.children.Remove(node);
        }

        node.Parent = this;
        this.children.Add(node);
    }

    public IEnumerator<TreeNode> GetEnumerator()
    {
        return this.children.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
