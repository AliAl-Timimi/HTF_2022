using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace solutions
{
    public class ChallengeA1
    {
        private static readonly string start = "/api/path/1/easy/Start";
        private static readonly string sample = "/api/path/1/easy/Sample";
        private static readonly string puzzle = "/api/path/1/easy/Puzzle";

        private static readonly HttpInstance _httpInstance = new();

        public static async Task Run()
        {
            Console.WriteLine("Challenge A1");
            await _httpInstance.Client.GetAsync(start);
            var data = await _httpInstance.Client.GetFromJsonAsync<List<string>>(puzzle) ?? new List<string>();
            var answer = Solve(data);
            Console.WriteLine(answer);
            var samplePostResponse = await _httpInstance.Client.PostAsJsonAsync(puzzle, answer);
            var samplePostResponseValue = await samplePostResponse.Content.ReadAsStringAsync();
            Console.WriteLine(samplePostResponseValue);
        }

        private static string Solve(List<string> list)
        {
            int sum = 0;
            string result = "";
            Dictionary<char, int> dict = new()
            {
                {'I', 1},
                {'V', 5},
                {'X', 10},
                {'L', 50},
                {'C', 100},
                {'D', 500},
                {'M', 1000}
            };

            Dictionary<string, int> dict2 = new()
            {
                {"I", 1},
                {"IV", 4},
                {"V", 5},
                {"IX", 9},
                {"X", 10},
                {"XL", 40},
                {"L", 50},
                {"XC", 90},
                {"C", 100},
                {"CD", 400},
                {"D", 500},
                {"CM", 900},
                {"M", 1000}
            };
            
            foreach (var s in list)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (i + 1 < s.Length && dict[s[i]] < dict[s[i + 1]])
                        sum -= dict[s[i]];
                    else
                        sum += dict[s[i]];
                }
            }
            
            foreach(var item in dict2.Reverse()) {
                if (sum <= 0) break;
                while (sum >= item.Value) {
                    result += item.Key;
                    sum -= item.Value;
                }
            }
            return result;
        }
    }
}