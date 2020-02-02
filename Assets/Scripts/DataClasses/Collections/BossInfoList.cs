using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  List of the boss info grabbed from /StreamingAssets/bossinfo.txt
public class BossInfoList
{
    public BossInfoList()
    {
        m_Data = new List<BossInfo>();
    }


    //  Properties
    public List<BossInfo> data { get { return m_Data; } }

    
    private List<BossInfo> m_Data;

    //  returns a sorted string list of boss names; used for dropdown.options parameter
    public List<string> GetBossNames()
    {
        List<string> temp = new List<string>();

        foreach(BossInfo info in data)
        {
            temp.Add(info.BossName);
        }

        temp.Sort();

        return temp;
    }


    //  Return BossInfo based on a matching string name value
    public BossInfo GetBossInfo(string _name)
    {
        foreach (BossInfo info in data)
        {
            if (info.BossName.CompareTo(_name) == 0)
            {
                return info;
            }
        }

        return null;
    }

    public override string ToString()
    {
        string value = "";

        for(int i = 0; i < data.Count; ++i)
        {
            value += data[i].BossName + " " + data[i].HasAccessToRareDropTable + "\n";
        }

        return value;
    }
}
