using Ajudante_Familiar_Global.Domain.Entities;
using Ajudante_Familiar_Global.Domain.Interfaces.IService;
using IdempotentAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace Ajudante_Familiar_Global.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    [Produces("application/json")]
    [EnableRateLimiting("fixed")]
    public class IdosoController : ControllerBase
    {
        private readonly IIdosoService _IdosoService;
        private readonly ILogger<IdosoController> _logger;

        public IdosoController(IIdosoService idosoService, ILogger<IdosoController> logger)
        {
            _IdosoService = idosoService;
            _logger = logger;
        }

        /// <summary>
        /// Carregar todos os idosos presentes no banco
        /// </summary>
        /// <response code="200">Busca feita com sucesso</response>
        /// <response code="500">Erro interno do servidor</response>
        /// <returns>Lista de idosos</returns>
        [HttpGet]
        [Route("[controller]/idoso")]
        [ProducesResponseType(typeof(IEnumerable<Idoso>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Idoso>>> GetAllIdoso()
        {
            try
            {
                var relatorioIdoso = await _IdosoService.GetAllIdosoAsync();
                return Ok(relatorioIdoso);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todos os idosos");
                return StatusCode(500, new { mensagem = "Erro interno do servidor" });
            }
        }

        /// <summary>
        /// Carregar o idoso por meio do id
        /// </summary>
        /// <response code="200">Busca feita com sucesso</response>
        /// <response code="404">Id não encontrado</response>
        /// <param name="id_idoso">Id do idoso</param>
        /// <returns>Idoso encontrado</returns>
        [HttpGet]
        [Route("[controller]/idoso/{id_idoso:int}")]
        [ProducesResponseType(typeof(Idoso), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Idoso>> GetIdoso(int id_idoso)
        {
            try
            {
                var idoso = await _IdosoService.GetIdosoAsync(id_idoso);
                return Ok(idoso);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("Idoso {Id} não encontrado", id_idoso);
                return NotFound(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Criação de idoso
        /// </summary>
        /// <param name="idoso">Dados do novo idoso</param>
        /// <response code="201">Idoso criado com sucesso</response>
        /// <response code="400">Erro na validação</response>
        /// <response code="404">Cuidador não encontrado</response>
        /// <returns>Idoso criado</returns>
        [HttpPost]
        [Route("[controller]/idoso/criar")]
        [ProducesResponseType(typeof(Idoso), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Idoso>> PostIdoso([FromBody] Idoso idoso)
        {
            try
            {
                var criado = await _IdosoService.CreateIdosoAsync(idoso);
                return CreatedAtAction(nameof(GetIdoso), new { id_idoso = criado.ID_idoso }, criado);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erro ao gravar idoso");
                return BadRequest(new { mensagem = "Não foi possível gravar o idoso. Verifique os valores informados (ex.: altura em metros, como 1.70)." });
            }
        }

        /// <summary>
        /// Atualizar dados do idoso
        /// </summary>
        /// <param name="id_idoso">Id do idoso na URL</param>
        /// <param name="idoso">Novos dados do idoso</param>
        /// <response code="200">Idoso atualizado</response>
        /// <response code="400">Erro na requisição</response>
        /// <response code="404">Idoso ou cuidador não encontrado</response>
        /// <returns>Idoso atualizado</returns>
        [HttpPut]
        [Route("[controller]/idoso/atualiza/{id_idoso:int}")]
        [ProducesResponseType(typeof(Idoso), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Idoso>> PutIdoso(int id_idoso, [FromBody] Idoso idoso)
        {
            try
            {
                var atualizada = await _IdosoService.UpdateIdosoAsync(id_idoso, idoso);
                return Ok(atualizada);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Remove um idoso do sistema pelo seu id
        /// </summary>
        /// <param name="id_idoso">Id do idoso a ser deletado</param>
        /// <response code="204">Idoso removido com sucesso</response>
        /// <response code="404">Idoso não encontrado</response>
        [HttpDelete]
        [Route("[controller]/idoso/deleta/{id_idoso:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteIdoso(int id_idoso)
        {
            try
            {
                await _IdosoService.DeleteIdosoAsync(id_idoso);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }
    }
}
