using Ajudante_Familiar_Global.Domain.Entities;

namespace Ajudante_Familiar_Global.Domain.Interfaces.IService
{
    public interface ICargoService
    {
        Task<Cargo> CreateCargoAsync(Cargo cargo);

        Task<Cargo> GetCargoAsync(int id_cargo);
        Task<IEnumerable<Cargo>> GetAllCargoAsync();

        Task<Cargo> DeleteCargoAsync(int id_cargo);
        Task<Cargo> UpdateCargoAsync(int id_cargo, Cargo cargo);
    }
}
