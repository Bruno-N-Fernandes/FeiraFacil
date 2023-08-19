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

        public Sistema(bool seed = true)
        {
            if (seed)
                Seed();
        }

        public void Seed()
        {
            _produtos.Add(new Produto { Codigo = 1, Nome = "Limonada 200ml", Custo = 0.50M, Preco = 1.50m, Estoque = 30 });
            _produtos.Add(new Produto { Codigo = 2, Nome = "Limonada 300ml", Custo = 0.75M, Preco = 3.00m, Estoque = 3000 });

            _clientes.Add(new Cliente { Codigo = 0, Nome = "Indefinido" });
            _clientes.Add(new Cliente { Codigo = 1, Nome = "Sidnei" });
            _clientes.Add(new Cliente { Codigo = 2, Nome = "Bruno" });

            _pedidos.Add(new PedidoDeVenda(_clientes[0]).Adicionar(_produtos[0], 1).Adicionar(_produtos[1], 2));
            _pedidos.Add(new PedidoDeVenda(_clientes[1]).Adicionar(_produtos[0], 3).Adicionar(_produtos[1], 4));
            _pedidos.Add(new PedidoDeVenda(_clientes[2]).Adicionar(_produtos[0], 5).Adicionar(_produtos[1], 6));

            _pedidos.Add(new PedidoDeVenda(_clientes[0]).Adicionar(_produtos[0], 1).Adicionar(_produtos[1], 2));
            _pedidos.Add(new PedidoDeVenda(_clientes[1]).Adicionar(_produtos[0], 3).Adicionar(_produtos[1], 4));
            _pedidos.Add(new PedidoDeVenda(_clientes[2]).Adicionar(_produtos[0], 5).Adicionar(_produtos[1], 6));
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

        public decimal? FecharConta(Cliente cliente)
        {
            var pedido = _pedidos.Find(x => x.Cliente.Codigo == cliente.Codigo && x.EstaAberto);
            return pedido?.Fechar();
        }
    }
}