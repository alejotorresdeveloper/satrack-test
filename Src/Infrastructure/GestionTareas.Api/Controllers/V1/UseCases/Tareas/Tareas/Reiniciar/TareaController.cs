using GestionTareas.Api.MiddleWare;
using GestionTareas.Application.TareasContext.UseCases.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GestionTareas.Api.Controllers.V1.UseCases.Tareas.Tareas.Reiniciar
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly IReiniciarTarea _reiniciarTarea;

        public TareaController(IReiniciarTarea iniciarTarea)
        {
            _reiniciarTarea = iniciarTarea;
        }

        [HttpPut]
        [Route("{id}/reiniciarTarea")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> ReiniciarTareaAsync(Guid id)
        {
            var actualizado = await _reiniciarTarea.ExecuteAsync(id);

            return Ok(CustomResponse<bool>
                .BuildSuccess(actualizado));
        }
    }
}
