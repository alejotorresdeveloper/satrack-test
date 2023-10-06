﻿using GestionTareas.Application.TareasContext.Repositories;
using GestionTareas.Application.TareasContext.UseCases.Contracts;
using GestionTareas.Domain.TareasContext;

namespace GestionTareas.Application.TareasContext.UseCases
{
    public class ReiniciarTareaUseCase : IReiniciarTarea
    {
        private readonly ITareaRepository _tareaRepository;

        public ReiniciarTareaUseCase(ITareaRepository tareaRepository)
        {
            _tareaRepository = tareaRepository;
        }

        public async Task<bool> ExecuteAsync(Guid tareaId)
        {
            var tarea = await _tareaRepository.GetAsync(tareaId) ??
                throw new TareasContextException(TareasContextExceptionEnum.LaTareaNoExiste);

            tarea.Nueva();

            return await _tareaRepository.UpdateAsync(tarea);
        }
    }
}
