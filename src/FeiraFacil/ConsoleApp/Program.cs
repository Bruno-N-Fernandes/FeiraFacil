using FeiraFacil.Core;

namespace ConsoleApp
{
    public static class Program
    {
        private static readonly Sistema _sistema = new Sistema();
        private static readonly ITabela _tabelaProduto = CreateTabelaProduto();


        public static void Main(string[] args)
        {
            var app = new App(false);
            app.AddMenu("0", "Inicializar Sistema", _sistema.Seed);
            app.AddMenu("1", "Listar Produtos", Produtos);
            app.AddMenu("1", "Nova Venda", _sistema.Seed);
            app.AddMenu("X", "Sair do Sistema", app.Sair);
            app.Run();
        }

        private static void Produtos()
        {
            _tabelaProduto.Draw(_sistema.Produtos);
        }

        private static ITabela CreateTabelaProduto()
        {
            var tabela = new Tabela<Produto>();
            tabela.Add("Codigo", 6, x => $"{x.Codigo}");
            tabela.Add("Nome", -30, x => $"{x.Nome}");
            tabela.Add("Estoque", 7, x => $"{x.Estoque:0.000}");
            tabela.Add("Custo", 10, x => $"{x.Custo:0.00}");
            tabela.Add("Preco", 10, x => $"{x.Preco:0.00}");
            return tabela;
        }
    }
}