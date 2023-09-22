using CompraFacil.App.Applications.DbConfigurations;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CompraFacil.App.Applications
{
    public class MyDbConfigurationSource : DbConfigurationSource
    {
        protected override string CommandSelectQuerySql => "Select * From Configuracao";
        protected override string ConfigurationKeyColumn => "Chave";
        protected override string ConfigurationValueColumn => "Valor";
        protected override IDbConnection CreateDbConnection(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("GwNet");
            return new SqlConnection(connectionString);
        }
    }
}