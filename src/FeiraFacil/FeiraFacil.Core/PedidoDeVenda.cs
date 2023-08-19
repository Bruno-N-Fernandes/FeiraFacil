using System;
using System.Collections.Generic;
using System.Linq;

namespace FeiraFacil.Core
{
    public class PedidoDeVenda
    {
        public DateTime DataVenda { get; }
        public Cliente Cliente { get; }
        public List<ItemPedido> Items { get; }
        public bool EstaAberto { get; private set; }
        public decimal Valor => Items.Sum(x => x.Quantidade * x.Produto.Preco);

        public PedidoDeVenda(Cliente cliente)
        {
            DataVenda = DateTime.Now;
            Cliente = cliente;
            Items = new List<ItemPedido>();
            EstaAberto = true;
        }

        public PedidoDeVenda Adicionar(Produto produto, int quantidade)
        {
            if (produto.Estoque >= quantidade)
            {
                var item = new ItemPedido { Produto = produto, Quantidade = quantidade };
                item.BaixarEstoque();
                Items.Add(item);
            }
            else // TODO: Rever isso!!!
                Console.WriteLine($"Não temos tantos copos de limonada {produto.Nome}");

            return this;
        }

        public decimal Fechar()
        {
            EstaAberto = false;
            return Valor;
        }
    }
}