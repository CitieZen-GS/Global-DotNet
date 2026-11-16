using Ajudante_Familiar_Global.Domain.Entities;

namespace Ajudante_Familiar_Global.Domain.Interfaces
{
    public interface ICargoRepository
    {
        Task<Cargo> CreateCargoAsync(Cargo cargo);
        Task<Cargo> GetCargoAsync(int id_cargo);
        Task<IEnumerable<Cargo>> GetAllCargoAsync();
        Task<Cargo> UpdateCargoAsync(int id_cargo, Cargo cargo);
        Task<Cargo> DeleteCargoAsync(int id_cargo);
    }
}
