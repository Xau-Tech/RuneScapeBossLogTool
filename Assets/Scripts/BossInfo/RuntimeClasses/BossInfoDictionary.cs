using System.Collections.Generic;
using UnityEngine;

//  List of the boss info from Resources/BossData/BossInfoLists.assets
public class BossInfoDictionary
{
    public BossInfoDictionary()
    {
        data = new Dictionary<short, BossInfo>();
        nameToIDDictionary = new Dictionary<string, short>();
    }

    private Dictionary<short, BossInfo> data;
    private Dictionary<string, short> nameToIDDictionary;

    public void Load(in string rsVersion)
    {
        BossListSO bossList = Resources.Load<BossListSO>("BossData/BossInfoLists");
        List<BossDataSO> bossData = bossList.GetBossData(in rsVersion);

        for(int i = 0; i < bossData.Count; ++i)
        {
            if (data.ContainsKey(bossData[i].bossID))
                throw new System.Exception($"Duplicate BossID found on ID {bossData[i].bossID}");

            data.Add(bossData[i].bossID,
                new BossInfo(bossData[i].bossID, bossData[i].bossName, bossData[i].hasAccessToRareDropTable, bossData[i].baseInstanceCost, bossData[i].combatData));

            nameToIDDictionary.Add(bossData[i].bossName, bossData[i].bossID);
        }

        Resources.UnloadAsset(bossList);

        Print();
        CacheManager.currentBoss = FirstBossAlphabetically();
    }

    //  Returns a sorted string list of boss names; used for dropdown.options parameter
    public List<string> GetBossNames()
    {
        List<string> temp = new List<string>();

        foreach(var boss in data.Values)
        {
            temp.Add(boss.bossName);
        }

        temp.Sort();
        return temp;
    }

    //  Get boss name via id
    public string GetBossName(in short bossID)
    {
        BossInfo boss = null;
        data.TryGetValue(bossID, out boss);
        return boss.bossName;
    }

    //  Get list of all boss ids
    public List<short> GetBossIDs()
    {
        List<short> ids = new List<short>();

        foreach (short id in data.Keys)
            ids.Add(id);

        return ids;
    }

    //  Get id via name
    public short GetBossIDByName(in string name)
    {
        if (!nameToIDDictionary.ContainsKey(name))
            return -1;
        else
        {
            short bossID;
            nameToIDDictionary.TryGetValue(name, out bossID);
            return bossID;
        }
    }

    //  Get boss via id
    public BossInfo GetBossByID(in short bossID)
    {
        if (!data.ContainsKey(bossID))
            return null;
        else
        {
            BossInfo boss;
            data.TryGetValue(bossID, out boss);
            return boss;
        }
    }

    //  Get boss via name
    public BossInfo GetBossByName(in string name)
    {
        if (!nameToIDDictionary.ContainsKey(name))
            return null;
        else
        {
            return GetBossByID(GetBossIDByName(in name));
        }
    }

    //  Get first boss alphabetically
    public BossInfo FirstBossAlphabetically()
    {
        BossInfo bossToReturn = new BossInfo() { bossName = "Z" };

        foreach(BossInfo boss in data.Values)
        {
            if (bossToReturn.CompareTo(boss) > 0)
                bossToReturn = boss;
        }

        Debug.Log($"First boss is {bossToReturn.bossName}");
        return bossToReturn;
    }

    public void Print()
    {
        string result = "BossInfoDictionary:";
        foreach(var info in data.Values)
            result += $"\n{info.ToString()}";

        Debug.Log(result);
    }
}
