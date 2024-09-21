using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace steamAchievementTracker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnGetAchievements_Click(object sender, EventArgs e)
        {
            string apiKey = "api dev key here"; // api dev key
            string steamId = "steam id here"; // User's Steam ID
            string appId = "app id here"; // Your game’s app ID

            SteamApi steamApi = new SteamApi(apiKey, steamId);

            try
            {
                string achievementsJson = await steamApi.GetAchievementsAsync(appId);
                // Deserialize
                AchievementsResponse achievements = JsonConvert.DeserializeObject<AchievementsResponse>(achievementsJson);
                MessageBox.Show(achievementsJson);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
