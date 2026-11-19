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
    public class DoencasController : ControllerBase
    {
        private readonly IDoencasService _DoencasService;
        private readonly ILogger<DoencasController> _logger;

        public DoencasController(IDoencasService doencasService, ILogger<DoencasController> logger)
        {
            _DoencasService = doencasService;
            _logger = logger;
        }

        /// <summary>
        /// Carregar todas as doenças presentes no banco
        /// </summary>
        /// <response code="200">Busca feita com sucesso</response>
        /// <response code="500">Erro interno do servidor</response>
        /// <returns>Lista de doenças</returns>
        [HttpGet]
        [Route("[controller]/doencas")]
        [ProducesResponseType(typeof(IEnumerable<Doencas>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Doencas>>> GetAllDoencas()
        {
            try
            {
                var relatorioDoencas = await _DoencasService.GetAllDoencasAsync();
                return Ok(relatorioDoencas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todas as doenças");
                return StatusCode(500, new { mensagem = "Erro interno do servidor" });
            }
        }

        /// <summary>
        /// Carregar a doença por meio do id
        /// </summary>
        /// <response code="200">Busca feita com sucesso</response>
        /// <response code="404">Id não encontrado</response>
        /// <param name="id_doenca">Id da doença</param>
        /// <returns>Doença encontrada</returns>
        [HttpGet]
        [Route("[controller]/doencas/{id_doenca:int}")]
        [ProducesResponseType(typeof(Doencas), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Doencas>> GetDoencas(int id_doenca)
        {
            try
            {
                var doencas = await _DoencasService.GetDoencasAsync(id_doenca);
                return Ok(doencas);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("Doença {Id} não encontrada", id_doenca);
                return NotFound(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Criação de doença
        /// </summary>
        /// <param name="doencas">Dados da nova doença</param>
        /// <response code="201">Doença criada com sucesso</response>
        /// <response code="400">Erro na validação</response>
        /// <response code="404">Idoso não encontrado</response>
        /// <returns>Doença criada</returns>
        [HttpPost]
        [Idempotent]
        [Route("[controller]/doencas/criar")]
        [ProducesResponseType(typeof(Doencas), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Doencas>> PostDoencas([FromBody] Doencas doencas)
        {
            try
            {
                var criada = await _DoencasService.CreateDoencasAsync(doencas);
                return CreatedAtAction(nameof(GetDoencas), new { id_doenca = criada.id_doenca }, criada);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Atualizar dados da doença
        /// </summary>
        /// <param name="id_doenca">Id da doença na URL</param>
        /// <param name="doencas">Novos dados da doença</param>
        /// <response code="200">Doença atualizada</response>
        /// <response code="400">Erro na requisição</response>
        /// <response code="404">Doença ou idoso não encontrado</response>
        /// <returns>Doença atualizada</returns>
        [HttpPut]
        [Route("[controller]/doencas/atualiza/{id_doenca:int}")]
        [ProducesResponseType(typeof(Doencas), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Doencas>> PutDoencas(int id_doenca, [FromBody] Doencas doencas)
        {
            try
            {
                var atualizada = await _DoencasService.UpdateDoencasAsync(id_doenca, doencas);
                return Ok(atualizada);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Remove uma doença do sistema pelo seu id
        /// </summary>
        /// <param name="id_doenca">Id da doença a ser deletada</param>
        /// <response code="204">Doença removida com sucesso</response>
        /// <response code="404">Doença não encontrada</response>
        [HttpDelete]
        [Route("[controller]/doencas/deleta/{id_doenca:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDoencas(int id_doenca)
        {
            try
            {
                await _DoencasService.DeleteDoencasAsync(id_doenca);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }
    }
}
