using System.Net.Http.Json;
using solutions;

namespace HTF_2022
{ 
    class Program
    {
        
        private static readonly HttpInstance _httpInstance = new();
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            //await ChallengeA1.Run();
            //await ChallengeA2.Run();
            //await ChallengeB1.Run();
            //await ChallengeB2.Run();
            await ChallengeA3.Run();
        }
    }
}