using GestionTareas.Domain.SharedKernel;
using GestionTareas.Domain.TareasContext;

namespace GestionTareas.TestData.Domain.TareasContext
{
    public class CategoriaMother
    {

        private static Guid Id = Guid.Parse("F94FCA26-1BA3-4D94-B1B4-83175C198737");
        private static string Nombre = "Desarrollo";
        private static DateTime FechaCreacion = DateTime.Parse("2023-09-20T16:09:50.1430000");
        private static DateTime FechaActualizacion = DateTime.Parse("2023-09-20T16:09:50.1430000");
        public static Categoria CreadaConLoad(string id = "",
                                     string nombre = "",
                                     EstadoEnum estadoEnum = EstadoEnum.Activo,
                                     string fechaCreacion = "",
                                     string fechaActualizacion = "")
        {
            var idWithValue = string.IsNullOrWhiteSpace(id) ? Id : Guid.Parse(id);
            var nombreWithValue = string.IsNullOrWhiteSpace(nombre) ? Nombre : nombre;
            var fechaCreacionWithValue = string.IsNullOrWhiteSpace(fechaCreacion) ? FechaCreacion : DateTime.Parse(fechaCreacion);
            var fechaActualizacionWithValue = string.IsNullOrWhiteSpace(fechaActualizacion) ? FechaActualizacion : DateTime.Parse(fechaActualizacion);

            return Categoria.Load(idWithValue,
                                  nombreWithValue,
                                  estadoEnum,
                                  fechaCreacionWithValue,
                                  fechaActualizacionWithValue);
        }
    }
}