using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

//  Load anything for setups either from save files or online or combining the two
public static class SetupLoader
{
    private static readonly string usernameLookupBaseURL = "https://secure.runescape.com/m=hiscore/index_lite.ws?player=";

    /*  
     *  Load a player's stats from the entered username
     *  Return for this query is structured as 0,1,2\n repeated for each skill in the game
     *  where 0=rank, 1=level, 2=exp
    */
    public async static Task<Player> LoadPlayerStatsAsync(string username)
    {
        string[] stats;

        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(usernameLookupBaseURL + username);

            //  If query isn't successful, tell the user and return the existing Player class
            if(!response.IsSuccessStatusCode)
            {
                InputWarningWindow.Instance.OpenWindow($"Error: could not find player - {username}'s stats!");
                return CacheManager.SetupTab.Setup.Player;
            }

            //  Split into lines
            stats = (await response.Content.ReadAsStringAsync()).Split('\n');
        }

        Player player = new Player(username);

        //  Fill each stat with its proper level from the HttpClient response
        player.PrayerLevel = sbyte.Parse(stats[PrayerSkill.WebQueryLineNumber].Split(',')[1]);
        player.SmithingLevel = sbyte.Parse(stats[SmithingSkill.WebQueryLineNumber].Split(',')[1]);

        return player;
    }
}
