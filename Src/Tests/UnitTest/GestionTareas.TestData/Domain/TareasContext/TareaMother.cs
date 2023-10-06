using GestionTareas.Domain.SharedKernel;
using GestionTareas.Domain.TareasContext.Enum;
using GestionTareas.Domain.TareasContext;

namespace GestionTareas.TestData.Domain.TareasContext
{
    public static class TareaMother
    {
        private static Guid Id = Guid.Parse("D2F2524E-9897-4E9C-ABE3-2658F823AC33");
        private static string Descripcion = "Finalizar tarea de desarrollo";
        private static DateTime FechaLimite = DateTime.Parse("2023-10-06T16:09:50.1430000");
        private static DateTime FechaFinalizacion = DateTime.Parse("2023-10-05T16:09:50.1430000");
        private static bool Cumplida = true;
        private static Categoria Categoria = CategoriaMother.CreadaConLoad();
        private static DateTime FechaCreacion = DateTime.Parse("2023-10-04T16:09:50.1430000");
        private static DateTime FechaActualizacion = DateTime.Parse("2023-10-04T16:09:50.1430000");

        public static Tarea CreadaConLoad(string id = "",
                                          EstadoEnum estado = EstadoEnum.Activo,
                                          EstadoTareaEnum estadoTarea = EstadoTareaEnum.Nueva,
                                          string descripcion = "",
                                          string fechaLimite = "",
                                          string fechaFinalizacion = "",
                                          bool? cumplida = null,
                                          Categoria categoria = null,
                                          string fechaCreacion = "",
                                          string fechaActualizacion = "")
        {
            var idWithValue = string.IsNullOrWhiteSpace(id) ? Id : Guid.Parse(id);
            var descripcionWithValue = string.IsNullOrWhiteSpace(descripcion) ? Descripcion : descripcion;
            var fechaLimiteWithValue = string.IsNullOrWhiteSpace(fechaLimite) ? FechaLimite : DateTime.Parse(fechaLimite).ToUniversalTime();
            var fechaFinalizacionWithValue = string.IsNullOrWhiteSpace(fechaFinalizacion) ? FechaFinalizacion : DateTime.Parse(fechaFinalizacion).ToUniversalTime();
            var cumplidaWithValue = cumplida ?? Cumplida;
            var categoriaWithValue = categoria ?? Categoria;
            var fechaCreacionWithValue = string.IsNullOrWhiteSpace(fechaCreacion) ? FechaCreacion : DateTime.Parse(fechaCreacion).ToUniversalTime();
            var fechaActualizacionWithValue = string.IsNullOrWhiteSpace(fechaActualizacion) ? FechaActualizacion : DateTime.Parse(fechaActualizacion).ToUniversalTime();

            return new Tarea(id: idWithValue,
                             estado: EstadoEnum.Activo,
                             estadoTarea: EstadoTareaEnum.Nueva,
                             descripcion: descripcionWithValue,
                             fechaLimite: fechaLimiteWithValue,
                             fechaFinalizacion: fechaFinalizacionWithValue,
                             cumplida: cumplidaWithValue,
                             categoria: categoriaWithValue,
                             fechaCreacion: fechaCreacionWithValue,
                             fechaActualizacion: fechaActualizacionWithValue);
        }
    }
}