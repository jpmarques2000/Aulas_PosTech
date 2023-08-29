using FiapStore.Dto;
using FiapStore.Entity;
using FiapStore.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FiapStore.Controllers
{
    [ApiController]
    [Route("Usuario")]
    public class UsuarioController: ControllerBase
    {
        private IUsuarioRepository _usuarioRepository;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUsuarioRepository usuarioRepository, 
            ILogger<UsuarioController> logger)
        {
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }

        [HttpGet("obter-todos-com-pedidos/{id}")]

        public IActionResult ObterTodosComPedidos(int id)
        {
            return Ok(_usuarioRepository.ObterComPedidos(id));
        }

        [HttpGet("obter-usuario-usuario")]
        public IActionResult ObterTodosUsuario()
        {
            try
            {
                //throw new Exception("DEU MERDA!");
                return Ok(_usuarioRepository.ObterTodos());
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"{DateTime.Now} | Exception forçada para testes: {ex.Message}");
                return BadRequest();
            }
            
        }

        [HttpGet("obter-por-usuario-id/{id}")]
        public IActionResult ObterPorUsuarioId(int id)
        {
            _logger.LogInformation("Executando método ObterPorUsuarioId");
            return Ok(_usuarioRepository.ObterPorId(id));
        }

        [HttpPost]
        public IActionResult CadastrarUsuario(CadastrarUsuarioDto usuarioDto)
        {
            _usuarioRepository.Cadastrar(new Usuario(usuarioDto));
            var mensagem = $"Usuário criado com sucesso! | Nome: {usuarioDto.Nome}";
            _logger.LogWarning(mensagem);
            return Ok(mensagem);
        }

        [HttpPut]
        public IActionResult AlterarUsuario(AlterarUsuarioDto usuarioDto)
        {
            _usuarioRepository.Alterar(new Usuario(usuarioDto));
            return Ok("Usuário alterado com sucesso!");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarUsuario(int id)
        {
            _usuarioRepository.Deletar(id);
            return Ok("Usuário deletado com sucesso!");
        }
    }
}
