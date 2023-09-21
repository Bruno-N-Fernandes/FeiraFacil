using FeiraFacil.Core;
using System.Linq;

namespace ConsoleApp
{
    public static class BoxFactory
    {
        public static IBox CreateBoxPedido()
        {
            var tabela = new Box<PedidoDeVenda>("Pedidos de Venda", IBox.LineBorder2);
            tabela.Add(008, "  Data  ", x => $"{x.DataVenda:dd/MM/yy}");
            tabela.Add(-12, "Cliente ", x => $"{x.Cliente.Nome}");
            tabela.Add(-79, "Produtos", x => string.Join("; ", x.Items.Select(i => $"{i.Quantidade} {i.Produto.Nome}")));
            tabela.Add(008, "Valor", x => $"{x.Valor:#,##0.00}");
            return tabela;
        }

        public static IBox CreateBoxProduto()
        {
            var tabela = new Box<Produto>("Cadastro de Produtos", IBox.LineBorder2);
            tabela.Add(006, "Codigo", x => $"{x.Codigo}");
            tabela.Add(-30, "Nome   ", x => $"{x.Nome}");
            tabela.Add(010, "Estoque", x => $"{x.Estoque:#,##0.000}");
            tabela.Add(010, "  Custo", x => $"{x.Custo:#,##0.00}");
            tabela.Add(010, "  Preco", x => $"{x.Preco:#,##0.00}");
            return tabela;
        }

        public static IBox CreateBoxCliente()
        {
            var tabela = new Box<Cliente>("Cadastro de Clientes", IBox.LineBorder2);
            tabela.Add(006, "Codigo", x => $"{x.Codigo}");
            tabela.Add(-30, "Nome  ", x => $"{x.Nome}");
            return tabela;
        }
    }
}