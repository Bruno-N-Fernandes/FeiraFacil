using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace CompraFacil.App.Applications.DbConfigurations
{
    public static class DbConfigurationExtensions
    {
        public static IConfigurationBuilder AddDbConfigurationSource<TDbConfigurationSource>(this IConfigurationBuilder builder)
            where TDbConfigurationSource : IDbConfigurationSource, new() => builder?.Add(new TDbConfigurationSource());

        public static IConfigurationBuilder AddDbConfigurationSource<TDbConfigurationSettings>(this IConfigurationBuilder builder, Action<TDbConfigurationSettings> build)
            where TDbConfigurationSettings : IDbConfigurationSettings, new()
        {
            var dbConfigurationSettings = new TDbConfigurationSettings();
            build.Invoke(dbConfigurationSettings);
            return builder?.Add(dbConfigurationSettings.CreateConfigurationSource());
        }


        public static IConfigurationBuilder AddDbConfigurationSource(this IConfigurationBuilder builder, IDbConfigurationSettings dbConfigurationSettings)
            => builder?.Add(dbConfigurationSettings.CreateConfigurationSource());

        internal static IDbCommand CreateCommand(this IDbConnection dbConnection, string commandText)
        {
            var dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = commandText;
            return dbCommand;
        }
    }
}