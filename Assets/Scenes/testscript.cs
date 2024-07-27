using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class testscript : MonoBehaviour
{
    private async void Awake()
    {
        var result = await MongoConnection.Instance.GetBossDrops("Zamorakian Undercity", true);

        var jsonResult = JArray.Parse(result);
        foreach(var jObj in jsonResult)
        {
            Debug.Log($"{jObj["itemName"]}");
        }
    }
}