using GestionTareas.Application.TareasContext.Repositories;
using GestionTareas.Application.TareasContext.UseCases.Contracts;
using GestionTareas.Domain.SharedKernel;
using GestionTareas.Domain.TareasContext;

namespace GestionTareas.Application.TareasContext.UseCases
{
    public class ObtenerTareasUseCase : IObtenerTareas
    {
        private readonly ITareaRepository _tareaRepository;

        public ObtenerTareasUseCase(ITareaRepository tareaRepository)
        {
            _tareaRepository = tareaRepository;
        }

        public async Task<IEnumerable<Tarea>> ExecuteAsync()
        {
            return await _tareaRepository.GetAllAsync(t => t.Estado.Equals(EstadoEnum.Activo))
                ?? throw new TareasContextException(TareasContextExceptionEnum.NoSeEncontraronTareas);
        }
    }
}