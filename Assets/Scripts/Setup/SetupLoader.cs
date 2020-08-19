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
     *  Return for this query is structured as X,Y,Z\n repeated for each skill in the game
     *  where X=rank, Y=level, Z=exp
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
                InputWarningWindow.Instance.OpenWindow($"Error: could not find player - {username} - stats!");
                return CacheManager.SetupTab.Setup.Player;
            }

            //  Split into lines
            stats = (await response.Content.ReadAsStringAsync()).Split('\n');
        }

        Player player = new Player(username);

        //  Look up and set only the skills listed in the SkillLoaderArray
        for(int i = 0; i < SkillLoaderArray.Skills.Count; ++i)
        {
            sbyte skillLevel = sbyte.Parse(stats[SkillLoaderArray.Skills[i].lineNumber].Split(',')[1]);
            player.GetSkill((SkillLoaderArray.SkillNames)i).level = skillLevel;
        }

        return player;
    }
}
