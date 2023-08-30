using FiapStore.Entity;
using FiapStore.Interface;
using Microsoft.EntityFrameworkCore;

namespace FiapStore.Repository
{
    public class EFUsuarioRepository : EFRepository<Usuario>, IUsuarioRepository
    {
        public EFUsuarioRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Usuario ObterComPedidos(int id)
        {
            return _context.Usuario
                .Include(u => u.Pedidos)
                .Where(u => u.Id == id)
                .ToList()
                .Select(usuario =>
                {
                    usuario.Pedidos = usuario.Pedidos.Select(pedido => new Pedido(pedido)).ToList();
                    return usuario;
                }).FirstOrDefault();
        }

        //Apenas para teste local, recomendado realizar login com criptografia, serviços de secrets.
        //Não realizar comparação direta com a senha
        public Usuario ObterPorNomeUsuarioESenha(string nomeUsuario, string senha)
        {
            return _context.Usuario.FirstOrDefault(usuario => 
                usuario.NomeUsuario == nomeUsuario && usuario.Senha == senha);
        }
    }
}
