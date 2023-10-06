using GestionTareas.Application.TareasContext.Repositories;
using GestionTareas.Application.TareasContext.UseCases.Contracts;
using GestionTareas.Domain.TareasContext;

namespace GestionTareas.Application.TareasContext.UseCases
{
    public class CrearCategoriasUseCase : ICrearCategorias
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CrearCategoriasUseCase(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<bool> ExecuteAsync()
        {
            var categorias = new string[] { "Desarrollo", "Diseño", "Infraestructura", "Documentación", "Pruebas", "Reuniones", "Otro" };

            foreach (var categoria in categorias)
            {
                var categoriaExiste = await _categoriaRepository.GetAsync(c => c.Nombre.Equals(categoria));
                
                if (categoriaExiste is null)
                    await _categoriaRepository.AddAsync(Categoria.Build(categoria));
            }

            return true;
        }
    }
}