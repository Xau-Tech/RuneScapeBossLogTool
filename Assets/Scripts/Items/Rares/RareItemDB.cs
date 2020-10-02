using System.Collections.Generic;
using UnityEngine;
using System.Xml;

//  Database loaded via xml to keep track of RareItems for each boss
public static class RareItemDB
{
    //private static readonly string filePath = Application.streamingAssetsPath + "/Data/RareItems.xml";
    private static readonly string filePath = Application.streamingAssetsPath + "/Data/";
    private static Dictionary<string, List<RareItemStruct>> data = new Dictionary<string, List<RareItemStruct>>();

    public static void Load(RSVersionOption rsVersionOption)
    {
        data = new Dictionary<string, List<RareItemStruct>>();

        string loadPath = filePath + rsVersionOption.GetValue() + "RareItems.xml";
        Debug.Log(loadPath);

        XmlDocument doc = new XmlDocument();
        doc.Load(loadPath);

        //  For each item
        foreach(XmlNode itemNode in doc.DocumentElement)
        {
            //  First child should be itemName
            XmlNode childNode = itemNode.FirstChild;
            string itemName = childNode.InnerText;
            if (string.IsNullOrEmpty(itemName = childNode.InnerText))
                throw new System.Exception($"Error: itemName value in RareItems.xml is empty!");

            //  Second child should be itemID
            childNode = childNode.NextSibling;
            int itemID;
            if (!int.TryParse(childNode.InnerText, out itemID))
                throw new System.Exception($"Error: could not parse itemID for item {itemName} in RareItems.xml!");

            //  Third child should be a ; separated list of bosses this item is dropped from
            childNode = childNode.NextSibling;
            string fromBossesLine;
            if (string.IsNullOrEmpty(fromBossesLine = childNode.InnerText))
                throw new System.Exception($"Error: must be some boss value listed for item {itemName} in RareItems.xml!");

            string[] fromBosses = fromBossesLine.Split(';');

            //  Construct a RareItemStruct to hold the data
            RareItemStruct rareItemStruct = new RareItemStruct(itemID, itemName);

            //  Add this struct to each listed boss in the dictionary
            for (int i = 0; i < fromBosses.Length; ++i)
            {
                //  Move to next iteration if string is empty
                if (string.IsNullOrEmpty(fromBosses[i]))
                    continue;

                //  Boss hasn't been added as a key yet
                if (!data.ContainsKey(fromBosses[i]))
                {
                    data.Add(fromBosses[i], new List<RareItemStruct>() { rareItemStruct });
                }
                //  Boss has been added as a key
                else
                {
                    List<RareItemStruct> rareItemStructList;
                    if (!data.TryGetValue(fromBosses[i], out rareItemStructList))
                        throw new System.Exception($"Null reference exception! Null List<RareItemStruct> for {fromBosses[i]}! in RareItemDB.cs!");
                    
                    rareItemStructList.Add(rareItemStruct);
                }
            }
        }

        Debug.Log(ToString());
    }

    //  Print the entirety of the dictionary
    public new static string ToString()
    {
        string returnString = "";

        foreach (KeyValuePair<string, List<RareItemStruct>> entry in data)
        {
            returnString += entry.Key;
            for(int i = 0; i < entry.Value.Count; ++i)
            {
                returnString += $"\n\t{entry.Value[i].ToString()}";
                if (i == (entry.Value.Count - 1))
                    returnString += "\n";
            }
        }

        return returnString;
    }

    //  Check if the passed item for the passed boss is considered a rare item
    public static bool IsRare(in string bossName, int itemID)
    {
        //  Dictionary doesn't contain this boss
        if (!data.ContainsKey(bossName))
            throw new System.Exception($"Key - {bossName} - could not be found in the RareItemDB!");
        //  Passed boss exists in this dictionary
        else
        {
            List<RareItemStruct> rareItemStructs;
            data.TryGetValue(bossName, out rareItemStructs);

            //  Passed item is valid rare for passed boss
            if (rareItemStructs.Exists(rareItem => rareItem.itemID == itemID))
                return true;
            else
            {
                BossInfo bossInfo;
                if ((bossInfo = DataController.Instance.bossInfoDictionary.GetBossByName(in bossName)) == null)
                    return false;

                //  Check if passed boss has access to RDT which has a few rare items itself
                if (bossInfo.hasAccessToRareDropTable)
                {
                    //  Check RDT rares if so
                    if (data.TryGetValue("Rare Drop Table", out rareItemStructs))
                    {
                        return rareItemStructs.Exists(rareItem => rareItem.itemID == itemID);
                    }
                    else
                        throw new System.Exception($"Rare Drop Table key not found in RareItemDB.cs");
                }
                //  If not return false
                else
                    return false;
            }
        }
    }

    //  Return the name of the RareItem via its itemID
    public static string GetRareItemName(in string bossName, int itemID)
    {
        List<RareItemStruct> rareItemStructList;

        if (data.TryGetValue(bossName, out rareItemStructList))
            return rareItemStructList.Find(rareItem => rareItem.itemID == itemID).name;
        else
            throw new System.Exception($"RareItemList for {bossName} could not be found in RareItemDB.cs!");
    }
}
