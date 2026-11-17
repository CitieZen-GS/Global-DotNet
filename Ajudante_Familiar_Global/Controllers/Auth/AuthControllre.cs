using Ajudante_Familiar_Global.Application.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ajudante_Familiar_Global.Controllers.Auth
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public AuthController(TokenService token)
        {
            _tokenService = token;
        }

        /// <summary>
        /// Realiza o login e gera o token JWT
        /// </summary>
        /// <param name="user">Usuário e senha</param>
        /// <response code="200">Token gerado com sucesso</response>
        /// <response code="401">Usuário ou senha inválidos</response>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Login([FromBody] UserLoginAux user)
        {
            if (user.Username == "admin" && user.Password == "123456")
            {
                var token = _tokenService.GenerateToken(user.Username);
                return Ok(new { token });
            }
            return Unauthorized(new { mensagem = "Usuário ou senha inválidos" });
        }

        /// <summary>
        /// Rota protegida: só acessa com token válido
        /// </summary>
        /// <response code="200">Usuário autenticado</response>
        /// <response code="401">Token ausente ou inválido</response>
        [HttpGet("dados-protegidos")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetDadosProtegidos()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok($"Usuario: {username}, está autenticado");
        }

        public class UserLoginAux
        {
            public string? Username { get; set; }
            public string? Password { get; set; }
        }
    }
}