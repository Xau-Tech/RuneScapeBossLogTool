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

    public List<BossInfo> BossInfoList { get { return m_BossInfoList; } }


    private List<BossInfo> m_BossInfoList;


    //  returns a sorted string list of boss names; used for dropdown.options parameter
    public List<string> GetBossNames()
    {
        List<string> temp = new List<string>();

        for (int i = 0; i < m_BossInfoList.Count; ++i)
        {
            temp.Add(m_BossInfoList[i].BossName);
        }

        temp.Sort();

        return temp;
    }


    //  Return BossInfo based on a matching string name value
    public BossInfo GetBossInfo(string _name)
    {
        for (int i = 0; i < m_BossInfoList.Count; ++i)
        {
            if (m_BossInfoList[i].BossName.CompareTo(_name) == 0)
            {
                return m_BossInfoList[i];
            }
        }

        return null;
    }
}
