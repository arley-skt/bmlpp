
namespace DevIO.Business.Models
{
    public class Usuario : Entity
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        //public TipoFornecedor TipoFornecedor { get; set; }
        public bool Ativo { get; set; }
        //public Endereco? Endereco { get; set; }

        /* EF Relation */
        //public IEnumerable<Produto> Produtos { get; set; }
    }
    
}
