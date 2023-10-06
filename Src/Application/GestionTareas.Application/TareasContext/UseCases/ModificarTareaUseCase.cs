using GestionTareas.Application.DomainServices.Contracts;
using GestionTareas.Application.TareasContext.Repositories;
using GestionTareas.Application.TareasContext.UseCases.Commands;
using GestionTareas.Application.TareasContext.UseCases.Contracts;
using GestionTareas.Domain.TareasContext;

namespace GestionTareas.Application.TareasContext.UseCases
{
    public class ModificarTareaUseCase : IModificarTarea
    {
        private readonly ITareaRepository _tareaRepository;
        private readonly ICategoriaDomainService _categoriaDomainService;

        public ModificarTareaUseCase(ITareaRepository tareaRepository, ICategoriaDomainService categoriaDomainService)
        {
            _tareaRepository = tareaRepository;
            _categoriaDomainService = categoriaDomainService;
        }

        public async Task<bool> ExecuteAsync(ModificarTareaCommand modificarTarea)
        {
            var tarea = await _tareaRepository.GetAsync(modificarTarea.Id) ??
                throw new TareasContextException(TareasContextExceptionEnum.LaTareaNoExiste);

            if (modificarTarea is null)
            {
                throw new TareasContextException(TareasContextExceptionEnum.LaInformacionParaCrearLaTareaNoEsValida);
            }

            if (string.IsNullOrWhiteSpace(modificarTarea.Descripcion))
            {
                throw new TareasContextException(TareasContextExceptionEnum.DescripcionDeLaTareaEsRequerida);
            }

            if (modificarTarea.Descripcion.Equals(DateTime.MinValue) || modificarTarea.FechaLimite < DateTime.UtcNow)
            {
                throw new TareasContextException(TareasContextExceptionEnum.FechaLimiteDeLaTareaEsRequerida);
            }

            var categoria = await _categoriaDomainService.ObtenerCategoria(modificarTarea.CategoriaId);

            tarea.DescripcionActualizada(modificarTarea.Descripcion)
                .FechaLimiteActualizada(modificarTarea.FechaLimite)
                .CategoriaActualizada(categoria);

            return await _tareaRepository.UpdateAsync(tarea);
        }
    }
}