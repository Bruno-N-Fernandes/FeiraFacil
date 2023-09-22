﻿using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace CompraFacil.App.Applications.DbConfigurations
{
    public static class DbConfigurationExtensions
    {
        /// <summary>
        /// builder.AddDbConfigurationSource<MyDbConfigurationSource>()
        /// </summary>
        /// <typeparam name="TDbConfigurationSource"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IConfigurationBuilder AddDbConfigurationSource<TDbConfigurationSource>(this IConfigurationBuilder builder) where TDbConfigurationSource : IDbConfigurationSource, new()
        {
            var configurationSource = new TDbConfigurationSource();
            return builder?.Add(configurationSource);
        }


        /// <summary>
        /// builder.AddDbConfigurationSource<DbConfigurationSettings>(x =>
        /// {
        ///     x.CommandSelectQuerySql = "Select * From Configuration";
        ///     x.ConfigurationKeyColumn = "Key";
        ///     x.ConfigurationValueColumn = "Value";
        ///     x.DbConnectionFactory = c => new SqlConnection(c.GetConnectionString("ConnectionName"));
        /// })
        /// </summary>
        /// <typeparam name="TDbConfigurationSettings"></typeparam>
        /// <param name="builder"></param>
        /// <param name="setup"></param>
        /// <returns></returns>
        public static IConfigurationBuilder AddDbConfigurationSource<TDbConfigurationSettings>(
            this IConfigurationBuilder builder, Action<TDbConfigurationSettings> setup)
            where TDbConfigurationSettings : IDbConfigurationSettings, new()
        {
            var dbConfigurationSettings = new TDbConfigurationSettings();
            setup?.Invoke(dbConfigurationSettings);
            var configurationSource = dbConfigurationSettings.CreateConfigurationSource();
            return builder?.Add(configurationSource);
        }


        /// <summary>
        /// builder.AddDbConfigurationSource(new DbConfigurationSettings
        /// {
        ///     CommandSelectQuerySql = "Select * From Configuration",
        ///     ConfigurationKeyColumn = "Key",
        ///     ConfigurationValueColumn = "Value",
        ///     DbConnectionFactory = c => new SqlConnection(c.GetConnectionString("ConnectionName"))
        /// })
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="dbConfigurationSettings"></param>
        /// <returns></returns>
        public static IConfigurationBuilder AddDbConfigurationSource(
            this IConfigurationBuilder builder, IDbConfigurationSettings dbConfigurationSettings)
        {
            var configurationSource = dbConfigurationSettings?.CreateConfigurationSource();
            return builder?.Add(configurationSource);
        }


        /// <summary>
        /// CreateCommand With CommandText
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        internal static IDbCommand CreateCommand(this IDbConnection dbConnection, string commandText)
        {
            var dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = commandText;
            return dbCommand;
        }
    }
}