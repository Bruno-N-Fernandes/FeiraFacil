using CompraFacil.App.Dtos;
using CompraFacil.App.Queries;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CompraFacil.App.Repositories
{
    public class GiroCompraRepository : IGiroCompraRepository
    {
        private readonly IDbConnection _dbConnection;

        public GiroCompraRepository(IServiceProvider serviceProvider)
        {
            _dbConnection = serviceProvider.GetRequiredService<IDbConnection>();
        }

        public async Task<IEnumerable<GiroCompra>> ObterListaCompras()
        {
            return await _dbConnection.QueryAsync<GiroCompra>(sql: QueryCompras.SqlGiroCompras);
        }
    }
}