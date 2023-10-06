using GestionTareas.Application.TareasContext.Repositories;
using GestionTareas.Application.TareasContext.UseCases.Contracts;
using GestionTareas.Domain.TareasContext;

namespace GestionTareas.Application.TareasContext.UseCases
{
    public class ObtenerTareasPorCategoriaUseCase : IObtenerTareasPorCategoria
    {
        private readonly ITareaRepository _tareaRepository;

        public ObtenerTareasPorCategoriaUseCase(ITareaRepository tareaRepository)
        {
            _tareaRepository = tareaRepository;
        }

        public async Task<IEnumerable<Tarea>> ExecuteAsync(Guid? categoriaId)
        {
            return await _tareaRepository.GetAllAsync(t => categoriaId.Equals(Guid.Empty) || categoriaId == null || t.Categoria.Id.Equals(categoriaId))
                ?? throw new TareasContextException(TareasContextExceptionEnum.NoSeEncontraronTareas);
        }
    }
}