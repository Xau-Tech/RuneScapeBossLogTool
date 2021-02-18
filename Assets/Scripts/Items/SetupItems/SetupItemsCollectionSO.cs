using System.Collections.Generic;
using UnityEngine;

//  ScriptableObject class to hold ScriptableObjects of all SetupItems
[CreateAssetMenu(fileName = "SetupItemsDB", menuName = "Setup/SetupItemsDB", order = 0)]
public class SetupItemsCollectionSO : ScriptableObject
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
    public List<SummoningPouchSO> summoningPouchList;
    public List<SummoningScrollSO> summoningScrollList;
}