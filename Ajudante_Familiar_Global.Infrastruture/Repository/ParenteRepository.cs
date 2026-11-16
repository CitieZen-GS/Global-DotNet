using Ajudante_Familiar_Global.Domain.Entities;
using Ajudante_Familiar_Global.Domain.Interfaces;
using Ajudante_Familiar_Global.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Ajudante_Familiar_Global.Infrastruture.Repository
{
    public class ParenteRepository : IParenteRepository
    {
        private readonly AppDbContext _context;

        public ParenteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Parente> CreateParenteAsync(Parente parente)
        {
            await _context.Parentes.AddAsync(parente);
            await _context.SaveChangesAsync();
            return parente;
        }

        public async Task<Parente> DeleteParenteAsync(int id_parente)
        {
            var parente = await _context.Parentes.FindAsync(id_parente);
            if (parente == null)
            {
                throw new KeyNotFoundException($"Id parente com id {id_parente} não foi encontrado");
            }
            _context.Parentes.Remove(parente);
            await _context.SaveChangesAsync();
            return parente;
        }

        public async Task<Parente> GetParenteAsync(int id_parente)
        {
            var existeParente = await _context.Parentes.FindAsync(id_parente);
            if (existeParente == null)
            {
                throw new KeyNotFoundException($"Parente com id {id_parente} não encontrado.");
            }
            return existeParente;
        }

        public async Task<IEnumerable<Parente>> GetAllParenteAsync()
        {
            return await _context.Parentes.ToListAsync();
        }

        public async Task<Parente> UpdateParenteAsync(int id_parente, Parente parente)
        {
            var existeParente = await _context.Parentes.FindAsync(id_parente);
            if (existeParente == null)
            {
                throw new KeyNotFoundException($"Parente com id {id_parente} não foi encontrado.");
            }

            existeParente.Nm_parente = parente.Nm_parente;
            existeParente.Nr_telefone = parente.Nr_telefone;
            existeParente.Ds_email = parente.Ds_email;
            existeParente.Ds_senha = parente.Ds_senha;
            existeParente.Id_idoso = parente.Id_idoso;
            existeParente.Id_perfil = parente.Id_perfil;

            await _context.SaveChangesAsync();

            return existeParente;
        }
    }
}
