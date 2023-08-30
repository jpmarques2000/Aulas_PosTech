using FiapStore.Dto;
using FiapStore.Entity;
using FiapStore.Enums;
using FiapStore.Interface;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Obtém todos os usuários com pedido
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Exemplo:
        /// 
        /// Enviar Id para requisição
        /// </remarks>
        /// <response code="200">Retorna Sucesso</response>
        /// <response code="401">Não Autenticado</response>
        /// <response code="403">Não Autorizado | Sem permissão</response>
        [Authorize]
        [HttpGet("obter-todos-com-pedidos/{id}")]
        public IActionResult ObterTodosComPedidos(int id)
        {
            return Ok(_usuarioRepository.ObterComPedidos(id));
        }

        /// <summary>
        /// Obtém todos os usuários cadastrados no sistema
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Retorna Sucesso</response>
        /// <response code="401">Não Autenticado</response>
        /// <response code="403">Não Autorizado | Sem permissão</response>
        [Authorize]
        [Authorize(Roles = $"{Permissoes.Administrador}, {Permissoes.Funcionario}")]
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

        /// <summary>
        /// Obtém usuário cadastrado por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Enviar Id do usuário para realizar a requisição
        /// </remarks>
        /// <response code="200">Retorna Sucesso</response>
        /// <response code="401">Não Autenticado</response>
        /// <response code="403">Não Autorizado | Sem permissão</response>
        [Authorize]
        [Authorize(Roles = Permissoes.Administrador)]
        [HttpGet("obter-por-usuario-id/{id}")]
        public IActionResult ObterPorUsuarioId(int id)
        {
            _logger.LogInformation("Executando método ObterPorUsuarioId");
            return Ok(_usuarioRepository.ObterPorId(id));
        }

        /// <summary>
        /// Realizar o cadastro de um novo usuário
        /// </summary>
        /// <param name="usuarioDto"></param>
        /// <returns></returns>
        /// <remarks>
        /// Dados:
        /// 
        /// Nome, Nome de usuário, senha e nível de permissão
        /// </remarks>
        /// <response code="200">Retorna Sucesso</response>
        /// <response code="401">Não Autenticado</response>
        /// <response code="403">Não Autorizado | Sem permissão</response>
        [Authorize]
        [Authorize(Roles = $"{Permissoes.Administrador}, {Permissoes.Funcionario}")]
        [HttpPost]
        public IActionResult CadastrarUsuario(CadastrarUsuarioDto usuarioDto)
        {
            _usuarioRepository.Cadastrar(new Usuario(usuarioDto));
            var mensagem = $"Usuário criado com sucesso! | Nome: {usuarioDto.Nome}";
            _logger.LogWarning(mensagem);
            return Ok(mensagem);
        }

        /// <summary>
        /// Alterar dados de usuário existente
        /// </summary>
        /// <param name="usuarioDto"></param>
        /// <returns></returns>
        /// <remarks>
        /// Dados:
        /// 
        /// Id do usuário a ser alterado e novo nome
        /// </remarks>
        /// <response code="200">Retorna Sucesso</response>
        /// <response code="401">Não Autenticado</response>
        /// <response code="403">Não Autorizado | Sem permissão</response>
        [HttpPut]
        public IActionResult AlterarUsuario(AlterarUsuarioDto usuarioDto)
        {
            _usuarioRepository.Alterar(new Usuario(usuarioDto));
            return Ok("Usuário alterado com sucesso!");
        }

        /// <summary>
        /// Excluir usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Inserir Id do usuário a ser excluído
        /// </remarks>
        /// <response code="200">Retorna Sucesso</response>
        /// <response code="401">Não Autenticado</response>
        /// <response code="403">Não Autorizado | Sem permissão</response>
        [HttpDelete("{id}")]
        public IActionResult DeletarUsuario(int id)
        {
            _usuarioRepository.Deletar(id);
            return Ok("Usuário deletado com sucesso!");
        }
    }
}
