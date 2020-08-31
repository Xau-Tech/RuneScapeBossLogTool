using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNode : IEnumerable<TreeNode>
{
    private readonly List<TreeNode> children = new List<TreeNode>();

    public readonly SetupItemTypes.Values ItemType;
    public TreeNode Parent { get; private set; }

    public TreeNode(SetupItemTypes.Values itemType)
    {
        this.ItemType = itemType;
    }

    public List<SetupItemTypes.Values> GetChildren()
    {
        List<SetupItemTypes.Values> valueList = new List<SetupItemTypes.Values>();

        for(int i = 0; i < children.Count; ++i)
        {
            valueList.Add(children[i].ItemType);
        }

        return valueList;
    }

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
