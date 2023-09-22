using Microsoft.Extensions.Configuration;
using System.Data;

namespace CompraFacil.App.Applications.DbConfigurations
{
    public class DbConfigurationSource : AbstractDbConfigurationSource
    {
        private readonly IDbConfigurationSettings _dbConfigurationSettings;
        protected override string CommandSelectQuerySql => _dbConfigurationSettings.CommandSelectQuerySql;
        protected override string ConfigurationKeyColumn => _dbConfigurationSettings.ConfigurationKeyColumn;
        protected override string ConfigurationValueColumn => _dbConfigurationSettings.ConfigurationValueColumn;
        protected override IDbConnection CreateDbConnection(IConfiguration configuration) => _dbConfigurationSettings.DbConnectionFactory.Invoke(configuration);

        public DbConfigurationSource(IConfigurationBuilder configurationBuilder, IDbConfigurationSettings dbConfigurationSettings)
            : base(configurationBuilder) => _dbConfigurationSettings = dbConfigurationSettings;
    }
}