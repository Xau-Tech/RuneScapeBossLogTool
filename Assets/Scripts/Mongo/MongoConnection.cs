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

    private async Task LoginAsync()
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
                Method = HttpMethod.Post,
                RequestUri = new Uri(MongoConnectionSettings.LOGINURL)
            };
            using (var response = await client.SendAsync(request))
            {
                var body = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var jsonBody = JObject.Parse(body);
                    m_BearerToken = Convert.ToString(jsonBody["access_token"]);
                    m_RefreshToken = Convert.ToString(jsonBody["refresh_token"]);
                }
                else
                {
                    HandleError("Connection could not be established!", true);
                }
            }
        }
        catch (Exception e)
        {
            HandleError(e.Message, true);
        }
    }

    private async Task RefreshAsync()
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
                Method = HttpMethod.Post,
                RequestUri = new Uri(MongoConnectionSettings.REFRESHTOKENURL),
                Headers =
            {
                { "Authorization", "Bearer " + m_RefreshToken }
            }
            };
            using (var response = await client.SendAsync(request))
            {
                var body = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var jsonBody = JObject.Parse(body);
                    m_BearerToken = Convert.ToString(jsonBody["access_token"]);
                }
                else
                {
                    HandleError("Token could not be refreshed!", false);
                }
            }
        }
        catch (Exception e)
        {
            HandleError(e.Message, false);
        }
    }

    public async Task<string> GetVersionInfoAsync()
    {
        if (string.IsNullOrEmpty(m_BearerToken))
            await LoginAsync();

        try
        {
            var clientHandler = new HttpClientHandler
            {
                UseCookies = false
            };
            var client = new HttpClient(clientHandler);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(MongoConnectionSettings.BASEURL + "/VersionInfo"),
                Headers =
                {
                    { "Authorization", "Bearer " + m_BearerToken }
                },
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "unityDeviceId", SystemInfo.deviceUniqueIdentifier }
                })
            };
            using (var response = await client.SendAsync(request))
            {
                var body = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return body;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    //  refresh token
                    await RefreshAsync();
                    return await GetVersionInfoAsync();
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
        if (string.IsNullOrEmpty(m_BearerToken))
            await LoginAsync();

        try
        {
            string queryString = $"?bossName={bossName}&rareDrops={includeRareDropTable}";

            var clientHandler = new HttpClientHandler
            {
                UseCookies = false
            };
            var client = new HttpClient(clientHandler);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(MongoConnectionSettings.BASEURL + "/GetBossDrops" + queryString),
                Headers =
            {
                { "Authorization", "Bearer " + m_BearerToken }
            }
            };
            using (var response = await client.SendAsync(request))
            {
                var body = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return body;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await LoginAsync();
                    return await GetBossDrops(bossName, includeRareDropTable);
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
            HandleError(e.Message, false);
            return null;
        }
    }

    public async Task<string> GetSetupItems()
    {
        if (string.IsNullOrEmpty(m_BearerToken))
            await LoginAsync();

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
                RequestUri = new Uri(MongoConnectionSettings.BASEURL + "/GetSetupItems"),
                Headers =
            {
                { "Authorization", "Bearer " + m_BearerToken }
            }
            };
            using (var response = await client.SendAsync(request))
            {
                var body = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return body;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await RefreshAsync();
                    return await GetSetupItems();
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