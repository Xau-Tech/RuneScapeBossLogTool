using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;

public class MongoConnection
{
    public static MongoConnection Instance
    {
        get
        {
            if (m_Instance == null)
                m_Instance = new MongoConnection();

            return m_Instance;
        }
    }

    private string m_BearerToken;
    private string m_RefreshToken;
    private static MongoConnection m_Instance;

    private MongoConnection()
    {
        
    }

    public async Task<string> GetVersionInfoAsync()
    {
        try
        {
            string queryString = $"&unityDeviceId={SystemInfo.deviceUniqueIdentifier}";

            var clientHandler = new HttpClientHandler
            {
                UseCookies = false
            };
            var client = new HttpClient(clientHandler);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(MongoConnectionSettings.VERSIONURL + queryString)
            };
            using (var response = await client.SendAsync(request))
            {
                var body = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return body;
                }
                else
                {
                    HandleError(body, false);
                    return null;
                }
            }
        }
        catch (Exception e)
        {
            HandleError(e.Message, false);
            return null;
        }
    }

    public async Task<string> GetBossDrops(string bossName, bool includeRareDropTable)
    {
        try
        {
            string queryString = $"&bossName={bossName}&rareDrops={includeRareDropTable}";

            var clientHandler = new HttpClientHandler
            {
                UseCookies = false
            };
            var client = new HttpClient(clientHandler);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(MongoConnectionSettings.BOSSDROPSURL + queryString)
            };
            using (var response = await client.SendAsync(request))
            {
                var body = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return body;
                }
                else
                {
                    HandleError("Boss drops could not be fetched!", false);
                    return null;
                }
            }
        }
        catch (Exception e)
        {
            HandleError("Boss drops could not be loaded", false);
            return null;
        }
    }

    public async Task<string> GetSetupItems()
    {
        try
        {
            var clientHandler = new HttpClientHandler
            {
                UseCookies = false
            };
            var client = new HttpClient(clientHandler);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(MongoConnectionSettings.SETUPITEMSURL),
            };
            using (var response = await client.SendAsync(request))
            {
                var body = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return body;
                }
                else
                {
                    HandleError("Setup Items could not be fetched!", true);
                    return null;
                }
            }
        }
        catch (Exception e)
        {
            HandleError(e.Message, true);
            return null;
        }
    }

    private void HandleError(string message, bool forceQuit)
    {
        PopupManager.Instance.ShowNotification($"ERROR: {message}");
        if (forceQuit) ApplicationController.Instance.ForceExit();
    }
}