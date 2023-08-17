namespace FeiraFacil.Core
{
    public class ItemPedido
    {
        public int Quantidade { get; set; }
        public Produto Produto { get; set; }

        public void BaixarEstoque()
        {
            Produto.BaixarEstoque(Quantidade);
        }
    }
}