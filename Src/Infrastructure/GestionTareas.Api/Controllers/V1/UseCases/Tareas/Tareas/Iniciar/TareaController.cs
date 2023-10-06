using GestionTareas.Api.MiddleWare;
using GestionTareas.Application.TareasContext.UseCases.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GestionTareas.Api.Controllers.V1.UseCases.Tareas.Tareas.Iniciar
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly IIniciarTarea _iniciarTarea;

        public TareaController(IIniciarTarea iniciarTarea)
        {
            _iniciarTarea = iniciarTarea;
        }

        [HttpPut]
        [Route("{id}/iniciarTarea")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> IniciarTareaAsync(Guid id)
        {
            var actualizado = await _iniciarTarea.ExecuteAsync(id);

            return Ok(CustomResponse<bool>
                .BuildSuccess(actualizado));
        }
    }
}
