using Ajudante_Familiar_Global.Domain.Entities;
using Ajudante_Familiar_Global.Domain.Interfaces.IRepository;
using Ajudante_Familiar_Global.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Ajudante_Familiar_Global.Infrastruture.Repository
{
    public class IdosoRepository : IIdosoRepository
    {
        private readonly AppDbContext _context;

        public IdosoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Idoso> CreateIdosoAsync(Idoso idoso)
        {
            await _context.Idosos.AddAsync(idoso);
            await _context.SaveChangesAsync();
            return idoso;
        }

        public async Task<Idoso> DeleteIdosoAsync(int id_idoso)
        {
            var idoso = await _context.Idosos.FindAsync(id_idoso);
            if (idoso == null)
            {
                throw new KeyNotFoundException($"Id idoso com id {id_idoso} não foi encontrado");
            }
            _context.Idosos.Remove(idoso);
            await _context.SaveChangesAsync();
            return idoso;
        }

        public async Task<Idoso> GetIdosoAsync(int id_idoso)
        {
            var existeIdoso = await _context.Idosos.FindAsync(id_idoso);
            if (existeIdoso == null)
            {
                throw new KeyNotFoundException($"Idoso com id {id_idoso} não encontrado.");
            }
            return existeIdoso;
        }

        public async Task<IEnumerable<Idoso>> GetAllIdosoAsync()
        {
            return await _context.Idosos.ToListAsync();
        }

        public async Task<Idoso> UpdateIdosoAsync(int id_idoso, Idoso idoso)
        {
            var existeIdoso = await _context.Idosos.FindAsync(id_idoso);
            if (existeIdoso == null)
            {
                throw new KeyNotFoundException($"Idoso com id {id_idoso} não foi encontrado.");
            }

            existeIdoso.Nm_idoso = idoso.Nm_idoso;
            existeIdoso.cpf_idoso = idoso.cpf_idoso;
            existeIdoso.vlr_altura = idoso.vlr_altura;
            existeIdoso.vlr_peso = idoso.vlr_peso;
            existeIdoso.dt_nascimento = idoso.dt_nascimento;
            existeIdoso.ds_comportamento = idoso.ds_comportamento;
            existeIdoso.id_cuidador = idoso.id_cuidador;

            await _context.SaveChangesAsync();

            return existeIdoso;
        }
    }
}
