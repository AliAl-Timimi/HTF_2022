using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Json;


namespace solutions
{
    public class ChallengeB2
    {
        private static readonly string start = "/api/path/2/medium/Start";
        private static readonly string sample = "/api/path/2/medium/Sample";
        private static readonly string puzzle = "/api/path/2/medium/Puzzle";

        private static readonly HttpInstance _httpInstance = new();

        public static async Task Run()
        {
            Console.WriteLine("Challenge B2");
            // startCall();
            var getResponse = await _httpInstance.Client.GetFromJsonAsync<List<string>>(puzzle) ?? new List<string>();
            var answer = solve(getResponse);
            Console.WriteLine(answer);
            var postReponse = await _httpInstance.Client.PostAsJsonAsync<string>(puzzle, answer);
            Console.WriteLine(await postReponse.Content.ReadAsStringAsync());
        }

        private static string solve(List<String> strings)
        {
            var word = "";
            for (int i = 0; i < strings.OrderByDescending(s => s.Length).FirstOrDefault()?.Length; i++)
            {
                char c = strings[0][i];
                bool add = true;
                foreach (var s in strings)
                {
                    if (s.Length > i)
                    {
                        if (s[i] != c) add = false;
                    }
                    else add = false;
                }
                if (add && c != ' ') word += c;
            }
            return word;
        }

        private static async void startCall()
        {
            var startResponse = await _httpInstance.Client.GetAsync(start);
        }
    }
}