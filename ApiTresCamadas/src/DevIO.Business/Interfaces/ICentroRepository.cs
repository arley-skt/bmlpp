using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface ICentroRepository : IRepository<Centro>
    {      
        Task<IEnumerable<Centro>> ObterCentros();
        Task<Centro> ObterPorNome(string nome);


    }
}
