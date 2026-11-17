using Ajudante_Familiar_Global.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ajudante_Familiar_Global.Domain.Interfaces.IService
{
    public interface IAlimentacaoService
    {
        Task<Alimentacao> CreateAlimentacaoAsync(Alimentacao alimentacao);

        Task<Alimentacao> GetAlimentacaoAsync(int id_alimentacao);
        Task<IEnumerable<Alimentacao>> GetAllAlimentacaoAsync();

        Task<Alimentacao> DeleteAlimentacaoAsync(int id_alimentacao);
        Task<Alimentacao> UpdateAlimentacaoAsync(int id_alimentacao, Alimentacao alimentacao);
    }
}
