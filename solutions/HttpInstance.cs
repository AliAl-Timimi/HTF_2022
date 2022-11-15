using System.Net.Http.Headers;
using System.Net.Http;
using System;
namespace solutions
{
    internal class HttpInstance
    {
        private readonly string token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiMyIsIm5iZiI6MTY2ODUwMjkxMiwiZXhwIjoxNjY4NTg5MzEyLCJpYXQiOjE2Njg1MDI5MTJ9.HfNBlBBWWrJ-_x-VCveEdL9sMPLsOreLqJ9mAGt6KJpPs1e-dEqyNPEfAeO0G5c7i5_1v4sFIeYUcuT2YucU-Q";

        public HttpClient Client { get; }

        public HttpInstance()
        {
            Client = new HttpClient
            {
                BaseAddress = new Uri("https://app-htf-2022.azurewebsites.net")
            };
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}