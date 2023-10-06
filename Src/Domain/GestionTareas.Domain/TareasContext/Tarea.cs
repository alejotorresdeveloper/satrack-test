using GestionTareas.Domain.SharedKernel;
using GestionTareas.Domain.TareasContext.Enum;

namespace GestionTareas.Domain.TareasContext
{
    public record Tarea
    {
        public Tarea(Guid id,
                     EstadoEnum estado,
                     EstadoTareaEnum estadoTarea,
                     string descripcion,
                     DateTime fechaLimite,
                     DateTime fechaFinalizacion,
                     bool cumplida,
                     Categoria categoria,
                     DateTime fechaCreacion,
                     DateTime fechaActualizacion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
                throw new TareasContextException(TareasContextExceptionEnum.DescripcionDeLaTareaEsRequerida);

            Descripcion = descripcion;
            Id = id;
            Estado = estado;
            EstadoTarea = estadoTarea;
            FechaLimite = fechaLimite;
            Categoria = categoria;
            FechaCreacion = fechaCreacion;
            FechaActualizacion = fechaActualizacion;
            FechaFinalizacion = fechaFinalizacion;
            Cumplida = cumplida;
        }

        public Guid Id { get; private set; }
        public EstadoEnum Estado { get; private set; }
        public string Descripcion { get; private set; }
        public EstadoTareaEnum EstadoTarea { get; private set; }

        public DateTime FechaLimite { get; private set; }
        public DateTime FechaFinalizacion { get; private set; }
        public bool Cumplida { get; private set; }

        public Categoria Categoria { get; private set; }
        public DateTime FechaCreacion { get; private set; }
        public DateTime FechaActualizacion { get; private set; }

        public static Tarea Build(string descripcion,
                                  DateTime fechaLimite,
                                  Categoria categoria)
        {
            return new Tarea(id: Guid.NewGuid(),
                             estado: EstadoEnum.Activo,
                             estadoTarea: EstadoTareaEnum.Nueva,
                             descripcion: descripcion,
                             fechaLimite: fechaLimite,
                             fechaFinalizacion: DateTime.MinValue,
                             cumplida: false,
                             categoria: categoria,
                             fechaCreacion: DateTime.UtcNow,
                             fechaActualizacion: DateTime.MinValue);
        }

        public static Tarea Load(Guid id,
                                 EstadoEnum estadoEnum,
                                 EstadoTareaEnum estadoTarea,
                                 string descripcion,
                                 DateTime fechaLimite,
                     DateTime fechaFinalizacion,
                     bool cumplida,
                                 Categoria categoria,
                                 DateTime fechaCreacion,
                                 DateTime fechaActualizacion)
        {
            return new Tarea(id,
                             estadoEnum,
                             estadoTarea,
                             descripcion,
                             fechaLimite,
                             fechaFinalizacion,
                             cumplida,
                             categoria,
                             fechaCreacion,
                             fechaActualizacion);
        }

        public Tarea Actualizado()
        {
            FechaActualizacion = DateTime.UtcNow;
            return this;
        }

        public Tarea Nueva()
        {
            EstadoTarea = EstadoTareaEnum.Nueva;
            FechaFinalizacion = DateTime.MinValue;
            Cumplida = false;
            return this.Actualizado();
        }

        public Tarea EnProgreso()
        {
            EstadoTarea = EstadoTareaEnum.EnProgreso;
            FechaFinalizacion = DateTime.MinValue;
            Cumplida = false;
            return this.Actualizado();
        }

        public Tarea Terminada()
        {
            EstadoTarea = EstadoTareaEnum.Terminada;
            FechaFinalizacion = DateTime.UtcNow;
            Cumplida = DateTime.Compare(FechaFinalizacion, FechaLimite) <= 0;
            return this.Actualizado();
        }

        public Tarea DescripcionActualizada(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
                throw new TareasContextException(TareasContextExceptionEnum.DescripcionDeLaTareaEsRequerida);

            Descripcion = descripcion;
            return this.Actualizado();
        }

        public Tarea FechaLimiteActualizada(DateTime fechaLimite)
        {
            if (fechaLimite == DateTime.MinValue || fechaLimite < DateTime.UtcNow)
                throw new TareasContextException(TareasContextExceptionEnum.FechaLimiteDeLaTareaEsRequerida);

            FechaLimite = fechaLimite;
            return this.Actualizado();
        }

        public Tarea CategoriaActualizada(Categoria categoria)
        {
            Categoria = categoria;
            return this.Actualizado();
        }
    }
}