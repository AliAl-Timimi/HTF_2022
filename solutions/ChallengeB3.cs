using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Json;


namespace solutions
{
    public class ChallengeB3
    {

        private static readonly string start = "/api/path/2/hard/Start";
        private static readonly string sample = "/api/path/2/hard/Sample";
        private static readonly string puzzle = "/api/path/2/hard/Puzzle";

        private static readonly HttpInstance _httpInstance = new();

        public static async Task Run()
        {
            Console.WriteLine("Challenge B3");
            startCall();
            var getResponse = await _httpInstance.Client.GetFromJsonAsync<CaesarSearchRecord>(sample) ?? new CaesarSearchRecord(new List<string>(), new List<Tile>());
            var answer = solve(getResponse);
            Console.WriteLine(answer);
            // var postReponse = await _httpInstance.Client.PostAsJsonAsync<string>(sample, answer);
            // Console.WriteLine(await postReponse.Content.ReadAsStringAsync());
        }

        private static string solve(CaesarSearchRecord record)
        {
            var decipheredWords = new List<string>();
            var charsToRemove = new List<int>();

            Console.WriteLine(record.CipheredWords[0]);
            var caesarKey = 0;

            for (var i = 0; i < 26; i++)
            {
                var test = decipher(record.CipheredWords[0], i);
                var list = checkGrid(decipher(record.CipheredWords[0], i), record);
                if (list.Count > 0)
                {
                    Console.WriteLine(decipher(record.CipheredWords[0], i));
                    charsToRemove.AddRange(list);
                    caesarKey = i;
                    break;
                }
            }

            Console.WriteLine(decipher(record.CipheredWords[0], caesarKey));
            var currentWord = record.CipheredWords[0];


            Console.WriteLine(record.CipheredWords.Count);
            System.Console.WriteLine(record.Grid.Count);
            return "";
        }

        private static async void startCall()
        {
            var startResponse = await _httpInstance.Client.GetAsync(start);
        }

