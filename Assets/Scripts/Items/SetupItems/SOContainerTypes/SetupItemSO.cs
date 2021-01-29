using UnityEngine;

//  ScriptableObject data container for SetupItems
public abstract class SetupItemSO : ScriptableObject
{
    public int itemID;
    public string itemName;
    public bool isStackable;
    public Sprite itemSprite;
}
