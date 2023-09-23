using Microsoft.Extensions.Configuration;
using MPSTI.PlenoSoft.Core.DbConfigurations.Sql;
using System.Data;
using System.Data.SqlClient;

namespace CompraFacil.App.Applications
{
    public class SqlConfigurationSource : DbConfigurationSource
    {
        protected override string CommandSelectQuerySql => "Select Chave, Valor From Configuracao";
        protected override string ConfigurationKeyColumn => "Chave";
        protected override string ConfigurationValueColumn => "Valor";
        protected override IDbConnection CreateDbConnection(IConfiguration configuration)
        {
            return new SqlConnection(configuration.GetConnectionString("GwNet"));
        }
    }
}