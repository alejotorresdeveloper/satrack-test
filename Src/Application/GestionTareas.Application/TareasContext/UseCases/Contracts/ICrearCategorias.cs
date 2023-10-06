using GestionTareas.Domain.TareasContext;

namespace GestionTareas.Application.TareasContext.UseCases.Contracts
{
    public interface ICrearCategorias
    {
        Task<bool> ExecuteAsync();
    }
}