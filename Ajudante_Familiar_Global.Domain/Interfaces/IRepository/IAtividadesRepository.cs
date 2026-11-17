using Ajudante_Familiar_Global.Domain.Entities;

namespace Ajudante_Familiar_Global.Domain.Interfaces.IRepository
{
    public interface IAtividadesRepository
    {
        Task<Atividades> CreateAtividadesAsync(Atividades atividades);
        Task<Atividades> GetAtividadesAsync(int id_atividade);
        Task<IEnumerable<Atividades>> GetAllAtividadesAsync();
        Task<Atividades> UpdateAtividadesAsync(int id_atividade, Atividades atividades);
        Task<Atividades> DeleteAtividadesAsync(int id_atividade);
    }
}
