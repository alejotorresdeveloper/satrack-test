using GestionTareas.Api.MiddleWare;
using GestionTareas.Application.TareasContext.UseCases.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GestionTareas.Api.Controllers.V1.UseCases.Tareas.Tareas.Modificar
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly IModificarTarea _modificarTarea;

        public TareaController(IModificarTarea modificarTarea)
        {
            _modificarTarea = modificarTarea;
        }

        [HttpPut]
        [Route("{id}/modificarTarea")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> ModificarTareaAsync([Required] Guid id, [FromBody] ModificarTareaRequest request)
        {
            var actualizado = await _modificarTarea.ExecuteAsync(modificarTarea: new()
            {
                Id = id,
                CategoriaId = request.CategoriaId,
                Descripcion = request.Descripcion,
                FechaLimite = request.FechaLimite
            });

            return Ok(CustomResponse<bool>
                .BuildSuccess(actualizado));
        }
    }

    public class ModificarTareaRequest
    {
        public string Descripcion { get; set; }
        public DateTime FechaLimite { get; set; }
        public Guid CategoriaId { get; set; }
    }

    public class CrearTareaResponse
    {
        //Fill the properties based on Tarea class in Domain project
        public Guid Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaLimite { get; set; }
        public DateTime FechaFinalizacion { get; set; }
        public bool Cumplida { get; set; }
        public CrearTareaCategoriaResponse Categoria { get; set; }
        public DateTime FechaCreacion { get; set; }

    }

    public class CrearTareaCategoriaResponse
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
    }
}
