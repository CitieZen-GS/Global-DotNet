using Ajudante_Familiar_Global.Domain.Entities;
using Ajudante_Familiar_Global.Domain.Interfaces.IService;
using IdempotentAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Ajudante_Familiar_Global.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    [Produces("application/json")]
    [EnableRateLimiting("fixed")]
    public class AtividadesController : ControllerBase
    {
        private readonly IAtividadesService _AtividadesService;
        private readonly ILogger<AtividadesController> _logger;

        public AtividadesController(IAtividadesService atividadesService, ILogger<AtividadesController> logger)
        {
            _AtividadesService = atividadesService;
            _logger = logger;
        }

        /// <summary>
        /// Carregar todas as atividades presentes no banco
        /// </summary>
        /// <response code="200">Busca feita com sucesso</response>
        /// <response code="500">Erro interno do servidor</response>
        /// <returns>Lista de atividades</returns>
        [HttpGet]
        [Route("[controller]/atividades")]
        [ProducesResponseType(typeof(IEnumerable<Atividades>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Atividades>>> GetAllAtividades()
        {
            try
            {
                var relatorioAtividades = await _AtividadesService.GetAllAtividadesAsync();
                return Ok(relatorioAtividades);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todas as atividades");
                return StatusCode(500, new { mensagem = "Erro interno do servidor" });
            }
        }

        /// <summary>
        /// Carregar a atividade por meio do id
        /// </summary>
        /// <response code="200">Busca feita com sucesso</response>
        /// <response code="404">Id não encontrado</response>
        /// <param name="id_atividade">Id da atividade</param>
        /// <returns>Atividade encontrada</returns>
        [HttpGet]
        [Route("[controller]/atividades/{id_atividade:int}")]
        [ProducesResponseType(typeof(Atividades), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Atividades>> GetAtividades(int id_atividade)
        {
            try
            {
                var atividades = await _AtividadesService.GetAtividadesAsync(id_atividade);
                return Ok(atividades);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("Atividade {Id} não encontrada", id_atividade);
                return NotFound(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Criação de atividade
        /// </summary>
        /// <param name="atividades">Dados da nova atividade</param>
        /// <response code="201">Atividade criada com sucesso</response>
        /// <response code="400">Erro na validação</response>
        /// <response code="404">Idoso não encontrado</response>
        /// <returns>Atividade criada</returns>
        [HttpPost]
        [Route("[controller]/atividades/criar")]
        [ProducesResponseType(typeof(Atividades), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Atividades>> PostAtividades([FromBody] Atividades atividades)
        {
            try
            {
                var criada = await _AtividadesService.CreateAtividadesAsync(atividades);
                return CreatedAtAction(nameof(GetAtividades), new { id_atividade = criada.Id_atividade }, criada);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Atualizar dados da atividade
        /// </summary>
        /// <param name="id_atividade">Id da atividade na URL</param>
        /// <param name="atividades">Novos dados da atividade</param>
        /// <response code="200">Atividade atualizada</response>
        /// <response code="400">Erro na requisição</response>
        /// <response code="404">Atividade ou idoso não encontrado</response>
        /// <returns>Atividade atualizada</returns>
        [HttpPut]
        [Route("[controller]/atividades/atualiza/{id_atividade:int}")]
        [ProducesResponseType(typeof(Atividades), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Atividades>> PutAtividades(int id_atividade, [FromBody] Atividades atividades)
        {
            try
            {
                var atualizada = await _AtividadesService.UpdateAtividadesAsync(id_atividade, atividades);
                return Ok(atualizada);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Remove uma atividade do sistema pelo seu id
        /// </summary>
        /// <param name="id_atividade">Id da atividade a ser deletada</param>
        /// <response code="204">Atividade removida com sucesso</response>
        /// <response code="404">Atividade não encontrada</response>
        [HttpDelete]
        [Route("[controller]/atividades/deleta/{id_atividade:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAtividades(int id_atividade)
        {
            try
            {
                await _AtividadesService.DeleteAtividadesAsync(id_atividade);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }
    }
}
