using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Setup/Database", fileName = "SetupItemDB", order = 1)]
public class SetupItemDB : ScriptableObject
{
    public List<SetupItem> items;
}
