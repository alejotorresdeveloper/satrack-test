using GestionTareas.Application.TareasContext.UseCases.Commands;

namespace GestionTareas.Application.TareasContext.UseCases.Contracts
{
    public interface IModificarTarea
    {
        Task<bool> ExecuteAsync(ModificarTareaCommand modificarTarea);
    }
}