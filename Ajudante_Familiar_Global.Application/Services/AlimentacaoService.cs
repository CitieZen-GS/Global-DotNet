using Ajudante_Familiar_Global.Domain.Entities;
using Ajudante_Familiar_Global.Domain.Interfaces.IRepository;
using Ajudante_Familiar_Global.Domain.Interfaces.IService;
using Microsoft.Extensions.Logging;

namespace Ajudante_Familiar_Global.Application.Services
{
    public class AlimentacaoService : IAlimentacaoService
    {
        private readonly IAlimentacaoRepository _alimentacao;
        private readonly IIdosoRepository _idoso;
        private readonly ILogger<AlimentacaoService> _logger;

        public AlimentacaoService(IAlimentacaoRepository alimentacao, IIdosoRepository idoso, ILogger<AlimentacaoService> logger)
        {
            _alimentacao = alimentacao;
            _logger = logger;
            _idoso = idoso;
        }

        public async Task<Alimentacao> CreateAlimentacaoAsync(Alimentacao alimentacao)
        {
            _logger.LogInformation("Alimentacao iniciado");
            var existeIdoso = await _idoso.GetIdosoAsync(alimentacao.Id_idoso);

            if (existeIdoso == null)
            {
                _logger.LogWarning("Sem o id idoso");
                throw new KeyNotFoundException("Idoso não encontrado para fazer a conexao");
            }

            return await _alimentacao.CreateAlimentacaoAsync(alimentacao);
        }

        public async Task<Alimentacao> DeleteAlimentacaoAsync(int id_alimentacao)
        {
            _logger.LogInformation("Excluido alimentacao");

            var deletado = await _alimentacao.DeleteAlimentacaoAsync(id_alimentacao);
            return deletado;
        }

        public async Task<Alimentacao> GetAlimentacaoAsync(int id_alimentacao)
        {
            _logger.LogInformation("Buscar alimentos");
            return await _alimentacao.GetAlimentacaoAsync(id_alimentacao);
        }

        public async Task<IEnumerable<Alimentacao>> GetAllAlimentacaoAsync()
        {
            _logger.LogInformation("Listando alimentações");

            return await _alimentacao.GetAllAlimentacaoAsync();
        }

        public async Task<Alimentacao> UpdateAlimentacaoAsync(int id_alimentacao, Alimentacao alimentacao)
        {
            _logger.LogInformation("Atualizando alimentação");

            var existeIdoso = await _idoso.GetIdosoAsync(alimentacao.Id_idoso);

            if (existeIdoso == null)
            {
                _logger.LogWarning("Sem o id idoso");
                throw new KeyNotFoundException("Idoso não encontrado para fazer a conexao");
            }
            return await _alimentacao.UpdateAlimentacaoAsync(id_alimentacao, alimentacao);

        }
    }
}
