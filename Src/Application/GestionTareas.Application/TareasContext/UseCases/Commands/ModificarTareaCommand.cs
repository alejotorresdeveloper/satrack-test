namespace GestionTareas.Application.TareasContext.UseCases.Commands
{
    public class ModificarTareaCommand : NuevaTareaCommand
    {
        public Guid Id { get; set; }
    }
}