using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

class Program
{
    public static async Task Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoalsAsTeam1 = await GetTotalScoredGoalsAsync(teamName, year);
        int totalGoalsAsTeam2 = await GetTotalScoredGoalsAsync(teamName, year, "team2");

        int totalGoals = totalGoalsAsTeam1 + totalGoalsAsTeam2;

        Console.WriteLine($"Team {teamName} scored {totalGoals} goals in {year}");

        teamName = "Chelsea";
        year = 2014;

        totalGoalsAsTeam1 = await GetTotalScoredGoalsAsync(teamName, year);
        totalGoalsAsTeam2 = await GetTotalScoredGoalsAsync(teamName, year, "team2");

        totalGoals = totalGoalsAsTeam1 + totalGoalsAsTeam2;

        Console.WriteLine($"Team {teamName} scored {totalGoals} goals in {year}");

        // Output expected:
        // Team Paris Saint-Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static async Task<int> GetTotalScoredGoalsAsync(string team, int year, string teamFilter = "team1")
    {

        using (HttpClient client = new HttpClient())
        {
            int totalPages = 1;
            int currentPage = 1;

            int totalGoals = 0;

            while(currentPage <= totalPages)
            {
                string apiUrl = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&{teamFilter}={team}&page={currentPage}";

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();

                    FootballDataResult? result = null;
                    
                    try
                    {

                        result = JsonConvert.DeserializeObject<FootballDataResult>(jsonContent);

                    }
                    catch(Exception ex)
                    {

                        Console.WriteLine(ex.Message);

                    }

                    if (result != null && result.Data != null && result.Data.Length > 0)
                    {
                        
                        totalPages = result.Total_Pages;

                        foreach (FootballMatch match in result.Data)
                        {

                            if (match.Team1 != null && match.Team1.Equals(team))
                            {
                                totalGoals += match.Team1Goals;
                            }
                            else if(match.Team2 != null && match.Team2.Equals(team))
                            {
                                totalGoals += match.Team2Goals;
                            }

                        }

                    }

                }

                currentPage+=1;

            }

            return totalGoals;

        }
    }
}

class FootballDataResult
{
    public int Page { get; set; }
    public int Total { get; set; }
    public int Total_Pages { get; set; }
    public FootballMatch[]? Data { get; set; }
}

class FootballMatch
{
    public string? Team1 { get; set; }
    public string? Team2 { get; set; }
    public int Team1Goals { get; set; }
    public int Team2Goals { get; set; }
    public int Year { get; set; }
}
