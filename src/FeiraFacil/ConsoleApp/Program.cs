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

        public static void Main()
        {
            var app = new App();
            app.AddMenu("0", "Inicializar Sistema", () => _sistema.Seed());
            app.AddMenu("P", "Listar Produtos", () => _boxProduto.Draw(_sistema.Produtos));
            app.AddMenu("C", "Listar Clientes", () => _boxCliente.Draw(_sistema.Clientes));
            app.AddMenu("V", "Listar Vendas", () => _boxPedidos.Draw(_sistema.Pedidos));
            app.AddMenu("N", "Novo Pedido", () => Vender());
            app.AddMenu("F", "Fechar Conta", () => Fechar());
            app.AddMenu("X", "Sair do Sistema", app.Sair);
            app.Run();
        }

        private static void Fechar()
        {
            var codigoCliente = GetInt("Informe o código do Cliente");
            var cliente = _sistema.Clientes.FirstOrDefault(x => x.Codigo == codigoCliente);
            var valor = _sistema.FecharConta(cliente);

            Console.WriteLine($"Sua conta deu {valor:0.00}");
        }

        private static void Vender()
        {
            var codigoCliente = GetInt("Qual o Código do Cliente: ");
            var cliente = _sistema.Clientes.FirstOrDefault(x => x.Codigo == codigoCliente);

            var codigoProduto = GetInt($"Oi {cliente?.Nome}, Qual o Código do Produto: ");
            var produto = _sistema.Produtos.FirstOrDefault(x => x.Codigo == codigoProduto);

            var quantidade = GetInt($"Informe a Quantidade de {produto?.Nome}: ");

            _sistema.Atender(cliente, produto, quantidade);
        }

        private static int GetInt(string mensagem) => int.Parse(Get(mensagem));

        private static string Get(string mensagem)
        {
            Console.Write(mensagem);
            return Console.ReadLine();
        }
    }
}