using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;

/// <summary>
/// Database for RareItem data
/// </summary>
public static class RareItemDB
{
    //  Properties & fields
    private static Dictionary<string, List<RareItemStruct>> _data = new Dictionary<string, List<RareItemStruct>>();

    private static readonly string FILEPATH = Application.streamingAssetsPath + "/Data/";

    //  Methods
    public static async Task<string> Load(string rsVersion)
    {
        _data = new Dictionary<string, List<RareItemStruct>>();
        string loadPath = FILEPATH + rsVersion + "RareItems.xml";

        XmlDocument doc = new XmlDocument();
        doc.Load(loadPath);

        foreach(XmlNode itemNode in doc.DocumentElement)
        {
            //  First child should be itemName
            XmlNode childNode = itemNode.FirstChild;
            string itemName = childNode.InnerText;
            if (string.IsNullOrEmpty(itemName = childNode.InnerText))
                throw new System.Exception($"ERROR: itemName value in RareItems.xml is empty!");

            //  Second child should be itemID
            childNode = childNode.NextSibling;
            int itemID;
            if (!int.TryParse(childNode.InnerText, out itemID))
                throw new System.Exception($"ERROR: could not parse itemID for item {itemName} in RareItems.xml!");

            //  Third child should be a ; separated list of bosses this item is dropped from
            childNode = childNode.NextSibling;
            string fromBossesLine;
            if (string.IsNullOrEmpty(fromBossesLine = childNode.InnerText))
                throw new System.Exception($"ERROR: must be some boss value listed for item {itemName} in RareItems.xml!");

            string[] fromBosses = fromBossesLine.Split(';');

            //  Construct a RareItemStruct to hold the data
            RareItemStruct ris = new RareItemStruct(itemName, itemID);

            //  Add this struct to each listed boss in the dictionary
            for (int i = 0; i < fromBosses.Length; ++i)
            {
                //  Move to next iteration if string is empty
                if (string.IsNullOrEmpty(fromBosses[i]))
                    continue;

                //  Boss hasn't been added as a key yet
                if (!_data.ContainsKey(fromBosses[i]))
                {
                    _data.Add(fromBosses[i], new List<RareItemStruct>() { ris });
                }
                //  Boss has been added as a key
                {
                    if (!_data.TryGetValue(fromBosses[i], out List<RareItemStruct> rareItemStructList))
                        throw new System.Exception($"Null reference exception!  Null List<RareItemStruct> for {fromBosses[i]} in RareItemDB.cs!");

                    rareItemStructList.Add(ris);
                }
            }
        }

        Debug.Log(ToString());
        return "RareItemDB load done";
    }

    /// <summary>
    /// Check if item is rare based on item id and boss name
    /// </summary>
    /// <returns></returns>
    public static bool IsRare(string bossName, int itemId)
    {
        //  Dictionary doesn't contain boss
        if (!_data.ContainsKey(bossName))
        {
            throw new System.Exception($"Key - {bossName} - could not be found in the RareItemDB!");
        }
        else
        {
            _data.TryGetValue(bossName, out List<RareItemStruct> rareItemStructs);

            //  Passed item is valid rare for passed boss
            if (rareItemStructs.Exists(rareItem => rareItem.ItemId == itemId))
            {
                return true;
            }
            else
            {
                BossInfo bossInfo;
                if ((bossInfo = ApplicationController.Instance.BossInfo.GetBoss(bossName)) == null)
                    return false;

                //  Check if passed boss has access to RDT to check rares there
                if (bossInfo.HasAccessToRareDropTable)
                {
                    if (_data.TryGetValue("Rare Drop Table", out rareItemStructs))
                        return rareItemStructs.Exists(rareItem => rareItem.ItemId == itemId);
                    else
                        throw new System.Exception($"Rare Drop Table key not found in RareItemDB.cs");
                }
                else
                {
                    return false;
                }
            }
        }
    }

    /// <summary>
    /// Gets name of a RareItem via item id and boss name
    /// </summary>
    /// <returns></returns>
    public static string GetRareItemName(string bossName, int itemId)
    {
        List<RareItemStruct> rareItemStructList;

        if(_data.TryGetValue(bossName, out rareItemStructList))
        {
            //  Check current boss' rare item list
            string itemName = rareItemStructList.Find(rareItem => rareItem.ItemId == itemId).Name;

            if (itemName != null)
                return itemName;

            //  Check rare drop table list
            _data.TryGetValue("Rare Drop Table", out rareItemStructList);
            itemName = rareItemStructList.Find(rareItem => rareItem.ItemId == itemId).Name;

            if (itemName != null)
                return itemName;
            else
                throw new System.Exception($"Itemname for {itemId} could not be found in RareItemDB.cs for boss {bossName}!");
        }
        else
        {
            throw new System.Exception($"Bosslist for {bossName} could not be found in RareItemDB.cs!");
        }
    }

    public new static string ToString()
    {
        string text = "RareItemDB:\n";

        foreach(KeyValuePair<string, List<RareItemStruct>> kvp in _data)
        {
            text += kvp.Key;
            for(int i = 0; i < kvp.Value.Count; ++i)
            {
                text += $"\n\t{kvp.Value[i].ToString()}";
                if (i == (kvp.Value.Count - 1))
                    text += "\n";
            }
        }

        return text;
    }
}
