using System.Net.Http.Headers;

namespace solutions
{
    internal class HttpInstance
    {
        private readonly string token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiMyIsIm5iZiI6MTY2ODUwNzQ3MywiZXhwIjoxNjY4NTkzODczLCJpYXQiOjE2Njg1MDc0NzN9.afRgds29obwpLPJNGDkb_fkg3rbbzGhHQE8PqoTRCJmRB_FzB-JkkxLMnZdRhmQgI-0cK7q-cPkQJJRGXJUSuQ";

        public HttpClient Client { get; }

        public HttpInstance()
        {
            Client = new HttpClient
            {
                BaseAddress = new Uri("https://app-htf-2022.azurewebsites.net/")
            };
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}