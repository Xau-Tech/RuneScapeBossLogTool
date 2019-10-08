using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  List of the boss info grabbed from /StreamingAssets/bossinfo.txt
public class BossInfoListClass
{
    public BossInfoListClass()
    {
        m_BossInfoList = new List<BossInfo>();
    }


    //  Properties
    public List<BossInfo> BossInfoList { get { return m_BossInfoList; } }

    
    private List<BossInfo> m_BossInfoList;


    //  returns a sorted string list of boss names; used for dropdown.options parameter
    public List<string> GetBossNames()
    {
        List<string> temp = new List<string>();

        foreach(BossInfo info in m_BossInfoList)
        {
            temp.Add(info.BossName);
        }

        temp.Sort();

        return temp;
    }


    //  Return BossInfo based on a matching string name value
    public BossInfo GetBossInfo(string _name)
    {
        foreach (BossInfo info in m_BossInfoList)
        {
            if (info.BossName.CompareTo(_name) == 0)
            {
                return info;
            }
        }

        return null;
    }
}
