using Ajudante_Familiar_Global.Domain.Entities;

namespace Ajudante_Familiar_Global.Domain.Interfaces.IService
{
    public interface IConsulta_MedicamentoService
    {
        Task<Consulta_Medicamento> CreateConsulta_MedicamentoAsync(Consulta_Medicamento consulta_Medicamento);

        Task<Consulta_Medicamento> GetConsulta_MedicamentoAsync(int id_consulta_medicamento);
        Task<IEnumerable<Consulta_Medicamento>> GetAllConsulta_MedicamentoAsync();

        Task<Consulta_Medicamento> DeleteConsulta_MedicamentoAsync(int id_consulta_medicamento);
        Task<Consulta_Medicamento> UpdateConsulta_MedicamentoAsync(int id_consulta_medicamento, Consulta_Medicamento consulta_Medicamento);
    }
}
