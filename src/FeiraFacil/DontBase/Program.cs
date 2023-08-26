using DontBase.Repositories;
using DontBase.Shareds.Apis;
using DontBase.Shareds.Sequences;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestSharp;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DontBase
{
    /// <summary>
    /// https://groups.google.com/g/startup-brasil/c/N0EZvdxIa8k
    /// </summary>
    public static class Program
    {
        public static void Main(string[] args)
        {
            var hostBuilder = new HostBuilder();
            hostBuilder.ConfigureFunctionsWorkerDefaults();

            hostBuilder.ConfigureServices(ConfigureServices);

            var host = hostBuilder.Build();

            host.Run();
        }

        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddHttpClient("DontPadApi", httpClient =>
            {
                var baseAddressApi = context.Configuration["DontPadApi.BaseAddress"];
                var applicationKey = context.Configuration["DontPadApi.ApplicationKey"];
                httpClient.BaseAddress = new Uri(Path.Combine(baseAddressApi, applicationKey));
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.Timeout = TimeSpan.FromMinutes(1);
            });

            services.AddSingleton<IDontPadApi>(serviceProvider =>
            {
                var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient("DontPadApi");
                var restClient = new RestClient(httpClient);
                return new DontPadApi(restClient);
            });

            services.AddScoped<ISequenceFactory, SequenceFactory>();

            services.AddSingleton<IProdutoRepository, ProdutoRepository>();
        }
    }
}