using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace CompraFacil.App.Applications.DbConfigurations
{
    public interface IDbConfigurationSource : IConfigurationSource
    {
        void ExecuteQueryAndLoadData(IDictionary<string, string> dataSource);
    }
}