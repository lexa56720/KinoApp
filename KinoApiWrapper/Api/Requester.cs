
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KinoApiWrapper.Api
{
    internal class Requester : IDisposable
    {
        private readonly HttpClient client;
        public Requester(string apiKey)
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://kinopoiskapiunofficial.tech"),
            };
            client.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void Dispose()
        {
            ((IDisposable)client).Dispose();
        }

        public async Task<string> Request(string apiUrl)
        {
            return await client.GetStringAsync(apiUrl);
        }
    }
}
