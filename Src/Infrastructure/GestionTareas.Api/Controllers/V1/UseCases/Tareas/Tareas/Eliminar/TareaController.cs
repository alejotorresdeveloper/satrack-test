using GestionTareas.Api.MiddleWare;
using GestionTareas.Application.TareasContext.UseCases.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GestionTareas.Api.Controllers.V1.UseCases.Tareas.Tareas.Eliminar
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly IEliminarTarea _eliminarTarea;

        public TareaController(IEliminarTarea eliminarTarea)
        {
            _eliminarTarea = eliminarTarea;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> EliminarTareaAsync(Guid id)
        {
            var result = await _eliminarTarea.ExecuteAsync(id);
            return Ok(CustomResponse<bool>.BuildSuccess(result));
        }
    }
}
