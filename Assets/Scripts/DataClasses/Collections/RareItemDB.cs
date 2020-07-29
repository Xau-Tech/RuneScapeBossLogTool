using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

//  Database loaded via xml to keep track of RareItems for each boss
public static class RareItemDB
{
    private static readonly string filePath = Application.dataPath + "/Data/RareItemDB.xml";
    private static Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();

    public static void Load()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(filePath);

        foreach(XmlNode node in doc.DocumentElement)
        {
            string name = node.Attributes[0].InnerText;
            string[] items = node.FirstChild.InnerText.Split(';');
            
            List<string> rareItems = new List<string>(items);
            for(int i = 0; i < rareItems.Count; ++i)
            {
                rareItems[i] = rareItems[i].ToLower();
            }

            data.Add(name, rareItems);
        }

        Debug.Log(ToString());
    }

    public new static string ToString()
    {
        string returnString = "";

        foreach (KeyValuePair<string, List<string>> entry in data)
        {
            returnString += entry.Key;
            for(int i = 0; i < entry.Value.Count; ++i)
            {
                returnString += $"\n\t{entry.Value[i]}";
                if (i == (entry.Value.Count - 1))
                    returnString += "\n";
            }
        }

        return returnString;
    }

    //  Check if the passed item for the passed boss is considered a rare item
    public static bool IsRare(in string bossName, in string itemName)
    {
        //  Dictionary doesn't contain this boss
        if (!data.ContainsKey(bossName))
            throw new System.Exception($"Key - {bossName} - could not be found in the RareItemDB!");
        //  Passed boss exists in this dictionary
        else
        {
            List<string> rareItems;
            data.TryGetValue(bossName, out rareItems);

            //  Passed item is valid rare for passed boss
            if (rareItems.Contains(itemName.ToLower()))
                return true;
            else
            {
                BossInfo bossInfo;
                DataController.Instance.bossInfoDictionary.TryGetBossInfo(in bossName, out bossInfo);

                //  Check if passed boss has access to RDT which has a few rare items itself
                if (bossInfo.hasAccessToRareDropTable)
                {
                    //  Check RDT rares if so
                    if (data.TryGetValue("Rare Drop Table", out rareItems))
                    {
                        return rareItems.Contains(itemName.ToLower());
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
}
