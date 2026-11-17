using Ajudante_Familiar_Global.Domain.Entities;
using Ajudante_Familiar_Global.Domain.Interfaces.IRepository;
using Ajudante_Familiar_Global.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Ajudante_Familiar_Global.Infrastruture.Repository
{
    public class CargoRepository : ICargoRepository
    {
        private readonly AppDbContext _context;

        public CargoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Cargo> CreateCargoAsync(Cargo cargo)
        {
            await _context.Cargos.AddAsync(cargo);
            await _context.SaveChangesAsync();
            return cargo;
        }

        public async Task<Cargo> DeleteCargoAsync(int id_cargo)
        {
            var cargo = await _context.Cargos.FindAsync(id_cargo);
            if (cargo == null)
            {
                throw new KeyNotFoundException($"Id cargo com id {id_cargo} não foi encontrado");
            }
            _context.Cargos.Remove(cargo);
            await _context.SaveChangesAsync();
            return cargo;
        }

        public async Task<Cargo> GetCargoAsync(int id_cargo)
        {
            var existeCargo = await _context.Cargos.FindAsync(id_cargo);
            if (existeCargo == null)
            {
                throw new KeyNotFoundException($"Cargo com id {id_cargo} não encontrado.");
            }
            return existeCargo;
        }

        public async Task<IEnumerable<Cargo>> GetAllCargoAsync()
        {
            return await _context.Cargos.ToListAsync();
        }

        public async Task<Cargo> UpdateCargoAsync(int id_cargo, Cargo cargo)
        {
            var existeCargo = await _context.Cargos.FindAsync(id_cargo);
            if (existeCargo == null)
            {
                throw new KeyNotFoundException($"Cargo com id {id_cargo} não foi encontrado.");
            }

            existeCargo.Nm_cargo = cargo.Nm_cargo;
            existeCargo.Fg_permissao = cargo.Fg_permissao;

            await _context.SaveChangesAsync();

            return existeCargo;
        }
    }
}
