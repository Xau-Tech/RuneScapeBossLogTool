using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bosses/Database", order = 1)]
public class BossListSO : ScriptableObject
{
    public List<BossInfoSO> RS3Bosses = new List<BossInfoSO>();
    public List<BossInfoSO> OSRSBosses = new List<BossInfoSO>();

    public List<BossInfoSO> GetBosses(string rsVersion)
    {
        if (rsVersion.CompareTo("RS3") == 0)
            return RS3Bosses;
        else
            return OSRSBosses;
    }
}
