using FiapStore.Entity;
using FiapStore.Interface;
using Microsoft.EntityFrameworkCore;

namespace FiapStore.Repository
{
    public class EFRepository<T> : IRepository<T> where T : Entidade
    {
        protected ApplicationDbContext _context;
        protected DbSet<T> _dbset;

        public EFRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        public void Alterar(T entidade)
        {
            // T podendo ser usuario ou pedido
            _dbset.Update(entidade);
            _context.SaveChanges();
        }

        public void Cadastrar(T entidade)
        {
            _dbset.Add(entidade);
            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            _dbset.Remove(ObterPorId(id));
            _context.SaveChanges();
        }

        public T ObterPorId(int id)
        {
            return _dbset.FirstOrDefault(t => t.Id == id);   
        }

        public IList<T> ObterTodos()
        {
            return _dbset.ToList();
        }
    }
}
