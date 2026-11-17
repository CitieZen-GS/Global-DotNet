using Ajudante_Familiar_Global.Domain.Entities;

namespace Ajudante_Familiar_Global.Domain.Interfaces.IRepository
{
    public interface IAlimentacaoRepository
    {
        Task<Alimentacao> CreateAlimentacaoAsync(Alimentacao alimentacao);
        Task<Alimentacao> GetAlimentacaoAsync(int id_alimentacao);
        Task<IEnumerable<Alimentacao>> GetAllAlimentacaoAsync();
        Task<Alimentacao> UpdateAlimentacaoAsync(int id_alimentacao, Alimentacao alimentacao);
        Task<Alimentacao> DeleteAlimentacaoAsync(int id_alimentacao);
    }
}