        private static List<int> checkGrid(string word, CaesarSearchRecord record)
        {
            var gridSize = Math.Sqrt(record.Grid.Count);
            var wordLength = word.Length;
            var list = new List<int>();
            var vertical = new List<string>();
            var horizontal = new List<string>();

            for (int i = 0; i < gridSize; i++)
            {
                var newWord = "";
                for (int j = 0; j < gridSize; j++)
                {
                    newWord += record.Grid.Where(t => t.X == j + 1 && t.Y == i + 1).FirstOrDefault()?.Content;
                }
                vertical.Add(newWord);
            }

            for (int i = 0; i < gridSize; i++)
            {
                var newWord = "";
                for (int j = 0; j < gridSize; j++)
                {
                    newWord += record.Grid.Where(t => t.X == i + 1 && t.Y == j + 1).FirstOrDefault()!.Content;
                }
                horizontal.Add(newWord);
            }
            var found = false;
            var verticalWay = false;
            var index = 0;
            var reverse = false;
            if (word == "encryptie")
            {
                Console.WriteLine("here");
            }
            foreach (var w in vertical)
            {
                if (w.Contains(word))
                {
                    found = true;
                    verticalWay = true;
                    index = vertical.IndexOf(w);
                    break;
                }
                else
                {
                    var chararray = word.ToCharArray();
                    chararray.Reverse();
                    var reversed = new String(chararray);
                    if (w.Contains(reversed))
                    {
                        found = true;
                        verticalWay = true;
                        index = vertical.IndexOf(w);
                        reverse = true;
                        word = reversed;
                        break;
                    }
                }
            }
            if (!verticalWay)
            {
                foreach (var w in horizontal)
                {
                    if (w.Contains(word))
                    {
                        found = true;
                        index = horizontal.IndexOf(w);
                        break;
                    }
                    else
                    {
                        var chararray = word.ToCharArray();
                        Array.Reverse(chararray);
                        var reversed = new String(chararray);
                        if (w.Contains(reversed))
                        {
                            found = true;
                            verticalWay = true;
                            index = vertical.IndexOf(w);
                            reverse = true;
                            word = reversed;
                            break;
                        }
                    }
                }
            }

            if (found)
            {
                if (verticalWay)
                {
                    var j = 0;
                    if (!reverse)
                    {
                        for (int i = 0; i < gridSize; i++)
                        {
                            if (record.Grid.Where(t => t.X == index + 1 && t.Y == i + 1).FirstOrDefault()!.Content[0] == word[j])
                            {
                                j++;
                                list.Add(record.Grid.Where(t => t.X == index + 1 && t.Y == i + 1).FirstOrDefault()!.Id);
                            }
                        }
                    }
                    else
                    {
                        for (int i = (int)gridSize; i > 0; i--)
                        {
                            if (record.Grid.Where(t => t.X == index + 1 && t.Y == i).FirstOrDefault()!.Content[0] == word[j])
                            {
                                j++;
                                list.Add(record.Grid.Where(t => t.X == index + 1 && t.Y == i).FirstOrDefault()!.Id);
                            }
                        }
                    }

                }
            }
            else
            {
                var j = 0;
                if (!reverse)
                {
                    for (int i = 0; i < gridSize; i++)
                    {
                        if (record.Grid.Where(t => t.X == i + 1 && t.Y == index + 1).FirstOrDefault()!.Content[0] == word[j])
                        {
                            j++;
                            list.Add(record.Grid.Where(t => t.X == i + 1 && t.Y == index + 1).FirstOrDefault()!.Id);
                        }
                    }
                }
                else
                {
                    for (int i = (int)gridSize; i > 0; i--)
                    {
                        if (record.Grid.Where(t => t.X == index + 1 && t.Y == i).FirstOrDefault()!.Content[0] == word[j])
                        {
                            j++;
                            list.Add(record.Grid.Where(t => t.X == index + 1 && t.Y == i).FirstOrDefault()!.Id);
                        }
                    }
                }
            }
            return list;

            /*
            for (int i = 1; i <= gridSize; i++)
            {
                for (int j = 1; j <= gridSize; j++)
                {
                    var found = true;
                    list.Clear();
                    if (record.Grid.Where(t => t.X == j && t.Y == i).FirstOrDefault()!.Content[0] == word[0])
                    {
                        if (j + wordLength <= gridSize)
                        {
                            for (int k = 0; k < wordLength; k++)
                            {
                                if (record.Grid.Where(t => t.X == j + k && t.Y == i).FirstOrDefault()?.Content[0] != word[k])
                                    found = false;
                                else
                                    list.Add(record.Grid.Where(t => t.X == j + k && t.Y == i).FirstOrDefault()!.Id);
                            }
                        }
                        else if (i + wordLength <= gridSize)
                        {
                            for (int k = 0; k < wordLength; k++)
                            {
                                if (record.Grid.Where(t => t.X == j && t.Y == i + k).FirstOrDefault()?.Content[0] != word[k])
                                    found = false;
                                else
                                    list.Add(record.Grid.Where(t => t.X == j && t.Y == i + k).FirstOrDefault()!.Id);
                            }

                        }
                        else
                            found = false;
                        if (found)
                            return list;
                    }
                }
                
            }
            */
        }
        private static string decipher(string word, int key)
        {
            string deciphered = "";
            foreach (char c in word)
            {
                int dc = c - key;
                if (dc < 97) dc += 26;
                else if (dc > 122) dc -= 26;
                deciphered += (char)dc;
            }
            return deciphered;
        }
    }

    public class CaesarSearchRecord
    {
        public List<string> CipheredWords { get; set; }
        public List<Tile> Grid { get; set; }

        public CaesarSearchRecord(List<string> cipheredWords, List<Tile> grid)
        {
            CipheredWords = cipheredWords;
            Grid = grid;
        }
    }

    public class Tile
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Content { get; set; }

        public Tile(int id, int x, int y, string content)
        {
            Id = id;
            X = x;
            Y = y;
            Content = content;
        }
    }
}