using Ajudante_Familiar_Global.Domain.Entities;

namespace Ajudante_Familiar_Global.Domain.Interfaces
{
    public interface IIdosoRepository
    {
        Task<Idoso> CreateIdosoAsync(Idoso idoso);
        Task<Idoso> GetIdosoAsync(int id_idoso);
        Task<IEnumerable<Idoso>> GetAllIdosoAsync();
        Task<Idoso> UpdateIdosoAsync(int id_idoso, Idoso idoso);
        Task<Idoso> DeleteIdosoAsync(int id_idoso);
    }
}
