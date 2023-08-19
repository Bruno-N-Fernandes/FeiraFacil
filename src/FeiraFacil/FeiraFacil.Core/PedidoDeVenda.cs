using System;
using System.Collections.Generic;
using System.Linq;

namespace FeiraFacil.Core
{
    public class PedidoDeVenda
    {
        public DateTime DataVenda { get; set; }
        public Cliente Cliente { get; set; }
        public List<ItemPedido> Items { get; set; }
        public bool EstaAberto { get; private set; }

        public PedidoDeVenda(Cliente cliente)
        {
            DataVenda = DateTime.Now;
            Cliente = cliente;
            Items = new List<ItemPedido>();
            EstaAberto = true;
        }

        public void Adicionar(Produto produto, int quantidade)
        {
            if (produto.Estoque >= quantidade)
            {
                var item = new ItemPedido { Produto = produto, Quantidade = quantidade };
                item.BaixarEstoque();
                Items.Add(item);
            }
            else // TODO: Rever isso!!!
                Console.WriteLine($"Não temos tantos copos de limonada {produto.Nome}");
        }

        public decimal Fechar()
        {
            EstaAberto = false;
            return Items.Sum(x => x.Quantidade * x.Produto.Preco);
        }
    }
}