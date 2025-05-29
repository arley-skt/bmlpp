using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> Obter(string email, string senha);

        Task<Usuario> ObterusuarioporId(Guid Id);
        Task<Usuario> Obterativo(string email, string senha);

        Task<Fornecedor> ObterFornecedorEndereco(Guid id);
        Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id);

        Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId);
        Task RemoverEnderecoFornecedor(Endereco endereco);
        Task<IEnumerable<Usuario>> Todos(bool ativo);
        Task<List<Usuario>> Rotas(bool ativo);
    }
}
