using Ajudante_Familiar_Global.Domain.Entities;
using Ajudante_Familiar_Global.Domain.Interfaces.IRepository;
using Ajudante_Familiar_Global.Domain.Interfaces.IService;
using Microsoft.Extensions.Logging;

namespace Ajudante_Familiar_Global.Application.Services
{
    public class Lembrete_ConsultaService : ILembrete_ConsultaService
    {
        private readonly ILembrete_ConsultaRepository _lembrete_Consulta;
        private readonly IIdosoRepository _idoso;
        private readonly ILogger<Lembrete_ConsultaService> _logger;

        public Lembrete_ConsultaService(ILembrete_ConsultaRepository lembrete_Consulta, IIdosoRepository idoso, ILogger<Lembrete_ConsultaService> logger)
        {
            _lembrete_Consulta = lembrete_Consulta;
            _logger = logger;
            _idoso = idoso;
        }

        public async Task<Lembrete_Consulta> CreateLembrete_ConsultaAsync(Lembrete_Consulta lembrete_Consulta)
        {
            _logger.LogInformation("Lembrete consulta iniciado");
            var existeIdoso = await _idoso.GetIdosoAsync(lembrete_Consulta.Id_idoso);

            if (existeIdoso == null)
            {
                _logger.LogWarning("Sem o id idoso");
                throw new KeyNotFoundException("Idoso não encontrado para fazer a conexao");
            }

            return await _lembrete_Consulta.CreateLembrete_ConsultaAsync(lembrete_Consulta);
        }

        public async Task<Lembrete_Consulta> DeleteLembrete_ConsultaAsync(int id_lembrete_consulta)
        {
            _logger.LogInformation("Excluido lembrete consulta");

            var deletado = await _lembrete_Consulta.DeleteLembrete_ConsultaAsync(id_lembrete_consulta);
            return deletado;
        }

        public async Task<Lembrete_Consulta> GetLembrete_ConsultaAsync(int id_lembrete_consulta)
        {
            _logger.LogInformation("Buscar lembrete consulta");
            return await _lembrete_Consulta.GetLembrete_ConsultaAsync(id_lembrete_consulta);
        }

        public async Task<IEnumerable<Lembrete_Consulta>> GetAllLembrete_ConsultaAsync()
        {
            _logger.LogInformation("Listando lembretes consulta");

            return await _lembrete_Consulta.GetAllLembrete_ConsultaAsync();
        }

        public async Task<Lembrete_Consulta> UpdateLembrete_ConsultaAsync(int id_lembrete_consulta, Lembrete_Consulta lembrete_Consulta)
        {
            _logger.LogInformation("Atualizando lembrete consulta");

            var existeIdoso = await _idoso.GetIdosoAsync(lembrete_Consulta.Id_idoso);

            if (existeIdoso == null)
            {
                _logger.LogWarning("Sem o id idoso");
                throw new KeyNotFoundException("Idoso não encontrado para fazer a conexao");
            }
            return await _lembrete_Consulta.UpdateLembrete_ConsultaAsync(id_lembrete_consulta, lembrete_Consulta);
        }
    }
}
