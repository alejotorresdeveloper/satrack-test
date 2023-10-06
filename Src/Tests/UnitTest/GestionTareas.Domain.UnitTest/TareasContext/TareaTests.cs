using GestionTareas.Domain.SharedKernel;
using GestionTareas.Domain.TareasContext;
using GestionTareas.Domain.TareasContext.Enum;
using GestionTareas.TestData.Domain.TareasContext;
using System;
using System.Threading;
using Xunit;

namespace GestionTareas.Domain.UnitTest.TareasContext
{
    public class TareaTests
    {
        [Fact]
        public void Tarea_Build_RetornaLaInstanciaDeLaClase()
        {
            // Arrange
            var descripcion = "Finalizar tareas de app gestiontareas";
            var fechaLimite = DateTime.Parse("2023-10-06T16:09:50.1430000");
            Categoria categoria = CategoriaMother.CreadaConLoad();

            // Act
            var result = Tarea.Build(
                descripcion,
                fechaLimite,
                categoria);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Equal(descripcion, result.Descripcion);
            Assert.Equal(EstadoEnum.Activo, result.Estado);
            Assert.Equal(EstadoTareaEnum.Nueva, result.EstadoTarea);
            Assert.Equal(fechaLimite, result.FechaLimite);
            Assert.NotEqual(fechaLimite, result.FechaCreacion);
            Assert.Equal(DateTime.MinValue, result.FechaActualizacion);
            Assert.Equal(DateTime.MinValue, result.FechaFinalizacion);
            Assert.False(result.Cumplida);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Tarea_Build_GeneraExcepcionTareasContextExceptionNombreCategoriaEsRequerido(string descripcion)
        {
            //Arrange
            var fechaLimite = DateTime.Parse("2023-10-06T16:09:50.1430000");
            Categoria categoria = CategoriaMother.CreadaConLoad();
            Tarea tarea = null;

            //Act
            var exception = Assert.Throws<TareasContextException>(() => tarea = Tarea.Build(descripcion, fechaLimite, categoria));

            //Assert
            Assert.Equal(2004, exception.Code);
            Assert.Equal("La descripción de la tarea es requerida.", exception.Message);
            Assert.Null(tarea);
        }

        [Fact]
        public void Tarea_Load_RetornaLaInstanciaDeLaClase()
        {
            // Arrange
            Guid id = Guid.Parse("D2F2524E-9897-4E9C-ABE3-2658F823AC33");
            EstadoEnum estadoEnum = EstadoEnum.Activo;
            EstadoTareaEnum estadoTarea = EstadoTareaEnum.Terminada;
            string descripcion = "Pruebas unitarias";
            DateTime fechaLimite = DateTime.UtcNow.AddMinutes(10).ToUniversalTime();
            DateTime fechaFinalizacion = DateTime.Parse("2023-10-05T16:09:50.1430000");
            bool cumplida = true;
            Categoria categoria = CategoriaMother.CreadaConLoad();
            DateTime fechaCreacion = DateTime.Parse("2023-10-02T16:09:50.1430000");
            DateTime fechaActualizacion = DateTime.Parse("2023-10-05T16:09:50.1430000");

            // Act
            var result = Tarea.Load(
                id,
                estadoEnum,
                estadoTarea,
                descripcion,
                fechaLimite,
                fechaFinalizacion,
                cumplida,
                categoria,
                fechaCreacion,
                fechaActualizacion);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(descripcion, result.Descripcion);
            Assert.Equal(EstadoEnum.Activo, result.Estado);
            Assert.Equal(EstadoTareaEnum.Terminada, result.EstadoTarea);
            Assert.Equal(fechaLimite, result.FechaLimite);
            Assert.Equal(fechaCreacion, result.FechaCreacion);
            Assert.Equal(fechaActualizacion, result.FechaActualizacion);
            Assert.Equal(fechaFinalizacion, result.FechaFinalizacion);
            Assert.True(result.Cumplida);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Tarea_Load_GeneraExcepcionTareasContextExceptionNombreCategoriaEsRequerido(string descripcion)
        {
            //Arrange
            Guid id = Guid.Parse("D2F2524E-9897-4E9C-ABE3-2658F823AC33");
            EstadoEnum estadoEnum = EstadoEnum.Activo;
            EstadoTareaEnum estadoTarea = EstadoTareaEnum.Terminada;
            DateTime fechaLimite = DateTime.Parse("2023-10-06T16:09:50.1430000");
            DateTime fechaFinalizacion = DateTime.Parse("2023-10-05T16:09:50.1430000");
            bool cumplida = true;
            Categoria categoria = CategoriaMother.CreadaConLoad();
            DateTime fechaCreacion = DateTime.Parse("2023-10-02T16:09:50.1430000");
            DateTime fechaActualizacion = DateTime.Parse("2023-10-05T16:09:50.1430000");
            Tarea tarea = null;

            //Act
            var exception = Assert.Throws<TareasContextException>(() => tarea = Tarea.Load(id,
                                                                                           estadoEnum,
                                                                                           estadoTarea,
                                                                                           descripcion,
                                                                                           fechaLimite,
                                                                                           fechaFinalizacion,
                                                                                           cumplida,
                                                                                           categoria,
                                                                                           fechaCreacion,
                                                                                           fechaActualizacion));

            //Assert
            Assert.Equal(2004, exception.Code);
            Assert.Equal("La descripción de la tarea es requerida.", exception.Message);
            Assert.Null(tarea);
        }

        [Fact]
        public void Tarea_Actualizado_ActualizaLaFechaActualizacion()
        {
            // Arrange
            var fechaActualizacion = "2023-10-05T12:09:52.0000000";
            DateTime fechaLimite = DateTime.UtcNow.AddMinutes(10).ToUniversalTime();
            var tarea = TareaMother.CreadaConLoad(fechaActualizacion: fechaActualizacion, fechaLimite: fechaLimite.ToUniversalTime().ToString("o"));

            // Act
            var result = tarea.Actualizado();

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Equal(tarea.Descripcion, result.Descripcion);
            Assert.Equal(EstadoEnum.Activo, result.Estado);
            Assert.Equal(EstadoTareaEnum.Nueva, result.EstadoTarea);
            Assert.Equal(tarea.FechaCreacion, result.FechaCreacion);
            Assert.Equal(fechaLimite, result.FechaLimite);
            Assert.NotEqual(DateTime.Parse(fechaActualizacion), result.FechaActualizacion);
            Assert.Equal(tarea.FechaFinalizacion, result.FechaFinalizacion);
            Assert.True(result.Cumplida);
        }

        [Fact]
        public void Tarea_Nueva_CambiaElEstadoTarea_Nueva()
        {
            // Arrange
            var fechaActualizacion = "2023-10-05T12:09:52.0000000";
            DateTime fechaLimite = DateTime.UtcNow.AddMinutes(10).ToUniversalTime();
            var tarea = TareaMother.CreadaConLoad(fechaActualizacion: fechaActualizacion, fechaLimite: fechaLimite.ToUniversalTime().ToString("o"));

            // Act
            var result = tarea.Nueva();

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Equal(tarea.Descripcion, result.Descripcion);
            Assert.Equal(EstadoEnum.Activo, result.Estado);
            Assert.Equal(EstadoTareaEnum.Nueva, result.EstadoTarea);
            Assert.Equal(tarea.FechaCreacion, result.FechaCreacion);
            Assert.Equal(fechaLimite, result.FechaLimite);
            Assert.NotEqual(DateTime.Parse(fechaActualizacion), result.FechaActualizacion);
            Assert.Equal(tarea.FechaFinalizacion, result.FechaFinalizacion);
            Assert.False(result.Cumplida);
        }

        [Fact]
        public void Tarea_EnProgreso_CambiaEstadoTarea_EnProgreso()
        {
            // Arrange
            var fechaActualizacion = "2023-10-05T12:09:52.0000000";
            DateTime fechaLimite = DateTime.UtcNow.AddMinutes(10).ToUniversalTime();
            var tarea = TareaMother.CreadaConLoad(fechaActualizacion: fechaActualizacion, fechaLimite: fechaLimite.ToUniversalTime().ToString("o"));

            // Act
            var result = tarea.EnProgreso();

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Equal(tarea.Descripcion, result.Descripcion);
            Assert.Equal(EstadoEnum.Activo, result.Estado);
            Assert.Equal(EstadoTareaEnum.EnProgreso, result.EstadoTarea);
            Assert.Equal(fechaLimite, result.FechaLimite);
            Assert.Equal(tarea.FechaCreacion, result.FechaCreacion);
            Assert.NotEqual(DateTime.Parse(fechaActualizacion), result.FechaActualizacion);
            Assert.Equal(tarea.FechaFinalizacion, result.FechaFinalizacion);
            Assert.False(result.Cumplida);
        }

        [Fact]
        public void Tarea_Terminada_CambiaElEstado_Terminada()
        {
            // Arrange
            var fechaActualizacion = "2023-10-05T12:09:52.0000000";
            DateTime fechaLimite = DateTime.UtcNow.AddMinutes(10).ToUniversalTime();
            var tarea = TareaMother.CreadaConLoad(fechaActualizacion: fechaActualizacion, fechaLimite: fechaLimite.ToUniversalTime().ToString("o"));

            // Act
            var result = tarea.Terminada();

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Equal(tarea.Descripcion, result.Descripcion);
            Assert.Equal(EstadoEnum.Activo, result.Estado);
            Assert.Equal(EstadoTareaEnum.Terminada, result.EstadoTarea);
            Assert.Equal(tarea.FechaCreacion, result.FechaCreacion);
            Assert.Equal(fechaLimite, result.FechaLimite);
            Assert.NotEqual(DateTime.Parse(fechaActualizacion), result.FechaActualizacion);
            Assert.Equal(tarea.FechaFinalizacion, result.FechaFinalizacion);
            Assert.True(result.Cumplida);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Tarea_DescripcionActualizada_GeneraTareasContextExceptionDescripcionDeLaTareaEsRequerida(string descripcion)
        {
            // Arrange
            var fechaActualizacion = "2023-10-05T12:09:52.0000000";
            DateTime fechaLimite = DateTime.UtcNow.AddMinutes(10).ToUniversalTime();
            var tarea = TareaMother.CreadaConLoad(fechaActualizacion: fechaActualizacion, fechaLimite: fechaLimite.ToUniversalTime().ToString("o"));

            // Act
            var exception = Assert.Throws<TareasContextException>(() => tarea.DescripcionActualizada(descripcion));

            // Assert
            Assert.Equal(2004, exception.Code);
            Assert.Equal("La descripción de la tarea es requerida.", exception.Message);
            Assert.NotNull(tarea);
            Assert.Equal(Guid.Parse("D2F2524E-9897-4E9C-ABE3-2658F823AC33"), tarea.Id);
            Assert.Equal("Finalizar tarea de desarrollo", tarea.Descripcion);
            Assert.Equal(EstadoEnum.Activo, tarea.Estado);
            Assert.Equal(EstadoTareaEnum.Nueva, tarea.EstadoTarea);
            Assert.Equal(DateTime.Parse("2023-10-04T16:09:50.1430000"), tarea.FechaCreacion);
            Assert.Equal(fechaLimite, tarea.FechaLimite);
            Assert.Equal(DateTime.Parse(fechaActualizacion).ToUniversalTime(), tarea.FechaActualizacion);
            Assert.Equal(DateTime.Parse("2023-10-05T16:09:50.1430000"), tarea.FechaFinalizacion);
            Assert.True(tarea.Cumplida);
        }

        [Fact]
        public void Tarea_DescripcionActualizada()
        {
            // Arrange
            var fechaActualizacion = "2023-10-05T12:09:52.0000000";
            DateTime fechaLimite = DateTime.UtcNow.AddMinutes(10).ToUniversalTime();
            var tarea = TareaMother.CreadaConLoad(fechaActualizacion: fechaActualizacion, fechaLimite: fechaLimite.ToUniversalTime().ToString("o"));
            string descripcion = "Finalizar casos de pruebas.";

            // Act
            var result = tarea.DescripcionActualizada(descripcion);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Equal(descripcion, result.Descripcion);
            Assert.Equal(EstadoEnum.Activo, result.Estado);
            Assert.Equal(EstadoTareaEnum.Nueva, result.EstadoTarea);
            Assert.Equal(tarea.FechaCreacion, result.FechaCreacion);
            Assert.Equal(tarea.FechaLimite, result.FechaLimite);
            Assert.NotEqual(DateTime.Parse(fechaActualizacion), result.FechaActualizacion);
            Assert.Equal(tarea.FechaFinalizacion, result.FechaFinalizacion);
            Assert.True(result.Cumplida);
        }

        [Theory]
        [InlineData("0001-01-01T00:00:00.0000000")]
        [InlineData("2023-10-04T16:09:50.1430000")]
        public void Tarea_FechaLimiteActualizada_GeneraExcepcionTareasContextExceptionFechaLimiteDeLaTareaEsRequerida(string fechaLimite)
        {
            // Arrange
            var fechaActualizacion = "2023-10-05T12:09:52.0000000";
            var tarea = TareaMother.CreadaConLoad(fechaActualizacion: fechaActualizacion);

            // Act
            var exception = Assert.Throws<TareasContextException>(() => tarea.FechaLimiteActualizada(
                DateTime.Parse(fechaLimite)));

            // Assert
            Assert.Equal(2005, exception.Code);
            Assert.Equal("La fecha limite de la tarea es requerida.", exception.Message);
            Assert.NotNull(tarea);
            Assert.Equal(Guid.Parse("D2F2524E-9897-4E9C-ABE3-2658F823AC33"), tarea.Id);
            Assert.Equal("Finalizar tarea de desarrollo", tarea.Descripcion);
            Assert.Equal(EstadoEnum.Activo, tarea.Estado);
            Assert.Equal(EstadoTareaEnum.Nueva, tarea.EstadoTarea);
            Assert.Equal(DateTime.Parse("2023-10-04T16:09:50.1430000"), tarea.FechaCreacion);
            Assert.Equal(DateTime.Parse(fechaActualizacion).ToUniversalTime(), tarea.FechaActualizacion);
            Assert.Equal(DateTime.Parse("2023-10-06T16:09:50.1430000"), tarea.FechaLimite);
            Assert.Equal(DateTime.Parse("2023-10-05T16:09:50.1430000"), tarea.FechaFinalizacion);
            Assert.True(tarea.Cumplida);
        }

        [Fact]
        public void Tarea_FechaLimiteActualizada()
        {
            // Arrange
            var fechaActualizacion = "2023-10-05T12:09:52.0000000";
            DateTime fechaLimite = DateTime.UtcNow.AddMinutes(10).ToUniversalTime();
            var tarea = TareaMother.CreadaConLoad(fechaActualizacion: fechaActualizacion, fechaLimite: fechaLimite.ToUniversalTime().ToString("o"));
            fechaLimite = DateTime.UtcNow.AddDays(1);

            // Act
            var result = tarea.FechaLimiteActualizada(fechaLimite);

            // Assert
            Assert.NotNull(tarea);
            Assert.Equal(Guid.Parse("D2F2524E-9897-4E9C-ABE3-2658F823AC33"), tarea.Id);
            Assert.Equal("Finalizar tarea de desarrollo", tarea.Descripcion);
            Assert.Equal(EstadoEnum.Activo, tarea.Estado);
            Assert.Equal(EstadoTareaEnum.Nueva, tarea.EstadoTarea);
            Assert.Equal(DateTime.Parse("2023-10-04T16:09:50.1430000"), tarea.FechaCreacion);
            Assert.Equal(fechaLimite, tarea.FechaLimite);
            Assert.NotEqual(DateTime.Parse(fechaActualizacion), tarea.FechaActualizacion);
            Assert.Equal(DateTime.Parse("2023-10-05T16:09:50.1430000"), tarea.FechaFinalizacion);
            Assert.True(tarea.Cumplida);
        }

        [Fact]
        public void Tarea_CategoriaActualizada_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var fechaActualizacion = "2023-10-05T12:09:52.0000000";
            DateTime fechaLimite = DateTime.UtcNow.AddDays(1).ToUniversalTime();
            var tarea = TareaMother.CreadaConLoad(fechaActualizacion: fechaActualizacion, fechaLimite: fechaLimite.ToUniversalTime().ToString("o"));
            Categoria categoria = CategoriaMother.CreadaConLoad(id: "6BA527B4-B401-49A7-B35C-FDB77ADCF608");

            // Act
            var result = tarea.CategoriaActualizada(categoria);

            // Assert
            Assert.NotNull(tarea);
            Assert.Equal(Guid.Parse("D2F2524E-9897-4E9C-ABE3-2658F823AC33"), tarea.Id);
            Assert.Equal("Finalizar tarea de desarrollo", tarea.Descripcion);
            Assert.Equal(EstadoEnum.Activo, tarea.Estado);
            Assert.Equal(EstadoTareaEnum.Nueva, tarea.EstadoTarea);
            Assert.Equal(DateTime.Parse("2023-10-04T16:09:50.1430000"), tarea.FechaCreacion);
            Assert.Equal(fechaLimite, tarea.FechaLimite);
            Assert.NotEqual(DateTime.Parse(fechaActualizacion), tarea.FechaActualizacion);
            Assert.Equal(DateTime.Parse("2023-10-05T16:09:50.1430000"), tarea.FechaFinalizacion);
            Assert.NotNull(result.Categoria);
            Assert.Equal(Guid.Parse("6BA527B4-B401-49A7-B35C-FDB77ADCF608"), result.Categoria.Id);
            Assert.True(tarea.Cumplida);
        }
    }
}
