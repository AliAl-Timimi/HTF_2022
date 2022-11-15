using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Json;


namespace solutions
{
    public class ChallengeB1
    {
        private static readonly string start = "/api/path/2/easy/Start";
        private static readonly string sample = "/api/path/2/easy/Sample";
        private static readonly string puzzle = "/api/path/2/easy/Puzzle";

        private static readonly HttpInstance _httpInstance = new();

        public static async Task Run()
        {
            Console.WriteLine("Challenge B1");
            // startCall();
            var getResponse = await _httpInstance.Client.GetFromJsonAsync<List<string>>(puzzle) ?? new List<string>();

            var answer = solve(getResponse);
            Console.WriteLine(answer);
            var postReponse = await _httpInstance.Client.PostAsJsonAsync<string>(puzzle, answer);
            Console.WriteLine(await postReponse.Content.ReadAsStringAsync());
        }

        private static string solve(List<String> strings)
        {
            var letters = new Dictionary<char, int>();
            var sentence = "";
            foreach (var s in strings)
            {
                var word = "";
                letters.Clear();
                foreach (var c in s.ToCharArray())
                {
                    if (letters.ContainsKey(c))
                        letters[c] = ++letters[c];
                    else
                        letters.Add(c, 1);
                }
                var list = letters.ToList();
                list.Sort((p1, p2) => p1.Value.CompareTo(p2.Value));
                list.ForEach((i) => word += i.Key);
                sentence += word + " ";
            }
            return sentence.Trim();
        }

        private static async void startCall()
        {
            var startResponse = await _httpInstance.Client.GetAsync(start);
        }

    }
}