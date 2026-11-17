using Ajudante_Familiar_Global.Domain.Entities;
using Ajudante_Familiar_Global.Domain.Interfaces.IRepository;
using Ajudante_Familiar_Global.Domain.Interfaces.IService;
using Microsoft.Extensions.Logging;

namespace Ajudante_Familiar_Global.Application.Services
{
    public class IdosoService : IIdosoService
    {
        private readonly IIdosoRepository _idoso;
        private readonly ICuidadorRepository _cuidador;
        private readonly ILogger<IdosoService> _logger;

        public IdosoService(IIdosoRepository idoso, ICuidadorRepository cuidador, ILogger<IdosoService> logger)
        {
            _idoso = idoso;
            _logger = logger;
            _cuidador = cuidador;
        }

        public async Task<Idoso> CreateIdosoAsync(Idoso idoso)
        {
            _logger.LogInformation("Idoso iniciado");
            var existeCuidador = await _cuidador.GetCuidadorAsync(idoso.id_cuidador);

            if (existeCuidador == null)
            {
                _logger.LogWarning("Sem o id cuidador");
                throw new KeyNotFoundException("Cuidador não encontrado para fazer a conexao");
            }

            return await _idoso.CreateIdosoAsync(idoso);
        }

        public async Task<Idoso> DeleteIdosoAsync(int id_idoso)
        {
            _logger.LogInformation("Excluido idoso");

            var deletado = await _idoso.DeleteIdosoAsync(id_idoso);
            return deletado;
        }

        public async Task<Idoso> GetIdosoAsync(int id_idoso)
        {
            _logger.LogInformation("Buscar idoso");
            return await _idoso.GetIdosoAsync(id_idoso);
        }

        public async Task<IEnumerable<Idoso>> GetAllIdosoAsync()
        {
            _logger.LogInformation("Listando idosos");

            return await _idoso.GetAllIdosoAsync();
        }

        public async Task<Idoso> UpdateIdosoAsync(int id_idoso, Idoso idoso)
        {
            _logger.LogInformation("Atualizando idoso");

            var existeCuidador = await _cuidador.GetCuidadorAsync(idoso.id_cuidador);

            if (existeCuidador == null)
            {
                _logger.LogWarning("Sem o id cuidador");
                throw new KeyNotFoundException("Cuidador não encontrado para fazer a conexao");
            }
            return await _idoso.UpdateIdosoAsync(id_idoso, idoso);
        }
    }
}
