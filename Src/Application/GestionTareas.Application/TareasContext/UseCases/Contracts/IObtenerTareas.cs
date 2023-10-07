using GestionTareas.Domain.TareasContext;

namespace GestionTareas.Application.TareasContext.UseCases.Contracts
{
    public interface IObtenerTareas
    {
        Task<IEnumerable<Tarea>> ExecuteAsync();
    }
}