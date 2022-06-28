using ApiTestCours.Models;
using ApiTestCours.Controllers;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System;
using System.Net.Http;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ApiTestCours.Clients
{
    public class DotaClient
    {
        private HttpClient _client;
        private static string _adress;
        public static string _TeamNameSubFile;
        public static string _Tournaments;
        public DotaClient()
        { 
            _adress = Const.Dotaadress;
            _TeamNameSubFile = Const.fileNameTeams;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_adress);
            _Tournaments = Const.tournaments;
        }
        public async Task<Heroes> GetHeroes(string hrname)
        {
            var response = await _client.GetAsync("api/heroStats");
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<List<Heroes>>(content);
            foreach (var item in result)
            {
                if (item.localized_name == hrname)
                {
                    return item;
                }

            }
            return null;
        }

        public async Task<string> GetCompare(string Twohrnames)
        {
            var response = await _client.GetAsync("api/heroStats");
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<List<Heroes>>(content);
            string[] HrName = Twohrnames.Split('&');
            if(HrName.Length == 2)
            {
                string Herolocalized_name1 = null;
                string Heroattack_type1 = null;
                float Herobase_health1 = 0;
                float Herobase_health_regen1 = 0;
                float Herobase_mana1 = 0;
                float Herobase_mana_regen1 = 0;
                float Herobase_armor1 = 0;
                float Herobase_attack_min1 = 0;
                float Herobase_attack_max1 = 0;

                string Herolocalized_name2 = null;
                string Heroattack_type2 = null;
                float Herobase_health2 = 0;
                float Herobase_health_regen2 = 0;
                float Herobase_mana2 = 0;
                float Herobase_mana_regen2 = 0;
                float Herobase_armor2 = 0;
                float Herobase_attack_min2 = 0;
                float Herobase_attack_max2 = 0;
                foreach (var hero in result)
                {
                    if (hero.localized_name == HrName[0])
                    {
                        Herolocalized_name1 = hero.localized_name;
                        Heroattack_type1 = hero.attack_type;
                        Herobase_health1 = hero.base_health;
                        Herobase_health_regen1 = hero.base_health_regen;
                        Herobase_mana1 = hero.base_mana;
                        Herobase_mana_regen1 = hero.base_mana_regen;
                        Herobase_armor1 = hero.base_armor;
                        Herobase_attack_min1 = hero.base_attack_min;
                        Herobase_attack_max1 = hero.base_attack_max;
                    }
                    else if (hero.localized_name == HrName[1])
                    {
                        Herolocalized_name2 = hero.localized_name;
                        Heroattack_type2 = hero.attack_type;
                        Herobase_health2 = hero.base_health;
                        Herobase_health_regen2 = hero.base_health_regen;
                        Herobase_mana2 = hero.base_mana;
                        Herobase_mana_regen2 = hero.base_mana_regen;
                        Herobase_armor2 = hero.base_armor;
                        Herobase_attack_min2 = hero.base_attack_min;
                        Herobase_attack_max2 = hero.base_attack_max;
                    }

                }
                string compare = $"Назва:\t\t{Herolocalized_name1}\t\t{Herolocalized_name2}\r\n" +
                                 $"Тип атаки:\t\t{Heroattack_type1}\t\t{Heroattack_type2}\r\n" +
                                 $"Колово Здоров'я:\t\t{Herobase_health1}\t\t{Herobase_health2}\r\n" +
                                 $"Реген Здоров'я:\t\t{Herobase_health_regen1}\t\t{Herobase_health_regen2}\r\n" +
                                 $"Колово Мани:\t\t{Herobase_mana1}\t\t{Herobase_mana2}\r\n" +
                                 $"Реген Мани:\t\t{Herobase_mana_regen1}\t\t{Herobase_mana_regen2}\r\n" +
                                 $"Армор:\t\t{Herobase_armor1}\t\t{Herobase_armor2}\r\n" +
                                 $"Мін дамаг:\t\t{Herobase_attack_min1}\t\t{Herobase_attack_min2}\r\n" +
                                 $"Макс дамаг:\t\t{Herobase_attack_max1}\t\t{Herobase_attack_max2}\r\n";
                return compare;
            }
            else
            {
                return null;
            }
            
        }

        public async Task<DotaTeams> GetTeam(string teamName)
        {
            var response = await _client.GetAsync($"api/teams");
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<List<DotaTeams>>(content);
            foreach(var team in result)
            {
                if (team.name == teamName)
                {
                    return team;
                }
            }
            return null;
        }
        public async Task<Leagues> GetLeague(string LeagueName)
        {
            var response = await _client.GetAsync($"api/leagues");
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<List<Leagues>>(content);
            foreach (var league in result)
            {
                if (league.name.StartsWith(LeagueName))
                {
                    return league;
                }
            }
            return null;
        }
        public async Task<string> GetTop(int topcount)
        {
            var response = await _client.GetAsync($"api/teams");
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<List<DotaTeams>>(content);
            int count = 1;
            int i = 0;
            string top = null;
            while (i < topcount)
            {
                top = $"{top}\r\n" +
                      $"{count}. {result[i].name}  rating: {result[i].rating}";
                count++;
                i++;
            }
            return top;
        }
        public async Task<List<LiveMatches>> GetTeammatchesLive()
        {
            
            var responseLiveMatch = await _client.GetAsync($"api/live");
            responseLiveMatch.EnsureSuccessStatusCode();
            var contentLiveMatch = responseLiveMatch.Content.ReadAsStringAsync().Result;
            var resultLiveMatch = JsonConvert.DeserializeObject<List<LiveMatches>>(contentLiveMatch);
            string TeamsSub = File.ReadAllText(_TeamNameSubFile);
            string[] Arrteams = TeamsSub.Split("&");
            List<string> TeamsList = new List<string>();
            List<LiveMatches> StringLiveMatches = new List<LiveMatches>();
            foreach (string teams in Arrteams)
            {
                if (teams != "")
                {
                    TeamsList.Add(teams);
                }
            }
            TeamsList.Remove("teams");
            foreach (string teams in TeamsList)
            {
                foreach (var Livematch in resultLiveMatch)
                {
                    if ((teams == Livematch.team_name_dire) || (teams == Livematch.team_name_radiant))
                    {

                        StringLiveMatches.Add(Livematch);                                          
                    }
                }
            }
            return StringLiveMatches;
        }
        public void Add(string TeamNameSub)
        {
            string TeamsJson = null;
            if (File.ReadAllText(_TeamNameSubFile) == null)
            {
                TeamsJson = $"{TeamNameSub}";
            }
            else
            {
                string JsonFile = File.ReadAllText(_TeamNameSubFile);
                TeamsJson = $"{JsonFile}&{TeamNameSub}";
            }
            string jsonString = TeamsJson;
            File.WriteAllText(_TeamNameSubFile, jsonString);

            Console.WriteLine(File.ReadAllText(_TeamNameSubFile));
        }
        public void Del(string TeamNameSub)
        {
            string TeamsSub = File.ReadAllText(_TeamNameSubFile);
            string[] Arrteams = TeamsSub.Split("&");
            List<string> TeamsList = new List<string>();
            string TeamNameSubList = null;
            string Delstr = null;
            foreach (string teams in Arrteams)
            {
                TeamsList.Add(teams);
            }
            foreach (string listteams in TeamsList)
            {
                if (listteams == TeamNameSub)
                {
                    Delstr = listteams;
                }
            }
            TeamsList.Remove(Delstr);
            foreach (string pstteam in TeamsList)
            {
                TeamNameSubList = TeamNameSubList + "&" + pstteam;
            }
            File.WriteAllText(_TeamNameSubFile, TeamNameSubList);
        }

        public async Task<string> GetListofTournaments()
        {
            var content = File.ReadAllText(_Tournaments);
            string list = null;

            var result = JsonConvert.DeserializeObject<List<DotaTournaments>>(content);
            foreach (var tournament in result)
            {
                list = $"{list}\r\n" +
                       $"Tournament : {tournament.tournament_name}\r\n" +
                       $"Twitch : {tournament.twitch_url}\r\n" +
                       $"Data : {tournament.start_data}\r\n";
            }
            return list;
        }
    }
}
