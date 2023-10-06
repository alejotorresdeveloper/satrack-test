using GestionTareas.Domain.TareasContext;

namespace GestionTareas.Application.TareasContext.UseCases.Contracts
{
    public interface IIniciarTarea
    {
        Task<bool> ExecuteAsync(Guid tareaId);
    }
}