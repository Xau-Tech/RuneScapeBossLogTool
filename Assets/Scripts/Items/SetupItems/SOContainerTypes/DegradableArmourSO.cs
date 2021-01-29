using UnityEngine;

//  Data holder for degradable armor
[CreateAssetMenu(fileName = "DegradableArmour", menuName = "Setup/Armour/Degradable", order = 2)]
public class DegradableArmourSO : EquippedItemSO
{
    public int effectiveCharges;
    public bool smithingCostReduction;
}
