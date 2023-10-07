using GestionTareas.Domain.TareasContext;

namespace GestionTareas.Application.TareasContext.UseCases.Contracts
{
    public interface IObtenerCategorias
    {
        Task<IEnumerable<Categoria>> ExecuteAsync();
    }
}
