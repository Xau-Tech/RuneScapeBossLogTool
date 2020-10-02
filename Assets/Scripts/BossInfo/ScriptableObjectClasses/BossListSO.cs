using System.Collections.Generic;
using UnityEngine;

//  Data holder for all bosses in rs3 and osrs
[CreateAssetMenu(menuName = "Bosses/Database", order = 1)]
public class BossListSO : ScriptableObject
{
    public List<BossDataSO> rs3BossList = new List<BossDataSO>();
    public List<BossDataSO> osrsBossList = new List<BossDataSO>();

    public List<BossDataSO> GetBossData(in string versionOption)
    {
        if (versionOption.CompareTo("RS3") == 0)
            return rs3BossList;
        else
            return osrsBossList;
    }
}
