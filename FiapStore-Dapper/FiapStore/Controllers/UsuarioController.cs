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

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet("obter-todos-com-pedidos/{id}")]

        public IActionResult ObterTodosComPedidos(int id)
        {
            return Ok(_usuarioRepository.ObterComPedidos(id));
        }

        [HttpGet("obter-usuario-usuario")]
        public IActionResult ObterTodosUsuario()
        {
            return Ok(_usuarioRepository.ObterTodos());
        }

        [HttpGet("obter-por-usuario-id/{id}")]
        public IActionResult ObterPorUsuarioId(int id)
        {
            return Ok(_usuarioRepository.ObterPorId(id));
        }

        [HttpPost]
        public IActionResult CadastrarUsuario(CadastrarUsuarioDto usuarioDto)
        {
            _usuarioRepository.Cadastrar(new Usuario(usuarioDto));
            return Ok("Usuário criado com sucesso!");
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
