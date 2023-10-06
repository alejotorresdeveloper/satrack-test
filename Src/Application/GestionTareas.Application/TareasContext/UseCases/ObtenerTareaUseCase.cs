using GestionTareas.Application.TareasContext.Repositories;
using GestionTareas.Application.TareasContext.UseCases.Contracts;
using GestionTareas.Domain.TareasContext;

namespace GestionTareas.Application.TareasContext.UseCases
{
    public class ObtenerTareaUseCase : IObtenerTarea
    {
        private readonly ITareaRepository _tareaRepository;

        public ObtenerTareaUseCase(ITareaRepository tareaRepository)
        {
            _tareaRepository = tareaRepository;
        }

        public async Task<Tarea> ExecuteAsync(Guid tareaId)
        {
            return await _tareaRepository.GetAsync(tareaId)
                ?? throw new TareasContextException(TareasContextExceptionEnum.LaTareaNoExiste);
        }
    }
}