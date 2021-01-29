using UnityEngine;

//  Data holder for time degrading armor
[CreateAssetMenu(fileName = "TimeDegradeArmour", menuName = "Setup/Armour/TimeDegrade", order = 3)]
public class TimeDegradeArmourSO : EquippedItemSO
{
    public int maxChargeInHours;
}
