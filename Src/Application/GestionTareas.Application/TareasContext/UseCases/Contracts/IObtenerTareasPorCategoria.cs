using GestionTareas.Domain.TareasContext;

namespace GestionTareas.Application.TareasContext.UseCases.Contracts
{
    public interface IObtenerTareasPorCategoria
    {
        Task<IEnumerable<Tarea>> ExecuteAsync(Guid? categoriaId);
    }
}