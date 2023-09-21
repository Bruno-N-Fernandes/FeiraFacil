using DontBase.Entities;
using DontBase.Shareds.Repositories;
using System;

namespace DontBase.Repositories
{
    public interface IProdutoRepository : IDontPadRepository<Produto> { }

    public class ProdutoRepository : DontPadRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(IServiceProvider serviceProvider) : base(serviceProvider) { }
    }
}