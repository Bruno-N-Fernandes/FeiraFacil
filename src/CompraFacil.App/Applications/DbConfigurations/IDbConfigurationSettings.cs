using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace CompraFacil.App.Applications.DbConfigurations
{
    public interface IDbConfigurationSettings
    {
        string CommandSelectQuerySql { get; set; }
        string ConfigurationKeyColumn { get; set; }
        string ConfigurationValueColumn { get; set; }
        Func<IConfiguration, IDbConnection> DbConnectionFactory { get; set; }
        IConfigurationSource CreateConfigurationSource(IConfigurationBuilder configurationBuilder);
    }
}