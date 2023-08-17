using System;
using System.Collections.Generic;

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
                Items.Add(new ItemPedido { Produto = produto, Quantidade = quantidade });
            else
                Console.WriteLine($"Não temos tantos copos de limonada {produto.Nome}");
        }


        public void Fechar()
        {
            EstaAberto = false;
            foreach (var item in Items)
                item.BaixarEstoque();
        }

    }
}