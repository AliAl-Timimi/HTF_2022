using System.Net.Http.Json;

namespace solutions;

public class ChallengeA2
{
    private static readonly string start = "/api/path/1/medium/Start";
    private static readonly string sample = "/api/path/1/medium/Sample";
    private static readonly string puzzle = "/api/path/1/medium/Puzzle";

    private static readonly HttpInstance _httpInstance = new();

    public static async Task Run()
    {
        Console.WriteLine("Challenge A2");
        var response = await _httpInstance.Client.GetAsync(start);
        Console.WriteLine(response.StatusCode);
        var data = await _httpInstance.Client.GetFromJsonAsync<Dictionary<string, List<Dictionary<string, int>>>>(puzzle) ?? new Dictionary<string, List<Dictionary<string, int>>>();
        var answer = Solve(data);
        Console.WriteLine(answer);
        var samplePostResponse = await _httpInstance.Client.PostAsJsonAsync(puzzle, answer);
        var samplePostResponseValue = await samplePostResponse.Content.ReadAsStringAsync();
        Console.WriteLine(samplePostResponseValue);
    }
    
    private static string Solve(Dictionary<string, List<Dictionary<string, int>>> dict)
    {
        List<Wizard> teamA = new();
        List<Wizard> teamB = new();

        foreach (var item in dict)
        {
            foreach (var wizard in item.Value)
            {
                if (item.Key == "teamA")
                {
                    teamA.Add(new Wizard{
                        Strength = wizard["strength"],
                        Speed = wizard["speed"],
                        Health = wizard["health"]
                    });
                }
                else
                {
                    teamB.Add(new Wizard(){
                        Strength = wizard["strength"],
                        Speed = wizard["speed"],
                        Health = wizard["health"]
                    });
                }
            }
        }

        while (teamA.Count > 0 && teamB.Count > 0)
        {
            if (Fight(teamA[0], teamB[0]))
                teamB.RemoveAt(0);
            else
                teamA.RemoveAt(0);
        }

        return teamA.Count == 0 ? "TeamB" : "TeamA";
    }

    private static bool Fight(Wizard w1, Wizard w2)
    {
        var w1Alive = true;
        var w2Alive = true;
        var w1Turn = w1.Speed > w2.Speed;

        while (w1Alive && w2Alive)
        {
            if (w1Turn)
            {
                w2.Health -= w1.Strength;
                if (w2.Health <= 0)
                    w2Alive = false;
                w1Turn = false;
            }
            else
            {
                w1.Health -= w2.Strength;
                if (w1.Health <= 0)
                    w1Alive = false;
                w1Turn = true;
            }
        }

        return w1Alive;
    }
}