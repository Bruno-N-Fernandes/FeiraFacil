using CompraFacil.App.Applications.DbConfigurations;
using CompraFacil.App.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddDbConfigurationSource<MyDbConfigurationSource>()
                .AddDbConfigurationSource<DbConfigurationSettings>(x =>
                {
                    x.CommandSelectQuerySql = "Select * From Configuracao";
                    x.ConfigurationKeyColumn = "Chave";
                    x.ConfigurationValueColumn = "Valor";
                    x.DbConnectionFactory = c => new SqlConnection(c.GetConnectionString("GwNet"));
                })
                .AddDbConfigurationSource(new DbConfigurationSettings
                {
                    CommandSelectQuerySql = "Select * From Configuracao",
                    ConfigurationKeyColumn = "Chave",
                    ConfigurationValueColumn = "Valor",
                    DbConnectionFactory = c => new SqlConnection(c.GetConnectionString("GwNet"))
                });

            var configuration = configurationBuilder.Build();
            return configuration;
        }
    }
}