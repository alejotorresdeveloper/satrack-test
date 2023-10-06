using GestionTareas.Application.TareasContext.Repositories;
using GestionTareas.Application.TareasContext.UseCases.Contracts;
using GestionTareas.Domain.TareasContext;

namespace GestionTareas.Application.TareasContext.UseCases
{
    public class EliminarTareaUseCase : IEliminarTarea
    {
        private readonly ITareaRepository _tareaRepository;

        public EliminarTareaUseCase(ITareaRepository tareaRepository)
        {
            _tareaRepository = tareaRepository;
        }

        public async Task<bool> ExecuteAsync(Guid tareaId)
        {
            var tareaEnBaseDeDatos = await _tareaRepository.GetAsync(tareaId) ??
                throw new TareasContextException(TareasContextExceptionEnum.LaTareaNoExiste);

            return await _tareaRepository.DeleteAsync(tareaEnBaseDeDatos);
        }
    }
}