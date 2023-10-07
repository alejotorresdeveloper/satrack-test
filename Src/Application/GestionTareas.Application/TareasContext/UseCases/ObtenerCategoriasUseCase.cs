using GestionTareas.Application.TareasContext.Repositories;
using GestionTareas.Application.TareasContext.UseCases.Contracts;
using GestionTareas.Domain.SharedKernel;
using GestionTareas.Domain.TareasContext;

namespace GestionTareas.Application.TareasContext.UseCases
{
    public class ObtenerCategoriasUseCase : IObtenerCategorias
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public ObtenerCategoriasUseCase(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<Categoria>> ExecuteAsync()
        {
            return await _categoriaRepository.GetAllAsync(t => t.Estado.Equals(EstadoEnum.Activo))
                ?? throw new TareasContextException(TareasContextExceptionEnum.NoSeEncontraronTareas);
        }
    }
}
