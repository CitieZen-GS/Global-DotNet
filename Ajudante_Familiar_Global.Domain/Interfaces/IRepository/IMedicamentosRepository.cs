using Ajudante_Familiar_Global.Domain.Entities;

namespace Ajudante_Familiar_Global.Domain.Interfaces.IRepository
{
    public interface IMedicamentosRepository
    {
        Task<Medicamentos> CreateMedicamentosAsync(Medicamentos medicamentos);
        Task<Medicamentos> GetMedicamentosAsync(int id_medicamentos);
        Task<IEnumerable<Medicamentos>> GetAllMedicamentosAsync();
        Task<Medicamentos> UpdateMedicamentosAsync(int id_medicamentos, Medicamentos medicamentos);
        Task<Medicamentos> DeleteMedicamentosAsync(int id_medicamentos);
    }
}
