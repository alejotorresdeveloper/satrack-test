using GestionTareas.Application.DomainServices.Contracts;
using GestionTareas.Application.TareasContext.Repositories;
using GestionTareas.Application.TareasContext.UseCases.Commands;
using GestionTareas.Application.TareasContext.UseCases.Contracts;
using GestionTareas.Domain.TareasContext;

namespace GestionTareas.Application.TareasContext.UseCases
{
    public class CrearTareaUseCase : ICrearTarea
    {
        private readonly ITareaRepository _tareaRepository;
        private readonly ICategoriaDomainService _categoriaDomainService;

        public CrearTareaUseCase(ITareaRepository tareaRepository, ICategoriaDomainService categoriaDomainService)
        {
            _tareaRepository = tareaRepository;
            _categoriaDomainService = categoriaDomainService;
        }

        public async Task<Tarea> ExecuteAsync(NuevaTareaCommand nuevaTarea)
        {
            if (nuevaTarea is null)
            {
                throw new TareasContextException(TareasContextExceptionEnum.LaInformacionParaCrearLaTareaNoEsValida);
            }

            if (string.IsNullOrWhiteSpace(nuevaTarea.Descripcion))
            {
                throw new TareasContextException(TareasContextExceptionEnum.DescripcionDeLaTareaEsRequerida);
            }

            if (nuevaTarea.Descripcion.Equals(DateTime.MinValue) || nuevaTarea.FechaLimite < DateTime.UtcNow)
            {
                throw new TareasContextException(TareasContextExceptionEnum.FechaLimiteDeLaTareaEsRequerida);
            }

            var categoria = await _categoriaDomainService.ObtenerCategoria(nuevaTarea.CategoriaId);

            var tarea = Tarea.Build(nuevaTarea.Descripcion, nuevaTarea.FechaLimite, categoria);

            return await _tareaRepository.AddAsync(tarea);
        }
    }
}