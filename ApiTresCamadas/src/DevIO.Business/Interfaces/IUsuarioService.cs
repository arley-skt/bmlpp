using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IUsuarioService : IDisposable
    {
        Task<Usuario> Obter(Usuario usuario);
        Task<Usuario> Obter(string email, string senha);
        Task Adicionar(Usuario fornecedor);
        Task Atualizar(Usuario fornecedor);
        Task Remover(Guid id);
        Task<List<Usuario>> Todos(bool ativo);
        Task<List<Usuario>> Rotas(bool ativo);

        Task<Usuario> ObterusuarioporId(Guid Id);
    }
}
