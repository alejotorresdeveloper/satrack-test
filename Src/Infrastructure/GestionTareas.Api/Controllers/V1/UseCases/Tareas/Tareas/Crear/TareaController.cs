using GestionTareas.Api.MiddleWare;
using GestionTareas.Application.TareasContext.UseCases.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GestionTareas.Api.Controllers.V1.UseCases.Tareas.Tareas.Crear
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly ICrearTarea _crearTarea;

        public TareaController(ICrearTarea crearTarea)
        {
            _crearTarea = crearTarea;
        }

        [HttpPost]
        [Route("crear")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<CrearTareaResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> CrearTareaAsync([FromBody] CrearTareaRequest request)
        {
            var tarea = await _crearTarea.ExecuteAsync(nuevaTarea: new()
            {
                CategoriaId = request.CategoriaId,
                Descripcion = request.Descripcion,
                FechaLimite = request.FechaLimite
            });

            return Ok(CustomResponse<CrearTareaResponse>
                .BuildSuccess(new CrearTareaResponse
                {
                    Id = tarea.Id,
                    Categoria = new CrearTareaCategoriaResponse
                    {
                        Id = tarea.Categoria.Id,
                        Nombre = tarea.Categoria.Nombre
                    },
                    Cumplida = tarea.Cumplida,
                    Descripcion = tarea.Descripcion,
                    FechaCreacion = tarea.FechaCreacion,
                    FechaFinalizacion = tarea.FechaFinalizacion == DateTime.MinValue ? null : tarea.FechaFinalizacion,
                    FechaLimite = tarea.FechaLimite,
                }));
        }
    }

    public class CrearTareaRequest
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
        public DateTime? FechaFinalizacion { get; set; }
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
