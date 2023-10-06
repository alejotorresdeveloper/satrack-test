using GestionTareas.Domain.TareasContext;

namespace GestionTareas.Application.DomainServices.Contracts
{
    public interface ICategoriaDomainService
    {
        Task<Categoria> ObtenerCategoria(Guid categoriaId);
    }
}