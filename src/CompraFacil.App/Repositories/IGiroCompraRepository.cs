using CompraFacil.App.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompraFacil.App.Repositories
{
    public interface IGiroCompraRepository
    {
        Task<IEnumerable<GiroCompra>> ObterListaCompras();
    }
}