using GestionTareas.Application.DomainServices.Contracts;
using GestionTareas.Application.TareasContext.Repositories;
using GestionTareas.Domain.TareasContext;

namespace GestionTareas.Application.DomainServices
{
    public class CategoriaDomainService : ICategoriaDomainService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaDomainService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<Categoria> ObtenerCategoria(Guid categoriaId)
        {
            return await _categoriaRepository.GetAsync(categoriaId)
                ?? throw new TareasContextException(TareasContextExceptionEnum.LaCategoriaNoExiste);
        }
    }
}