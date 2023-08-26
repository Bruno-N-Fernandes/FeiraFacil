using System.Collections.Generic;
using System.Threading.Tasks;

namespace DontBase.Shareds.Repositories
{
    public interface IDontPadRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> ObterTodos();
        Task Incluir(TEntity entity);
        Task Excluir(TEntity entity);
        Task Alterar(TEntity entity);
    }
}