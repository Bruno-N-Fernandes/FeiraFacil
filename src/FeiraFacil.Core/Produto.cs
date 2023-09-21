namespace FeiraFacil.Core
{
    public class Produto
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Custo { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }

        public void BaixarEstoque(int quantidade)
        {
            Estoque -= quantidade;
        }
    }
}