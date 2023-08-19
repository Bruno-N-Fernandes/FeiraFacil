using FeiraFacil.Core;
using System;
using System.Linq;

namespace ConsoleApp
{
    public static class Program
    {
        private static readonly Sistema _sistema = new(seed: true);
        private static readonly IBox _boxProduto = BoxFactory.CreateBoxProduto();
        private static readonly IBox _boxCliente = BoxFactory.CreateBoxCliente();
        private static readonly IBox _boxPedidos = BoxFactory.CreateBoxPedido();

        public static int Main()
        {
            var menuApp = new MenuApp();
            menuApp.AddMenu("0", "Setup do Sistema", _sistema.Seed);
            menuApp.AddMenu("P", "Listar Produtos ", ListarProdutos);
            menuApp.AddMenu("C", "Listar Clientes ", ListarClientes);
            menuApp.AddMenu("V", "Listar Vendas   ", ListarVendas);
            menuApp.AddMenu("A", "Atender Cliente ", AtenderCliente);
            menuApp.AddMenu("F", "Fechar Conta    ", FecharConta);
            menuApp.AddMenu("X", "Sair do Sistema ", menuApp.Sair);
            return menuApp.Run();
        }

        private static void ListarProdutos() => _boxProduto.Draw(_sistema.Produtos);

        private static void ListarClientes() => _boxCliente.Draw(_sistema.Clientes);

        private static void ListarVendas() => _boxPedidos.Draw(_sistema.Pedidos);

        private static void AtenderCliente()
        {
            var codigoCliente = GetInt("Qual o Código do Cliente: ");
            var cliente = _sistema.Clientes.FirstOrDefault(x => x.Codigo == codigoCliente);

            var codigoProduto = GetInt($"Oi {cliente?.Nome}, Qual o Código do Produto: ");
            var produto = _sistema.Produtos.FirstOrDefault(x => x.Codigo == codigoProduto);

            var quantidade = GetInt($"Informe a Quantidade de {produto?.Nome}: ");

            _sistema.Atender(cliente, produto, quantidade);
        }

        private static void FecharConta()
        {
            var codigoCliente = GetInt("Informe o código do Cliente: ");
            var cliente = _sistema.Clientes.FirstOrDefault(x => x.Codigo == codigoCliente);
            var valor = _sistema.FecharConta(cliente);

            Console.WriteLine($"Sua conta deu {valor:#,##0.00}");
        }


        private static int GetInt(string mensagem) => int.Parse(Get(mensagem));

        private static string Get(string mensagem)
        {
            Console.Write(mensagem);
            return Console.ReadLine();
        }
    }
}