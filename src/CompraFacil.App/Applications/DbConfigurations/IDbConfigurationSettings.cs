using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace CompraFacil.App.Applications.DbConfigurations
{
    public interface IDbConfigurationSettings
    {
        string CommandSelectQuerySql { get; }
        string ConfigurationKeyColumn { get; }
        string ConfigurationValueColumn { get; }
        Func<IConfiguration, IDbConnection> DbConnectionFactory { get; }
        IConfigurationSource CreateConfigurationSource();
    }
}