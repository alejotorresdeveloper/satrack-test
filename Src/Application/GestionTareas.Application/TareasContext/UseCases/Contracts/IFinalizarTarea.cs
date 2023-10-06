using GestionTareas.Domain.TareasContext;

namespace GestionTareas.Application.TareasContext.UseCases.Contracts
{
    public interface IFinalizarTarea
    {
        Task<bool> ExecuteAsync(Guid tareaId);
    }
}