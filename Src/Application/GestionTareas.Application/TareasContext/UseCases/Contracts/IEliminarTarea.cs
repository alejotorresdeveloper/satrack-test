namespace GestionTareas.Application.TareasContext.UseCases.Contracts
{
    public interface IEliminarTarea
    {
        Task<bool> ExecuteAsync(Guid tareaId);
    }
}