using Microsoft.Extensions.Configuration;

namespace CompraFacil.App.Applications.DbConfigurations
{
    public class DbConfigurationProvider : ConfigurationProvider
    {
        private readonly IDbConfigurationSource _dbConfigurationSource;

        public DbConfigurationProvider(IDbConfigurationSource dbConfigurationSource)
            => _dbConfigurationSource = dbConfigurationSource;

        public override void Load() => _dbConfigurationSource.ExecuteQueryAndLoadData(Data);
    }
}