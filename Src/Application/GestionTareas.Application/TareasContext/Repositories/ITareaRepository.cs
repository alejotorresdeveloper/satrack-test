using GestionTareas.Domain.TareasContext;
using System.Linq.Expressions;

namespace GestionTareas.Application.TareasContext.Repositories
{
    public interface ITareaRepository
    {
        Task<Tarea> AddAsync(Tarea tarea);

        Task<bool> UpdateAsync(Tarea tarea);

        Task<bool> DeleteAsync(Tarea tarea);

        Task<Tarea> GetAsync(Guid tareaId);

        Task<Tarea> GetAsync(Expression<Func<Tarea, bool>> predicate);

        Task<ICollection<Tarea>> GetAllAsync(Expression<Func<Tarea, bool>> predicate, int offset = 0, int limit = 10);
    }
}