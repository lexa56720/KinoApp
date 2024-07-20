
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace KinoApiWrapper.Api.RequestSender
{
    internal class Requester : IRequester, IDisposable
    {
        private readonly HttpClient client;
        private readonly string url;

        private bool isDisposed;
        public Requester(string apiKey, string url)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.url = url;
        }

        public void Dispose()
        {
            if (isDisposed)
                return;
            ((IDisposable)client).Dispose();
            isDisposed = true;
        }
        public async Task<string> Request(string apiUrl, Dictionary<string, string> args)
        {
            var url = BuildUri(args, apiUrl);
            return await SendRequest(url);
        }
        public async Task<string> Request(string apiUrl)
        {
            return await SendRequest(url + apiUrl);
        }

        public void UpdateApiKey(string apiKey)
        {
            if (isDisposed)
                return;

            client.DefaultRequestHeaders.Remove("X-API-KEY");
            client.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
        }

        private async Task<string> SendRequest(string request)
        {
            if (isDisposed)
                return string.Empty;

            var response = await client.GetAsync(request);

            if (response.IsSuccessStatusCode != true)
                return string.Empty;

            return await response.Content.ReadAsStringAsync();
        }

        private string BuildUri(Dictionary<string, string> args, string apiUrl)
        {
            var builder = new UriBuilder(url + apiUrl)
            {
                Port = -1,
            };
            var query = HttpUtility.ParseQueryString(builder.Query);
            foreach (var key in args.Keys)
            {
                query[key] = args[key];
            }
            builder.Query = query.ToString();
            return builder.ToString();
        }
    }
}
