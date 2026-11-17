using Ajudante_Familiar_Global.Domain.Entities;
using Ajudante_Familiar_Global.Domain.Interfaces.IRepository;
using Ajudante_Familiar_Global.Domain.Interfaces.IService;
using Microsoft.Extensions.Logging;

namespace Ajudante_Familiar_Global.Application.Services
{
    public class AtividadesService : IAtividadesService
    {
        private readonly IAtividadesRepository _atividades;
        private readonly IIdosoRepository _idoso;
        private readonly ILogger<AtividadesService> _logger;

        public AtividadesService(IAtividadesRepository atividades, IIdosoRepository idoso, ILogger<AtividadesService> logger)
        {
            _atividades = atividades;
            _logger = logger;
            _idoso = idoso;
        }

        public async Task<Atividades> CreateAtividadesAsync(Atividades atividades)
        {
            _logger.LogInformation("Atividades iniciado");
            var existeIdoso = await _idoso.GetIdosoAsync(atividades.id_idoso);

            if (existeIdoso == null)
            {
                _logger.LogWarning("Sem o id idoso");
                throw new KeyNotFoundException("Idoso não encontrado para fazer a conexao");
            }

            return await _atividades.CreateAtividadesAsync(atividades);
        }

        public async Task<Atividades> DeleteAtividadesAsync(int id_atividade)
        {
            _logger.LogInformation("Excluido atividades");

            var deletado = await _atividades.DeleteAtividadesAsync(id_atividade);
            return deletado;
        }

        public async Task<Atividades> GetAtividadesAsync(int id_atividade)
        {
            _logger.LogInformation("Buscar atividades");
            return await _atividades.GetAtividadesAsync(id_atividade);
        }

        public async Task<IEnumerable<Atividades>> GetAllAtividadesAsync()
        {
            _logger.LogInformation("Listando atividades");

            return await _atividades.GetAllAtividadesAsync();
        }

        public async Task<Atividades> UpdateAtividadesAsync(int id_atividade, Atividades atividades)
        {
            _logger.LogInformation("Atualizando atividades");

            var existeIdoso = await _idoso.GetIdosoAsync(atividades.id_idoso);

            if (existeIdoso == null)
            {
                _logger.LogWarning("Sem o id idoso");
                throw new KeyNotFoundException("Idoso não encontrado para fazer a conexao");
            }
            return await _atividades.UpdateAtividadesAsync(id_atividade, atividades);
        }
    }
}
