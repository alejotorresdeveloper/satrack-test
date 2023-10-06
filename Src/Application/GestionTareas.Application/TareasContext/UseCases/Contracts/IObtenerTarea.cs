using GestionTareas.Domain.TareasContext;

namespace GestionTareas.Application.TareasContext.UseCases.Contracts
{
    public interface IObtenerTarea
    {
        Task<Tarea> ExecuteAsync(Guid tareaId);
    }
}