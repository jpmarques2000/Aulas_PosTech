using FiapStore.Dto;

namespace FiapStore.Entity
{
    public class Usuario : Entidade
    {
        public string Nome { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }

        public Usuario()
        {
            
        }

        public Usuario(CadastrarUsuarioDto cadastrarUsuarioDto)
        {
            Nome = cadastrarUsuarioDto.Nome;
        }

        public Usuario(AlterarUsuarioDto alterarUsuarioDto)
        {
            Nome = alterarUsuarioDto.Nome;
        }
    }
}
