using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  Data holder for an individual boss in either rs3 or osrs
 *  Base instance cost is the coin value (0 if instance requires an item or no cost)
 */
[CreateAssetMenu(menuName = "Bosses/BossInfo", order = 0)]
[System.Serializable]
public class BossDataSO : ScriptableObject
{
    public short bossID;
    public string bossName;
    public bool hasAccessToRareDropTable;
    public uint baseInstanceCost;
}
