using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface ICentroService : IDisposable
    {
        Task Adicionar(Centro centro);
        Task Atualizar(Centro centro);
        Task Remover(Guid id);
    }
}
