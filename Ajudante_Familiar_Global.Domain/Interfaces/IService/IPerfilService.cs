using Ajudante_Familiar_Global.Domain.Entities;

namespace Ajudante_Familiar_Global.Domain.Interfaces.IService
{
    public interface IPerfilService
    {
        Task<Perfil> CreatePerfilAsync(Perfil perfil);

        Task<Perfil> GetPerfilAsync(int id_perfil);
        Task<IEnumerable<Perfil>> GetAllPerfilAsync();

        Task<Perfil> DeletePerfilAsync(int id_perfil);
        Task<Perfil> UpdatePerfilAsync(int id_perfil, Perfil perfil);
    }
}
