using DontBase.Shareds.Repositories;

namespace DontBase.Entities
{
    public class Produto : IEntity
    {
        public long Id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
    }
}
