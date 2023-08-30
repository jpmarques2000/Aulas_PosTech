using FiapStore.Dto;
using FiapStore.Enums;

namespace FiapStore.Entity
{
    public class Usuario : Entidade
    {
        public string Nome { get; set; }
        public string NomeUsuario { get; set; }
        public string Senha { get; set; }
        public TipoPermissao Permissao { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }

        public Usuario()
        {
            
        }

        public Usuario(CadastrarUsuarioDto cadastrarUsuarioDto)
        {
            Nome = cadastrarUsuarioDto.Nome;
            NomeUsuario = cadastrarUsuarioDto.NomeUsuario;
            Senha = cadastrarUsuarioDto.Senha;
            Permissao = cadastrarUsuarioDto.Permissao;

        }

        public Usuario(AlterarUsuarioDto alterarUsuarioDto)
        {
            Nome = alterarUsuarioDto.Nome;
        }
    }
}
