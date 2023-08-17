using System.Collections.Generic;

namespace FeiraFacil.Core
{
    public class Sistema
    {
        private readonly List<Produto> _produtos = new();
        private readonly List<Cliente> _clientes = new();
        private readonly List<PedidoDeVenda> _pedidos = new();

        public IEnumerable<Produto> Produtos => _produtos;
        public IEnumerable<Cliente> Clientes => _clientes;
        public IEnumerable<PedidoDeVenda> Pedidos => _pedidos;

        public void Seed()
        {
            _produtos.Add(new Produto { Codigo = 1, Nome = "Limonada Pequena 200 ml", Custo = 0.50M, Preco = 1.50m, Estoque = 30 });
            _produtos.Add(new Produto { Codigo = 2, Nome = "Limonada Grande  300 ml", Custo = 0.75M, Preco = 3.00m, Estoque = 30 });

            _clientes.Add(new Cliente { Codigo = 0, Nome = "Indefinido" });
            _clientes.Add(new Cliente { Codigo = 1, Nome = "Sidnei" });
            _clientes.Add(new Cliente { Codigo = 2, Nome = "Bruno" });
        }

        public void Atender(Cliente cliente, Produto produto, int quantidade)
        {
            var pedido = _pedidos.Find(x => x.Cliente.Codigo == cliente.Codigo && x.EstaAberto);
            if (pedido == null)
            {
                pedido = new PedidoDeVenda(cliente);
                _pedidos.Add(pedido);
            }
            pedido.Adicionar(produto, quantidade);
        }

        public void FecharConta(Cliente cliente)
        {
            var pedido = _pedidos.Find(x => x.Cliente.Codigo == cliente.Codigo && x.EstaAberto);
            pedido.Fechar();
        }
    }
}