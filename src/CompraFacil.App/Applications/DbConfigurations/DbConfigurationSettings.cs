using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace CompraFacil.App.Applications.DbConfigurations
{
    public class DbConfigurationSettings : IDbConfigurationSettings
    {
        public string CommandSelectQuerySql { get; set; }
        public string ConfigurationKeyColumn { get; set; }
        public string ConfigurationValueColumn { get; set; }
        public Func<IConfiguration, IDbConnection> DbConnectionFactory { get; set; }

        IConfigurationSource IDbConfigurationSettings.CreateConfigurationSource() => new DbConfigurationSourceProxy(this);
    }
}