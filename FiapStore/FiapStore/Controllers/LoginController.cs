using FiapStore.Dto;
using FiapStore.Interface;
using FiapStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace FiapStore.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenService _tokenService;

        public LoginController(IUsuarioRepository usuarioRepository, ITokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Realizar autenticação no sistema
        /// </summary>
        /// <param name="usuarioDto"></param>
        /// <returns></returns>
        /// <remarks>
        /// Insira nome de usuário e senha 
        /// </remarks>
        /// <response code="200">Retorna Sucesso</response>
        /// <response code="401">Não Autenticado</response>
        /// <response code="403">Não Autorizado | Sem permissão</response>
        [HttpPost]
        public IActionResult Autenticar([FromBody] LoginDto usuarioDto)
        {
            var usuario = _usuarioRepository.ObterPorNomeUsuarioESenha(
                usuarioDto.NomeUsuario, usuarioDto.Senha);
            if(usuario == null)
               return NotFound(new {mensagem = "Usuário ou senha invállidos" });
            var token = _tokenService.GerarToken(usuario);

            usuario.Senha = null;

            return Ok(new
            {
                Usuario = usuario,
                Token = token
            });

        }
    }
}
