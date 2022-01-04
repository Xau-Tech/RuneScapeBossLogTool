using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNode : IEnumerable<TreeNode>
{
    //  Properties & fields

    public readonly Enums.SetupItemCategory ItemType;
    public TreeNode Parent { get; private set; }

    private readonly List<TreeNode> _children = new List<TreeNode>();

    //  Constructor

    public TreeNode(Enums.SetupItemCategory itemType)
    {
        this.ItemType = itemType;
    }

    //  Methods

    public bool HasChildren()
    {
        return _children.Count != 0;
    }

    public List<Enums.SetupItemCategory> GetChildren()
    {
        List<Enums.SetupItemCategory> childList = new List<Enums.SetupItemCategory>();

        foreach(TreeNode child in _children)
        {
            childList.Add(child.ItemType);
        }

        return childList;
    }

    public void Add(TreeNode node)
    {
        if (node.Parent != null)
            node.Parent._children.Remove(node);

        node.Parent = this;
        this._children.Add(node);
    }

    public IEnumerator<TreeNode> GetEnumerator()
    {
        return this._children.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
