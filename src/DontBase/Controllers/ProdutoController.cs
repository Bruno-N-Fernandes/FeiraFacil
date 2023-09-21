using DontBase.Entities;
using DontBase.Repositories;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace DontBase.Controllers
{
    public class ProdutoController : WebApiController
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IServiceProvider serviceProvider): base(serviceProvider)
        {
            _produtoRepository = GetService<IProdutoRepository>();
        }

        [Function("Get")]
        public async Task<HttpResponseData> Get([HttpTrigger(Default, "get")] HttpRequestData req)
        {
            var produtos = await _produtoRepository.ObterTodos();

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(produtos);
            return response;
        }

        [Function("Post")]
        public async Task<HttpResponseData> Post([HttpTrigger(Default, "get")] HttpRequestData req)
        {
            var produto = new Produto { Codigo = "abc123", Nome = "Limonada" };
            await _produtoRepository.Incluir(produto);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(produto);
            return response;
        }

        [Function("Put")]
        public async Task<HttpResponseData> Put([HttpTrigger(Default, "put")] HttpRequestData req)
        {
            var produto = new Produto { Id = 1, Codigo = "abc123", Nome = "Limonada" };
            await _produtoRepository.Alterar(produto);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(produto);
            return response;
        }

        [Function("Delete")]
        public async Task<HttpResponseData> Delete([HttpTrigger(Default, "delete")] HttpRequestData req)
        {
            var produto = new Produto { Id = 1, Codigo = "abc123", Nome = "Limonada" };
            await _produtoRepository.Excluir(produto);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(produto);
            return response;
        }
    }
}