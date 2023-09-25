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
                    throw new HttpRequestException();
                }
            }
        }
        catch (Exception e)
        {
            PopupManager.Instance.ShowNotification($"Error {e.GetBaseException()}: Connection to the server could not be established!");
            ApplicationController.Instance.ForceExit();
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
                    throw new HttpRequestException();
                }
            }
        }
        catch (Exception e)
        {
            PopupManager.Instance.ShowNotification($"Error {e.GetBaseException()}: Connection to the server could not be refreshed!");
        }
    }

    public async Task<MongoResponse> GetVersionInfoAsync()
    {
        if (string.IsNullOrEmpty(m_BearerToken))
            await LoginAsync();

        //  get some user stuff for quick & dirty metrics & user count
        //  SystemInfo class - device unique id, os

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
                RequestUri = new Uri(MongoConnectionSettings.BASEURL + "/VersionInfo"),
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
                    return new MongoResponse
                    {
                        Success = true,
                        Data = body
                    };
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    //  refresh token
                    await RefreshAsync();
                    return await GetVersionInfoAsync();
                }
                else
                {
                    string message = response.StatusCode + ": " + body;
                    return new MongoResponse
                    {
                        Success = false,
                        Data = message
                    };
                }
            }
        }
        catch (Exception e)
        {
            PopupManager.Instance.ShowNotification($"Error {e.GetBaseException()}: {e.Message}");
            ApplicationController.Instance.ForceExit();
        }

        return new MongoResponse
        {
            Success = false,
            Data = ""
        };
    }

    public async Task<MongoResponse> GetBossDrops(string bossName, bool includeRareDropTable)
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
                    return new MongoResponse
                    {
                        Success = true,
                        Data = body
                    };
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await LoginAsync();
                    return await GetBossDrops(bossName, includeRareDropTable);
                }
                else
                {
                    string message = response.StatusCode + ": " + body;
                    return new MongoResponse
                    {
                        Success = false,
                        Data = message
                    };
                }
            }
        }
        catch (Exception e)
        {
            PopupManager.Instance.ShowNotification($"Error {e.GetBaseException()}: {e.Message}");
            return new MongoResponse
            {
                Success = false,
                Data = e.Message
            };
        }
    }

    public async Task<MongoResponse> GetSetupItems()
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
                    return new MongoResponse
                    {
                        Success = true,
                        Data = body
                    };
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await RefreshAsync();
                    return await GetSetupItems();
                }
                else
                {
                    string message = response.StatusCode + ": " + body;
                    return new MongoResponse
                    {
                        Success = false,
                        Data = message
                    };
                }
            }
        }
        catch (Exception e)
        {
            PopupManager.Instance.ShowNotification($"Error {e.GetBaseException()}: {e.Message}");
            ApplicationController.Instance.ForceExit();
        }

        return new MongoResponse
        {
            Success = false,
            Data = ""
        };
    }

    public struct MongoResponse
    {
        public bool Success;
        public string Data;
    }
}