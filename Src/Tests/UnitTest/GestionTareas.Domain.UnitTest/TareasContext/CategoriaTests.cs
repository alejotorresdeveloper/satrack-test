using GestionTareas.Domain.SharedKernel;
using GestionTareas.Domain.TareasContext;
using GestionTareas.TestData.Domain.TareasContext;

namespace GestionTareas.Domain.UnitTest.TareasContext
{
    public class CategoriaTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Categoria_Build_GeneraExcepcionTareasContextExceptionNombreCategoriaEsRequerido(string nombre)
        {
            //Arrange
            Categoria? categoria = null;

            // Act
            var exception = Assert.Throws<TareasContextException>(() => categoria = Categoria.Build(nombre));

            // Assert
            Assert.Null(categoria);
            Assert.Equal(2001, exception.Code);
            Assert.Equal("El nombre de la categoría es requerido.", exception.Message);
        }

        [Theory]
        [InlineData("Infraestructura")]
        [InlineData("Redes")]
        [InlineData("Comercio")]
        public void Categoria_Build_RetornaLaIntanciaDeLaClase(string nombre)
        {
            //Arrange

            // Act
            var result = Categoria.Build(nombre);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Equal(nombre, result.Nombre);
            Assert.Equal(EstadoEnum.Activo, result.Estado);
            Assert.NotEqual(DateTime.MinValue, result.FechaCreacion);
            Assert.Equal(DateTime.MinValue, result.FechaActualizacion);
        }

        [Fact]
        public void Categoria_Load_RetornaLaInstanciaDeLaClase()
        {
            //Arrange
            var id = Guid.Parse("2B03279F-B694-4F36-9668-C955A9B8D9F8");
            var nombre = "Administración";
            var estadoEnum = EstadoEnum.Inactivo;
            var fechaCreacion = DateTime.Parse("2023-10-05T12:09:51.0000000");
            var fechaActualizacion = DateTime.Parse("2023-10-05T12:09:52.0000000");

            //Act
            var result = Categoria.Load(id,
                                        nombre,
                                        estadoEnum,
                                        fechaCreacion,
                                        fechaActualizacion);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(nombre, result.Nombre);
            Assert.Equal(EstadoEnum.Inactivo, result.Estado);
            Assert.Equal(fechaCreacion, result.FechaCreacion);
            Assert.Equal(fechaActualizacion, result.FechaActualizacion);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Categoria_Loadd_GeneraExcepcionTareasContextExceptionNombreCategoriaEsRequerido(string nombre)
        {
            //Arrange
            Categoria? categoria = null;
            var id = Guid.Parse("2B03279F-B694-4F36-9668-C955A9B8D9F8");
            var fechaCreacion = DateTime.Parse("2023-10-05T12:09:51.0000000");
            var fechaActualizacion = DateTime.Parse("2023-10-05T12:09:52.0000000");

            // Act
            var exception = Assert.Throws<TareasContextException>(() => categoria = Categoria.Build(nombre));

            // Assert
            Assert.Null(categoria);
            Assert.Equal(2001, exception.Code);
            Assert.Equal("El nombre de la categoría es requerido.", exception.Message);
        }

        [Fact]
        public void Categoria_Actualizado_ActualizarElValorDeFechaActualizacion()
        {
            // Arrange
            var fechaActualizacion = "2023-10-05T12:09:52.0000000";
            var categoria = CategoriaMother.CreadaConLoad(fechaActualizacion: fechaActualizacion);

            // Act
            var result = categoria.Actualizado();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(Guid.Parse("F94FCA26-1BA3-4D94-B1B4-83175C198737"), result.Id);
            Assert.Equal("Desarrollo", result.Nombre);
            Assert.Equal(EstadoEnum.Activo, result.Estado);
            Assert.Equal(DateTime.Parse("2023-09-20T16:09:50.1430000"), result.FechaCreacion);
            Assert.NotEqual(DateTime.Parse(fechaActualizacion), result.FechaActualizacion);
        }

        [Fact]
        public void Categoria_NombreActualizado_CambiaElValorDeNombre()
        {
            //Arrange
            var fechaActualizacion = "2023-10-05T12:09:52.0000000";
            var categoria = CategoriaMother.CreadaConLoad(fechaActualizacion: fechaActualizacion);
            string nombre = "QA";

            //Act
            var result = categoria.NombreActualizado(nombre);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(Guid.Parse("F94FCA26-1BA3-4D94-B1B4-83175C198737"), result.Id);
            Assert.Equal("QA", result.Nombre);
            Assert.Equal(EstadoEnum.Activo, result.Estado);
            Assert.Equal(DateTime.Parse("2023-09-20T16:09:50.1430000"), result.FechaCreacion);
            Assert.NotEqual(DateTime.Parse(fechaActualizacion), result.FechaActualizacion);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Categoria_NombreActualizado_GeneraExcepcionTareasContextExceptionNombreCategoriaEsRequerido(string nombre)
        {
            //Arrange
            var fechaActualizacion = "2023-10-05T12:09:52.0000000";
            var categoria = CategoriaMother.CreadaConLoad(fechaActualizacion: fechaActualizacion);

            //Act
            var exception = Assert.Throws<TareasContextException>(() => categoria.NombreActualizado(nombre));

            //Assert
            Assert.Equal(2001, exception.Code);
            Assert.Equal("El nombre de la categoría es requerido.", exception.Message);
            Assert.NotNull(categoria);
            Assert.Equal(Guid.Parse("F94FCA26-1BA3-4D94-B1B4-83175C198737"), categoria.Id);
            Assert.Equal("Desarrollo", categoria.Nombre);
            Assert.Equal(EstadoEnum.Activo, categoria.Estado);
            Assert.Equal(DateTime.Parse("2023-09-20T16:09:50.1430000"), categoria.FechaCreacion);
            Assert.Equal(DateTime.Parse(fechaActualizacion), categoria.FechaActualizacion);
        }

    }
}
