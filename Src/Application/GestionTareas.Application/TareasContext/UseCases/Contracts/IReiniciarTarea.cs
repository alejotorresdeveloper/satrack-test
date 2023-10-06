using GestionTareas.Domain.TareasContext;

namespace GestionTareas.Application.TareasContext.UseCases.Contracts
{
    public interface IReiniciarTarea
    {
        Task<bool> ExecuteAsync(Guid tareaId);
    }
}
