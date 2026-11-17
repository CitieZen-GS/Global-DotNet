using Ajudante_Familiar_Global.Domain.Entities;

namespace Ajudante_Familiar_Global.Domain.Interfaces.IRepository
{
    public interface IDoencasRepository
    {
        Task<Doencas> CreateDoencasAsync(Doencas doencas);
        Task<Doencas> GetDoencasAsync(int id_doenca);
        Task<IEnumerable<Doencas>> GetAllDoencasAsync();
        Task<Doencas> UpdateDoencasAsync(int id_doenca, Doencas doencas);
        Task<Doencas> DeleteDoencasAsync(int id_doenca);
    }
}
