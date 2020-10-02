using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  ScriptableObject class to hold ScriptableObjects of all SetupItems
[CreateAssetMenu(fileName = "SetupItemsDB", menuName = "Setup/SetupItemsDB", order = 0)]
public class SetupItemsCollectionSO : ScriptableObject
{
    public List<SetupItem> foodList;
    public List<SetupItem> potionList;
}
