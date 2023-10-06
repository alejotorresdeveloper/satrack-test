namespace GestionTareas.Application.TareasContext.UseCases.Commands
{
    public class NuevaTareaCommand
    {
        public string Descripcion { get; set; }
        public DateTime FechaLimite { get; set; }
        public Guid CategoriaId { get; set; }
    }
}