using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace steamAchievementTracker
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
    // achievement class
    public class Achievement
    {
        public string Name { get; set; }
        public bool Achieved { get; set; }
    }
    // gets/sets for achievements in api response
    public class AchievementsResponse
    {
        public List<Achievement> Achievements { get; set; }
    }
    // class for steamAPI
    public class SteamApi
    {
        private readonly string apiKey;
        private readonly string steamId;

        public SteamApi(string apiKey, string steamId)
        {
            this.apiKey = apiKey;
            this.steamId = steamId;
        }
        // takes variable values for appID, steamID, and apiKey and calls API address - waits for reponse of user/achievement info
        public async Task<string> GetAchievementsAsync(string appId)
        {
            using (var client = new HttpClient())
            {
                //string url = $"http://api.steampowered.com/ISteamUserStats/GetPlayerAchievements/v0001/?appid={appId}&steamid={steamId}&key={apiKey}"; //use this for player info
                string url = $"https://api.steampowered.com/ISteamUserStats/GetSchemaForGame/v2/?key={apiKey}&appid={appId}"; //use this from game info
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
        //method to parse data from string and send back
        public (string, string) achievementParse(string achievementsStringParse)
        {
            string achievementParseFirst, achievementParseLast;
            string[] splitData = achievementsStringParse.Split('[');
            achievementParseFirst = splitData[0];
            achievementParseLast = splitData[1];


            return (achievementParseFirst, achievementParseLast);
        }
    }

}
