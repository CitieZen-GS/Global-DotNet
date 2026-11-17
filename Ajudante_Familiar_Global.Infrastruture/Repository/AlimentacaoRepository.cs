using Ajudante_Familiar_Global.Domain.Entities;
using Ajudante_Familiar_Global.Domain.Interfaces.IRepository;
using Ajudante_Familiar_Global.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Ajudante_Familiar_Global.Infrastruture.Repository
{
    public class AlimentacaoRepository : IAlimentacaoRepository
    {
        private readonly AppDbContext _context;

        public AlimentacaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Alimentacao> CreateAlimentacaoAsync(Alimentacao alimentacao)
        {
            await _context.Alimentacoes.AddAsync(alimentacao);
            await _context.SaveChangesAsync();
            return alimentacao;
        }

        public async Task<Alimentacao> DeleteAlimentacaoAsync(int id_alimentacao)
        {
            var alimentacao = await _context.Alimentacoes.FindAsync(id_alimentacao);
            if (alimentacao == null)
            {
                throw new KeyNotFoundException($"Id alimentacao com id {id_alimentacao} não foi encontrado");
            }
            _context.Alimentacoes.Remove(alimentacao);
            await _context.SaveChangesAsync();
            return alimentacao;
        }

        public async Task<Alimentacao> GetAlimentacaoAsync(int id_alimentacao)
        {
            var existeALimentacao = await _context.Alimentacoes.FindAsync(id_alimentacao);
            if (existeALimentacao == null)
            {
                throw new KeyNotFoundException($"Animal com id {id_alimentacao} não encontrado.");
            }
            return existeALimentacao;
        }

        public async Task<IEnumerable<Alimentacao>> GetAllAlimentacaoAsync()
        {
            return await _context.Alimentacoes.ToListAsync();
        }

        public async Task<Alimentacao> UpdateAlimentacaoAsync(int id_alimentacao, Alimentacao alimentacao)
        {
            var existeAlimentacao = await _context.Alimentacoes.FindAsync(id_alimentacao);
            if (existeAlimentacao == null)
            {
                throw new KeyNotFoundException($"Alimentacao nao encontrado o id");
            }

            existeAlimentacao.Nm_alimentacao = alimentacao.Nm_alimentacao;
            existeAlimentacao.tp_alimentacao = alimentacao.tp_alimentacao;
            existeAlimentacao.Dt_consumo = alimentacao.Dt_consumo;
            existeAlimentacao.Hr_consumo = alimentacao.Hr_consumo;
            existeAlimentacao.Vlr_caloria = alimentacao.Vlr_caloria;
            existeAlimentacao.Ds_preparacao = alimentacao.Ds_preparacao;
            existeAlimentacao.Id_idoso = alimentacao.Id_idoso;

            await _context.SaveChangesAsync();

            return existeAlimentacao;
        }
    }
}
