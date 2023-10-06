using GestionTareas.Api.MiddleWare;
using GestionTareas.Application.TareasContext.UseCases.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GestionTareas.Api.Controllers.V1.UseCases.Tareas.Tareas.Obtener
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {

        private readonly IObtenerTarea _obtenerTarea;
        private readonly IObtenerTareasPorCategoria _obtenerTareasPorCategoria;

        public TareaController(IObtenerTarea obtenerTarea,
                               IObtenerTareasPorCategoria obtenerTareasPorCategoria)
        {
            _obtenerTarea = obtenerTarea;
            _obtenerTareasPorCategoria = obtenerTareasPorCategoria;
        }

        [HttpGet]
        [Route("{id}/obtenerTarea")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<ObtenerTareaResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> ObtenerTareaAsync(Guid id)
        {
            var tarea = await _obtenerTarea.ExecuteAsync(id);

            return Ok(CustomResponse<ObtenerTareaResponse>
                .BuildSuccess(new ObtenerTareaResponse
                {
                    Id = tarea.Id,
                    Categoria = new ObtenerTareaCategoriaResponse
                    {
                        Id = tarea.Categoria.Id,
                        Nombre = tarea.Categoria.Nombre
                    },
                    Cumplida = tarea.Cumplida,

                    Descripcion = tarea.Descripcion,
                    FechaCreacion = tarea.FechaCreacion,
                    FechaFinalizacion = tarea.FechaFinalizacion,
                    FechaLimite = tarea.FechaLimite,
                }));
        }

        [HttpGet]
        [Route("{categoriaId}/obtenerTareas")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<List<ObtenerTareaResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> ObtenerPorCategoriaTareaAsync(Guid? categoriaId)
        {
            var tareas = await _obtenerTareasPorCategoria.ExecuteAsync(categoriaId);

            return Ok(CustomResponse<List<ObtenerTareaResponse>>
                .BuildSuccess(tareas.Select(tarea => new ObtenerTareaResponse
                {
                    Id = tarea.Id,
                    Categoria = new ObtenerTareaCategoriaResponse
                    {
                        Id = tarea.Categoria.Id,
                        Nombre = tarea.Categoria.Nombre
                    },
                    Cumplida = tarea.Cumplida,

                    Descripcion = tarea.Descripcion,
                    FechaCreacion = tarea.FechaCreacion,
                    FechaFinalizacion = tarea.FechaFinalizacion,
                    FechaLimite = tarea.FechaLimite,
                }).ToList()));
        }
    }

    public class ObtenerTareaResponse
    {
        //Fill the properties based on Tarea class in Domain project
        public Guid Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaLimite { get; set; }
        public DateTime FechaFinalizacion { get; set; }
        public bool Cumplida { get; set; }
        public ObtenerTareaCategoriaResponse Categoria { get; set; }
        public DateTime FechaCreacion { get; set; }

    }

    public class ObtenerTareaCategoriaResponse
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
    }
}
