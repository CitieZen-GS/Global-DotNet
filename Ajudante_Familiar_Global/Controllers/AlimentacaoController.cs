using Ajudante_Familiar_Global.Domain.Entities;
using Ajudante_Familiar_Global.Domain.Interfaces.IService;
using Ajudante_Familiar_Global.Infrastruture.Data;
using Microsoft.AspNetCore.Mvc;

namespace Ajudante_Familiar_Global.Controllers
{
    [ApiController]
    [Route("api")]
    [Produces("application/json")]
    public class AlimentacaoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAlimentacaoService _AlimentacaoService;
        private readonly ILogger<AlimentacaoController> _logger;

        public AlimentacaoController(AppDbContext context,
            IAlimentacaoService alimentacaoService, 
            ILogger<AlimentacaoController> logger)
        {
            _context = context;
            _AlimentacaoService = alimentacaoService;
            _logger = logger;
        }

        /// <summary>
        /// Carregar todas as alimentações presentes no banco
        /// </summary>
        /// <response code="200">Busca feita com sucesso</response>
        /// <response code="500">Erro interno do servidor</response>
        /// <returns>Lista de alimentações</returns>
        [HttpGet]
        [Route("[controller]/alimentacao")]
        [ProducesResponseType(typeof(IEnumerable<Alimentacao>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Alimentacao>>> GetAllAlimentacao()
        {
            try
            {
                var relatorioAlimentacao = await _AlimentacaoService.GetAllAlimentacaoAsync();
                return Ok(relatorioAlimentacao);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todas as alimentações");
                return StatusCode(500, new { mensagem = "Erro interno do servidor" });
            }
        }

        /// <summary>
        /// Carregar a alimentação por meio do id
        /// </summary>
        /// <response code="200">Busca feita com sucesso</response>
        /// <response code="404">Id não encontrado</response>
        /// <param name="id_alimentacao">Id da alimentação</param>
        /// <returns>Alimentação encontrada</returns>
        [HttpGet]
        [Route("[controller]/alimentacao/{id_alimentacao:int}")]
        [ProducesResponseType(typeof(Alimentacao), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Alimentacao>> GetAlimentacao(int id_alimentacao)
        {
            try
            {
                var alimentacao = await _AlimentacaoService.GetAlimentacaoAsync(id_alimentacao);
                return Ok(alimentacao);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("Alimentação {Id} não encontrada", id_alimentacao);
                return NotFound(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Criação de alimentação
        /// </summary>
        /// <param name="alimentacao">Dados da nova alimentação</param>
        /// <response code="201">Alimentação criada com sucesso</response>
        /// <response code="400">Erro na validação</response>
        /// <response code="404">Idoso não encontrado</response>
        /// <returns>Alimentação criada</returns>
        [HttpPost]
        [Route("[controller]/alimentacao/criar")]
        [ProducesResponseType(typeof(Alimentacao), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Alimentacao>> PostAlimentacao([FromBody] Alimentacao alimentacao)
        {
            try
            {
                var criada = await _AlimentacaoService.CreateAlimentacaoAsync(alimentacao);

                return CreatedAtAction(
                    nameof(GetAlimentacao),
                    new { id_alimentacao = criada.Id_alimentacao },
                    criada
                );
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Atualizar dados de alimentação
        /// </summary>
        /// <param name="id_alimentacao">Id da alimentação na URL</param>
        /// <param name="alimentacao">Novos dados da alimentação</param>
        /// <response code="200">Alimentação atualizada</response>
        /// <response code="400">Erro na requisição</response>
        /// <response code="404">Alimentação ou idoso não encontrado</response>
        /// <returns>Alimentação atualizada</returns>
        [HttpPut]
        [Route("[controller]/alimentacao/atualiza/{id_alimentacao:int}")]
        [ProducesResponseType(typeof(Alimentacao), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Alimentacao>> PutAlimentacao(int id_alimentacao, [FromBody] Alimentacao alimentacao)
        {
            try
            {
                var atualizada = await _AlimentacaoService.UpdateAlimentacaoAsync(id_alimentacao, alimentacao);
                return Ok(atualizada);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Remove uma alimentação do sistema pelo seu id
        /// </summary>
        /// <param name="id_alimentacao">Id da alimentação a ser deletada</param>
        /// <response code="204">Alimentação removida com sucesso</response>
        /// <response code="404">Alimentação não encontrada</response>
        [HttpDelete]
        [Route("[controller]/alimentacao/deleta/{id_alimentacao:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAlimentacao(int id_alimentacao)
        {
            try
            {
                await _AlimentacaoService.DeleteAlimentacaoAsync(id_alimentacao);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }
    }
}