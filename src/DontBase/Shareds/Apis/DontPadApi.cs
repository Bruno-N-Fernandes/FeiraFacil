using RestSharp;
using System;
using System.Threading.Tasks;

namespace DontBase.Shareds.Apis
{
    /// <summary>
    /// https://groups.google.com/g/startup-brasil/c/N0EZvdxIa8k
    /// </summary>
    public class DontPadApi : IDontPadApi
    {
        private readonly IRestClient _restClient;

        public DontPadApi(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<string> Get(string resource, long lastModified = 0)
        {
            var restRequest = new RestRequest($"{resource}.body.json?lastModified={lastModified}");

            var restResponse = await _restClient.ExecuteAsync<DontPadResponse>(restRequest);

            return restResponse?.Data?.body;
        }

        public async Task<long> Post(string resource, string text)
        {
            var restRequest = new RestRequest(resource, Method.Post);
            restRequest.AddHeader("content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
            restRequest.AddParameter("text", text);
            restRequest.AddParameter("captcha-token-v2", "");
            restRequest.AddParameter("lastModified", DateTime.UtcNow.Ticks);
            restRequest.AddParameter("force", true);

            var restResponse = await _restClient.ExecuteAsync<long>(restRequest);

            return restResponse.Data;
        }
    }
}