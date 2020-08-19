using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SetupItemsDB", menuName = "Setup/SetupItemsDB", order = 0)]
public class SetupItemsDB : ScriptableObject, IEnumerable
{
    public List<Food> foodList;
    public List<Potion> potionList;

    private Dictionary<string, SetupItem> setupItemsDictionary = new Dictionary<string, SetupItem>();

    public IEnumerator GetEnumerator()
    {
        return ((IEnumerable)setupItemsDictionary).GetEnumerator();
    }
}
