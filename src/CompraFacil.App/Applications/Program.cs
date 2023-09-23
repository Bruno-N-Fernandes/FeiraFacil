using CompraFacil.App.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MPSTI.PlenoSoft.Core.DbConfigurations.Sql;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace CompraFacil.App.Applications
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            ApplicationConfiguration.Initialize();
            var serviceProvider = UseDependencyInjection(ConfigureServices);
            Application.Run(serviceProvider.GetRequiredService<FormRelatorioCompras>());
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.UseDbConnection("GwNet");
            services.AddTransient<IGiroCompraRepository, GiroCompraRepository>();
            services.AddTransient<FormRelatorioCompras>();
        }

        private static void UseDbConnection(this IServiceCollection services, string connectionName)
        {
            services.AddTransient<IDbConnection, DbConnection>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString(connectionName);
                var dbConnection = new SqlConnection(connectionString);
                dbConnection.Open();
                return dbConnection;
            });
        }

        private static IServiceProvider UseDependencyInjection(Action<IServiceCollection, IConfiguration> configureServices)
        {
            var services = new ServiceCollection();
            var configuration = UseConfiguration();
            services.AddSingleton<IConfiguration>(configuration);
            configureServices.Invoke(services, configuration);
            return services.BuildServiceProvider();
        }

        private static IConfigurationRoot UseConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            return configuration;
        }
    }
}