using GestionTareas.Application.TareasContext.UseCases.Commands;
using GestionTareas.Domain.TareasContext;

namespace GestionTareas.Application.TareasContext.UseCases.Contracts
{
    public interface ICrearTarea
    {
        Task<Tarea> ExecuteAsync(NuevaTareaCommand nuevaTarea);
    }
}