using Ajudante_Familiar_Global.Domain.Entities;

namespace Ajudante_Familiar_Global.Domain.Interfaces.IService
{
    public interface IIdosoService
    {
        Task<Idoso> CreateIdosoAsync(Idoso idoso);

        Task<Idoso> GetIdosoAsync(int id_idoso);
        Task<IEnumerable<Idoso>> GetAllIdosoAsync();

        Task<Idoso> DeleteIdosoAsync(int id_idoso);
        Task<Idoso> UpdateIdosoAsync(int id_idoso, Idoso idoso);
    }
}
