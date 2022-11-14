using System.Net.Http.Headers;

namespace solutions
{
    internal class HttpInstance
    {
        private readonly string token = "your token here";

        public HttpClient Client { get; }

        public HttpInstance()
        {
            Client = new HttpClient
            {
                BaseAddress = new Uri("Link")
            };
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}