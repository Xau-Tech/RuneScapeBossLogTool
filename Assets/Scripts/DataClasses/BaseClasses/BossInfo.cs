using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  General boss info class for use in populating the boss list and the item drop list
public class BossInfo
{
    public BossInfo()
    {
    }
    public BossInfo(string name, bool flag)
    {
        m_BossName = name;
        m_HasAccessToRareDropTable = flag;
    }


    //  Properties
    public string BossName { get { return m_BossName; } }
    public bool HasAccessToRareDropTable { get { return m_HasAccessToRareDropTable; } }


    private string m_BossName;
    private bool m_HasAccessToRareDropTable;
}
