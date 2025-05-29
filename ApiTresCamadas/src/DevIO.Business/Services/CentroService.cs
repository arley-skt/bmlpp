using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class CentroService : BaseService, ICentroService
    {
        private readonly ICentroRepository _centroRepository;

        public CentroService(ICentroRepository centroRepository,
                              INotificador notificador) : base(notificador)
        {
            _centroRepository = centroRepository;
        }

        public async Task Adicionar(Centro centro)
        {

            
            if (!ExecutarValidacao(new CentroValidation(), centro)) return;

            var centroExistente = await _centroRepository.ObterPorId(centro.Id);

            if (centroExistente == null)
            {
                 centroExistente = await _centroRepository.ObterPorNome(centro.Nome);
            }
               

            if (centroExistente != null)
            {
                Notificar("Já existe um Centro ou nome com o ID informado!");
                return;
            }

            await _centroRepository.Adicionar(centro);
        }

        public async Task Atualizar(Centro centro)
        {
            if (!ExecutarValidacao(new CentroValidation(), centro)) return;

            await _centroRepository.Atualizar(centro);
        }

        public async Task Remover(Guid id)
        {
            await _centroRepository.Remover(id);
        }

        public void Dispose()
        {
            _centroRepository?.Dispose();
        }
    }
}
