using GestionTareas.Domain.SharedKernel;

namespace GestionTareas.Domain.TareasContext
{
    public class TareasContextException : BusinessException
    {
        public TareasContextException(TareasContextExceptionEnum tareasContextException) : base(Detalle(tareasContextException).Item2)
        {
            Code = Detalle(tareasContextException).Item1;
        }

        private static Tuple<int, string> Detalle(TareasContextExceptionEnum tareasContextException)
        {
            var code = (int)tareasContextException;
            string mensaje = tareasContextException switch
            {

                TareasContextExceptionEnum.NombreCategoriaEsRequerido => "El nombre de la categoría es requerido.",
                TareasContextExceptionEnum.CategoriaActiva => "La categoría ya se encuentra activa.",
                TareasContextExceptionEnum.CategoriaInactiva => "La categoría ya se encuentra activa.",
                TareasContextExceptionEnum.DescripcionDeLaTareaEsRequerida => "La descripción de la tarea es requerida.",
                TareasContextExceptionEnum.FechaLimiteDeLaTareaEsRequerida => "La fecha limite de la tarea es requerida.",
                TareasContextExceptionEnum.TareaActiva => "La tarea ya se encuentra activa.",
                TareasContextExceptionEnum.TareaInactiva => "La tarea ya se encuentra inactiva.",
                TareasContextExceptionEnum.YaExisteLaCategoria => "Ya existe esta categoría.",
                TareasContextExceptionEnum.LaInformacionParaCrearLaTareaNoEsValida => "Los datos para crear la tarea están incompletos.",
                TareasContextExceptionEnum.LaCategoriaNoExiste => "La categoría no esta registrada.",
                TareasContextExceptionEnum.LaTareaNoExiste => "La tarea no esta registrada.",
                TareasContextExceptionEnum.NoSeEncontraronTareas => "No se encontraron tareas.",
                _ => "Error desconocido."
            };

            return Tuple.Create(code, mensaje);
        }
    }

    public enum TareasContextExceptionEnum
    {
        NombreCategoriaEsRequerido = 2001,
        CategoriaActiva = 2002,
        CategoriaInactiva = 2003,
        DescripcionDeLaTareaEsRequerida = 2004,
        FechaLimiteDeLaTareaEsRequerida = 2005,
        TareaActiva = 2006,
        TareaInactiva = 2007,
        YaExisteLaCategoria = 2008,
        LaInformacionParaCrearLaTareaNoEsValida = 2009,
        LaCategoriaNoExiste = 2010,
        LaTareaNoExiste = 2011,
        NoSeEncontraronTareas = 2012,
    }
}