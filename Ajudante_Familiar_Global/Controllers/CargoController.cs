using Ajudante_Familiar_Global.Domain.Entities;
using Ajudante_Familiar_Global.Domain.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;

namespace Ajudante_Familiar_Global.Controllers
{
    [ApiController]
    [Route("api")]
    [Produces("application/json")]
    public class CargoController : ControllerBase
    {
        private readonly ICargoService _CargoService;
        private readonly ILogger<CargoController> _logger;

        public CargoController(ICargoService cargoService, ILogger<CargoController> logger)
        {
            _CargoService = cargoService;
            _logger = logger;
        }

        /// <summary>
        /// Carregar todos os cargos presentes no banco
        /// </summary>
        /// <response code="200">Busca feita com sucesso</response>
        /// <response code="500">Erro interno do servidor</response>
        /// <returns>Lista de cargos</returns>
        [HttpGet]
        [Route("[controller]/cargo")]
        [ProducesResponseType(typeof(IEnumerable<Cargo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Cargo>>> GetAllCargo()
        {
            try
            {
                var relatorioCargo = await _CargoService.GetAllCargoAsync();
                return Ok(relatorioCargo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todos os cargos");
                return StatusCode(500, new { mensagem = "Erro interno do servidor" });
            }
        }

        /// <summary>
        /// Carregar o cargo por meio do id
        /// </summary>
        /// <response code="200">Busca feita com sucesso</response>
        /// <response code="404">Id não encontrado</response>
        /// <param name="id_cargo">Id do cargo</param>
        /// <returns>Cargo encontrado</returns>
        [HttpGet]
        [Route("[controller]/cargo/{id_cargo:int}")]
        [ProducesResponseType(typeof(Cargo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Cargo>> GetCargo(int id_cargo)
        {
            try
            {
                var cargo = await _CargoService.GetCargoAsync(id_cargo);
                return Ok(cargo);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("Cargo {Id} não encontrado", id_cargo);
                return NotFound(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Criação de cargo
        /// </summary>
        /// <param name="cargo">Dados do novo cargo</param>
        /// <response code="201">Cargo criado com sucesso</response>
        /// <response code="400">Erro na validação</response>
        /// <returns>Cargo criado</returns>
        [HttpPost]
        [Route("[controller]/cargo/criar")]
        [ProducesResponseType(typeof(Cargo), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Cargo>> PostCargo([FromBody] Cargo cargo)
        {
            var criada = await _CargoService.CreateCargoAsync(cargo);
            return CreatedAtAction(nameof(GetCargo), new { id_cargo = criada.Id_cargo }, criada);
        }

        /// <summary>
        /// Atualizar dados do cargo
        /// </summary>
        /// <param name="id_cargo">Id do cargo na URL</param>
        /// <param name="cargo">Novos dados do cargo</param>
        /// <response code="200">Cargo atualizado</response>
        /// <response code="400">Erro na requisição</response>
        /// <response code="404">Cargo não encontrado</response>
        /// <returns>Cargo atualizado</returns>
        [HttpPut]
        [Route("[controller]/cargo/atualiza/{id_cargo:int}")]
        [ProducesResponseType(typeof(Cargo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Cargo>> PutCargo(int id_cargo, [FromBody] Cargo cargo)
        {
            try
            {
                var atualizada = await _CargoService.UpdateCargoAsync(id_cargo, cargo);
                return Ok(atualizada);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Remove um cargo do sistema pelo seu id
        /// </summary>
        /// <param name="id_cargo">Id do cargo a ser deletado</param>
        /// <response code="204">Cargo removido com sucesso</response>
        /// <response code="404">Cargo não encontrado</response>
        [HttpDelete]
        [Route("[controller]/cargo/deleta/{id_cargo:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCargo(int id_cargo)
        {
            try
            {
                await _CargoService.DeleteCargoAsync(id_cargo);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }
    }
}
