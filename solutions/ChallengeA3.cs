using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.VisualBasic.CompilerServices;

namespace solutions
{

    public class ChallengeA3
    {
        private static readonly string start = "/api/path/1/hard/Start";
        private static readonly string sample = "/api/path/1/hard/Sample";
        private static readonly string puzzle = "/api/path/1/hard/Puzzle";
        private static List<MazeRecord> _mazeRecords = new();
        private static Random _random = new();

        private static readonly HttpInstance _httpInstance = new();

        public static async Task Run()
        {
            var correct = false;
            Console.WriteLine("Challenge A3");
            var response = await _httpInstance.Client.GetAsync(start);
            Console.WriteLine(response.StatusCode);
            var input = await _httpInstance.Client.GetFromJsonAsync<MazeRecord>(sample) ?? new MazeRecord();
            Console.WriteLine("First room: # of doors: " + input.Doors.Count + " roomNr: " + input.RoomNr);
            _mazeRecords.Add(input);
            var data = _mazeRecords[0];
            while (!correct)
            {
                var doorNr = Solve(data);
                Console.WriteLine("Guess: " + doorNr);
                var samplePostResponse = await _httpInstance.Client.PostAsJsonAsync(sample, doorNr);
                var responseRecord = await samplePostResponse.Content.ReadFromJsonAsync<MazeRecord>() ?? new MazeRecord();
                if (responseRecord.Finished)
                {
                    Console.WriteLine("Done!");
                    correct = true;
                }
                else
                {
                    if (responseRecord.RoomNr == 1)
                    {
                        Console.WriteLine("Wrong guess! Back to first room");
                        data.Doors.Remove(doorNr);
                        data = _mazeRecords[0];
                    }
                    else
                    {
                        Console.WriteLine("Correct guess! New room: " + responseRecord.RoomNr);
                        data.CorrectGuess = doorNr;
                        var matches = _mazeRecords.Where(m => m.RoomNr == responseRecord.RoomNr).ToList();
                        if (!matches.Any())
                        {
                            _mazeRecords.Add(responseRecord);
                            data = _mazeRecords.LastOrDefault();
                        }
                        else
                        {
                            data = matches[0];
                        }
                    }
                }
            }
        }

        private static int Solve(MazeRecord mazeRecord)
        {
            if (mazeRecord.CorrectGuess != -1)
                return mazeRecord.CorrectGuess;

            return mazeRecord.Doors[_random.Next(mazeRecord.Doors.Count)];
        }
    }

    public class MazeRecord
    {
        public List<int> Doors { get; set; }
        public int RoomNr { get; set; }
        public bool Finished { get; set; }

        public int CorrectGuess = -1;
    }
}