using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject to hold all setup item scriptable objects to load from
/// </summary>
[CreateAssetMenu(fileName = "SetupItemsDB", menuName = "Setup/SetupItemsDB", order = 0)]
public class SetupItemCollectionSO : ScriptableObject
{
    public List<SetupItemSO> foodList;
    public List<SetupItemSO> potionList;
    public List<NondegradeArmourSO> nondegradeArmourList;
    public List<AugArmourSO> augmentedArmourList;
    public List<DegradableArmourSO> degradableArmourList;
    public List<TimeDegradeArmourSO> timeDegradeArmourList;
    public List<NondegradeWeaponSO> nondegradeWeaponList;
    public List<AugWeaponSO> augmentedWeaponList;
    public List<DegradableWeaponSO> degradableWeaponList;
    public List<GeneralItemSO> generalItemList;
    public List<SummPouchSO> summoningPouchList;
    public List<SummScrollSO> summoningScrollList;
}
