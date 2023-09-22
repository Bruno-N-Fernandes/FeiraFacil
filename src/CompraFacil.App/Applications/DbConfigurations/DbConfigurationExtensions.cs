using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace CompraFacil.App.Applications.DbConfigurations
{
    public static class DbConfigurationExtensions
    {
        public static IConfigurationBuilder AddDbConfigurationSource<TDbConfigurationSource>(this IConfigurationBuilder builder, Func<IConfigurationBuilder, TDbConfigurationSource> factory)
            where TDbConfigurationSource : IDbConfigurationSource => builder?.Add(factory.Invoke(builder));

        public static IConfigurationBuilder AddDbConfigurationSource(this IConfigurationBuilder builder, IDbConfigurationSettings dbConfigurationSettings)
            => builder?.Add(dbConfigurationSettings.CreateConfigurationSource(builder));

        internal static IDbCommand CreateCommand(this IDbConnection dbConnection, string commandText)
        {
            var dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = commandText;
            return dbCommand;
        }
    }
}