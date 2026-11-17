using Ajudante_Familiar_Global.Domain.Entities;
using Ajudante_Familiar_Global.Domain.Interfaces.IRepository;
using Ajudante_Familiar_Global.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Ajudante_Familiar_Global.Infrastruture.Repository
{
    public class CuidadorRepository : ICuidadorRepository
    {
        private readonly AppDbContext _context;

        public CuidadorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Cuidador> CreateCuidadorAsync(Cuidador cuidador)
        {
            await _context.Cuidadores.AddAsync(cuidador);
            await _context.SaveChangesAsync();
            return cuidador;
        }

        public async Task<Cuidador> DeleteCuidadorAsync(int id_cuidador)
        {
            var cuidador = await _context.Cuidadores.FindAsync(id_cuidador);
            if (cuidador == null)
            {
                throw new KeyNotFoundException($"Id cuidador com id {id_cuidador} não foi encontrado");
            }
            _context.Cuidadores.Remove(cuidador);
            await _context.SaveChangesAsync();
            return cuidador;
        }

        public async Task<Cuidador> GetCuidadorAsync(int id_cuidador)
        {
            var existeCuidador = await _context.Cuidadores.FindAsync(id_cuidador);
            if (existeCuidador == null)
            {
                throw new KeyNotFoundException($"Cuidador com id {id_cuidador} não encontrado.");
            }
            return existeCuidador;
        }

        public async Task<IEnumerable<Cuidador>> GetAllCuidadorAsync()
        {
            return await _context.Cuidadores.ToListAsync();
        }

        public async Task<Cuidador> UpdateCuidadorAsync(int id_cuidador, Cuidador cuidador)
        {
            var existeCuidador = await _context.Cuidadores.FindAsync(id_cuidador);
            if (existeCuidador == null)
            {
                throw new KeyNotFoundException($"Cuidador com id {id_cuidador} não foi encontrado.");
            }

            existeCuidador.Nm_cuidador = cuidador.Nm_cuidador;
            existeCuidador.Cpf_cuidador = cuidador.Cpf_cuidador;
            existeCuidador.Ds_email = cuidador.Ds_email;
            existeCuidador.Ds_senha = cuidador.Ds_senha;
            existeCuidador.Id_perfil = cuidador.Id_perfil;

            await _context.SaveChangesAsync();

            return existeCuidador;
        }
    }
}
