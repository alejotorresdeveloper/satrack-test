using GestionTareas.Domain.SharedKernel;

namespace GestionTareas.Domain.TareasContext
{
    public record Categoria //: Entity
    {
        private Categoria(Guid id,
                          string nombre,
                          EstadoEnum estadoEnum,
                          DateTime fechaCreacion,
                          DateTime fechaActualizacion) //: base(id, estadoEnum, fechaCreacion, fechaActualizacion)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new TareasContextException(TareasContextExceptionEnum.NombreCategoriaEsRequerido);
            Id = id;
            Nombre = nombre;
            Estado = estadoEnum;
            FechaCreacion = fechaCreacion;
            FechaActualizacion = fechaActualizacion;
        }

        public Guid Id { get; private set; }
        public string Nombre { get; private set; }
        public EstadoEnum Estado { get; private set; }
        public DateTime FechaCreacion { get; private set; }
        public DateTime FechaActualizacion { get; private set; }

        public static Categoria Build(string nombre)
        {
            return new Categoria(id: Guid.NewGuid(),
                                 nombre,
                                 estadoEnum: EstadoEnum.Activo,
                                 fechaCreacion: DateTime.UtcNow,
                                 fechaActualizacion: DateTime.MinValue);
        }

        public static Categoria Load(Guid id,
                                     string nombre,
                                     EstadoEnum estadoEnum,
                                     DateTime fechaCreacion,
                                     DateTime fechaActualizacion)
        {
            return new Categoria(id,
                                 nombre,
                                 estadoEnum,
                                 fechaCreacion,
                                 fechaActualizacion);
        }

        public Categoria Actualizado()
        {
            FechaActualizacion = DateTime.UtcNow;
            return this;
        }

        public Categoria NombreActualizado(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new TareasContextException(TareasContextExceptionEnum.NombreCategoriaEsRequerido);

            Nombre = nombre;
            return this.Actualizado();
        }

    }
}