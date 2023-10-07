using GestionTareas.Api.MiddleWare;
using GestionTareas.Application.TareasContext.UseCases.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GestionTareas.Api.Controllers.V1.UseCases.Tareas.Categorias.Obtener
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly IObtenerCategorias _obtenerCategorias;

        public TareaController(IObtenerCategorias obtenerCategorias)
        {
            _obtenerCategorias = obtenerCategorias;
        }

        [HttpGet]
        [Route("obtenerCategorias")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<List<ObtenerCategoriasResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> ObtenerCategoriasAsync()
        {
            var categorias = await _obtenerCategorias.ExecuteAsync();

            return Ok(CustomResponse<List<ObtenerCategoriasResponse>>
               .BuildSuccess(categorias.Select(tarea => new ObtenerCategoriasResponse
               {
                   Id = tarea.Id,
                   Descripcion = tarea.Nombre
               }).ToList()));
        }
    }

    public class ObtenerCategoriasResponse
    {
        //Fill the properties based on Tarea class in Domain project
        public Guid Id { get; set; }
        public string Descripcion { get; set; }
    }
}
