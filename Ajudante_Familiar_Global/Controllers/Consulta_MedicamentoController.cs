using Ajudante_Familiar_Global.Domain.Entities;
using Ajudante_Familiar_Global.Domain.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;

namespace Ajudante_Familiar_Global.Controllers
{
    [ApiController]
    [Route("api")]
    [Produces("application/json")]
    public class Consulta_MedicamentoController : ControllerBase
    {
        private readonly IConsulta_MedicamentoService _Consulta_MedicamentoService;
        private readonly ILogger<Consulta_MedicamentoController> _logger;

        public Consulta_MedicamentoController(IConsulta_MedicamentoService consulta_MedicamentoService, ILogger<Consulta_MedicamentoController> logger)
        {
            _Consulta_MedicamentoService = consulta_MedicamentoService;
            _logger = logger;
        }

        /// <summary>
        /// Carregar todas as relações consulta-medicamento presentes no banco
        /// </summary>
        /// <response code="200">Busca feita com sucesso</response>
        /// <response code="500">Erro interno do servidor</response>
        /// <returns>Lista de relações consulta-medicamento</returns>
        [HttpGet]
        [Route("[controller]/consulta_medicamento")]
        [ProducesResponseType(typeof(IEnumerable<Consulta_Medicamento>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Consulta_Medicamento>>> GetAllConsulta_Medicamento()
        {
            try
            {
                var relatorioConsulta_Medicamento = await _Consulta_MedicamentoService.GetAllConsulta_MedicamentoAsync();
                return Ok(relatorioConsulta_Medicamento);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todas as relações consulta-medicamento");
                return StatusCode(500, new { mensagem = "Erro interno do servidor" });
            }
        }

        /// <summary>
        /// Carregar a relação consulta-medicamento por meio do id
        /// </summary>
        /// <response code="200">Busca feita com sucesso</response>
        /// <response code="404">Id não encontrado</response>
        /// <param name="id_consulta_medicamento">Id da relação</param>
        /// <returns>Relação encontrada</returns>
        [HttpGet]
        [Route("[controller]/consulta_medicamento/{id_consulta_medicamento:int}")]
        [ProducesResponseType(typeof(Consulta_Medicamento), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Consulta_Medicamento>> GetConsulta_Medicamento(int id_consulta_medicamento)
        {
            try
            {
                var consulta_Medicamento = await _Consulta_MedicamentoService.GetConsulta_MedicamentoAsync(id_consulta_medicamento);
                return Ok(consulta_Medicamento);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("Relação consulta-medicamento {Id} não encontrada", id_consulta_medicamento);
                return NotFound(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Criação de relação entre consulta e medicamento
        /// </summary>
        /// <param name="consulta_Medicamento">Dados da nova relação</param>
        /// <response code="201">Relação criada com sucesso</response>
        /// <response code="400">Erro na validação</response>
        /// <response code="404">Consulta ou medicamento não encontrado</response>
        /// <returns>Relação criada</returns>
        [HttpPost]
        [Route("[controller]/consulta_medicamento/criar")]
        [ProducesResponseType(typeof(Consulta_Medicamento), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Consulta_Medicamento>> PostConsulta_Medicamento([FromBody] Consulta_Medicamento consulta_Medicamento)
        {
            try
            {
                var criada = await _Consulta_MedicamentoService.CreateConsulta_MedicamentoAsync(consulta_Medicamento);
                return CreatedAtAction(nameof(GetConsulta_Medicamento), new { id_consulta_medicamento = criada.id_consulta_medicamento }, criada);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Atualizar a relação entre consulta e medicamento
        /// </summary>
        /// <param name="id_consulta_medicamento">Id da relação na URL</param>
        /// <param name="consulta_Medicamento">Novos dados da relação</param>
        /// <response code="200">Relação atualizada</response>
        /// <response code="400">Erro na requisição</response>
        /// <response code="404">Relação, consulta ou medicamento não encontrado</response>
        /// <returns>Relação atualizada</returns>
        [HttpPut]
        [Route("[controller]/consulta_medicamento/atualiza/{id_consulta_medicamento:int}")]
        [ProducesResponseType(typeof(Consulta_Medicamento), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Consulta_Medicamento>> PutConsulta_Medicamento(int id_consulta_medicamento, [FromBody] Consulta_Medicamento consulta_Medicamento)
        {
            try
            {
                var atualizada = await _Consulta_MedicamentoService.UpdateConsulta_MedicamentoAsync(id_consulta_medicamento, consulta_Medicamento);
                return Ok(atualizada);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Remove uma relação consulta-medicamento pelo seu id
        /// </summary>
        /// <param name="id_consulta_medicamento">Id da relação a ser deletada</param>
        /// <response code="204">Relação removida com sucesso</response>
        /// <response code="404">Relação não encontrada</response>
        [HttpDelete]
        [Route("[controller]/consulta_medicamento/deleta/{id_consulta_medicamento:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteConsulta_Medicamento(int id_consulta_medicamento)
        {
            try
            {
                await _Consulta_MedicamentoService.DeleteConsulta_MedicamentoAsync(id_consulta_medicamento);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }
    }
}
