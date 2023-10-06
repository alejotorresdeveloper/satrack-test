using GestionTareas.Api.MiddleWare;
using GestionTareas.Application.TareasContext.UseCases.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GestionTareas.Api.Controllers.V1.UseCases.Tareas.Tareas.Finalizar
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly IFinalizarTarea _finalizarTarea;

        public TareaController(IFinalizarTarea finalizarTarea)
        {
            _finalizarTarea = finalizarTarea;
        }

        [HttpPut]
        [Route("{id}/finalizarTareaTarea")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> FinalizarTareaTareaAsync(Guid id)
        {
            var actualizado = await _finalizarTarea.ExecuteAsync(id);

            return Ok(CustomResponse<bool>
                .BuildSuccess(actualizado));
        }
    }
}
