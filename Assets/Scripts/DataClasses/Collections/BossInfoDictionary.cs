using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleSheetsToUnity;

//  List of the boss info grabbed from /StreamingAssets/bossinfo.txt
public class BossInfoDictionary
{
    public BossInfoDictionary()
    {
        bossInfoDictionary = new Dictionary<string, BossInfo>();
    }

    private static Dictionary<string, BossInfo> bossInfoDictionary;
    private const byte COLUMNS = 4;

    //  Return the correct value for our boss info sheet in Google Docs
    public static string BossInfoFile(in string rsVersion)
    {
        if (rsVersion.ToLower().CompareTo("rs3") == 0)
            return "RS3BossInfo";
        else
            return "OSRSBossInfo";
    }

    public void Load(GstuSpreadSheet ss)
    {
        BossInfo bossInfo;
        int numRows = ss.Cells.Count / COLUMNS;  //   Divide by number of columns
        
        //  Create and add a boss for each row in the sheet
        for (int i = 2; i < (numRows + 1); ++i)
        {
            string name = ss["A" + i].value;
            bool hasRDT;
            bool isInstanceItem;
            uint baseInstanceCost;

            //  Make sure all values are properly formatted
            if (!bool.TryParse(ss["B" + i].value, out hasRDT))
                throw new System.Exception($"Value in B{i} cannot be parsed to a bool!");
            if (!bool.TryParse(ss["C" + i].value, out isInstanceItem))
                throw new System.Exception($"Value in C{i} cannot be parsed to a bool!");
            if (!uint.TryParse(ss["D" + i].value, out baseInstanceCost))
                throw new System.Exception($"Value in D{i} cannot be parsed to a uint!");

            //  Create and add boss
            bossInfo = new BossInfo(name, hasRDT, isInstanceItem, baseInstanceCost);
            Add(in bossInfo);
        }

        Print();

        CacheManager.currentBoss = GetBossNames()[0];
        EventManager.Instance.BossInfoLoaded();
    }

    //  Returns a sorted string list of boss names; used for dropdown.options parameter
    public List<string> GetBossNames()
    {
        List<string> temp = new List<string>();

        foreach(var name in bossInfoDictionary.Keys)
        {
            temp.Add(name);
        }

        temp.Sort();
        return temp;
    }

    //  Wrapper for Dictionary.Add
    public void Add(in BossInfo bossInfo)
    {
        bossInfoDictionary.Add(bossInfo.bossName, bossInfo);
    }

    //  Wrapper for Dictionary.TryGetValue
    public bool TryGetBossInfo(in string name, out BossInfo bossInfo)
    {
        bossInfo = null;
        if (bossInfoDictionary.TryGetValue(name, out bossInfo))
            return true;
        else
            return false;
    }

    //  Wrapper for Dictionary.Count
    public int Count()
    {
        return bossInfoDictionary.Count;
    }

    public void Print()
    {
        foreach(var info in bossInfoDictionary.Values)
        {
            Debug.Log(info.ToString());
        }
    }
}
