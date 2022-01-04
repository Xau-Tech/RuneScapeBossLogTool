using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;

public static class StatsLoader
{
    //  Properties & fields

    private static readonly string _BASEURL = "https://secure.runescape.com/m=hiscore/index_lite.ws?player=";

    //  Methods

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
            HttpResponseMessage response = await client.GetAsync(_BASEURL + username);

            //  If query isn't successful, tell the user and return the existing player class
            if (!response.IsSuccessStatusCode)
            {
                PopupManager.Instance.ShowNotification($"ERROR: could not find player - {username}'s stats!");
                return ApplicationController.Instance.CurrentSetup.Player;
            }

            stats = (await response.Content.ReadAsStringAsync()).Split('\n');
        }

        Player player = ApplicationController.Instance.CurrentSetup.Player;

        player.Username = username;

        //  Fill each stats with its proper level from the response
        sbyte prayerLevel = sbyte.Parse(stats[PrayerSkill.WebQueryLineNumber].Split(',')[1]);
        player.SetLevel(Enums.SkillName.Prayer, prayerLevel);
        sbyte smithingLevel = sbyte.Parse(stats[SmithingSkill.WebQueryLineNumber].Split(',')[1]);
        player.SetLevel(Enums.SkillName.Smithing, smithingLevel);

        return player;
    }
}
