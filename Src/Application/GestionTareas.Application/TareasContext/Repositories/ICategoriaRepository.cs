using GestionTareas.Domain.TareasContext;
using System.Linq.Expressions;

namespace GestionTareas.Application.TareasContext.Repositories
{
    public interface ICategoriaRepository
    {
        Task<Categoria> AddAsync(Categoria categoria);

        Task<bool> UpdateAsync(Categoria categoria);

        Task<Categoria> GetAsync(Guid categoriaId);

        Task<Categoria> GetAsync(Expression<Func<Categoria, bool>> predicate);

        Task<ICollection<Categoria>> GetAllAsync(Expression<Func<Categoria, bool>> predicate, int offset = 0, int limit = 10);
    }
}