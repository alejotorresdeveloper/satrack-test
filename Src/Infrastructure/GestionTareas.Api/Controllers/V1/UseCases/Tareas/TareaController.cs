using GestionTareas.Api.MiddleWare;
using GestionTareas.Application.TareasContext.UseCases.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GestionTareas.Api.Controllers.V1.UseCases.Tareas
{

    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly ICrearCategorias _crearCategoria;

        public TareaController(ICrearCategorias crearCategoria)
        {
            _crearCategoria = crearCategoria;
        }

        [HttpPost]
        [Route("seed")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> PlantarSemillaAsync()
        {
            var semillaPlantada = await _crearCategoria.ExecuteAsync();

            return Ok(CustomResponse<bool>
                .BuildSuccess(semillaPlantada));
        }
    }
}
