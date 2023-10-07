using GestionTareas.Application.DomainServices;
using GestionTareas.Application.DomainServices.Contracts;
using GestionTareas.Application.TareasContext.UseCases;
using GestionTareas.Application.TareasContext.UseCases.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace GestionTareas.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            //Domain services
            services.AddScoped<ICategoriaDomainService, CategoriaDomainService>();

            //Use cases
            services.AddScoped<ICrearTarea, CrearTareaUseCase>();
            services.AddScoped<IModificarTarea, ModificarTareaUseCase>();
            services.AddScoped<IEliminarTarea, EliminarTareaUseCase>();
            services.AddScoped<IObtenerTarea, ObtenerTareaUseCase>();
            services.AddScoped<IObtenerTareas, ObtenerTareasUseCase>();
            services.AddScoped<IIniciarTarea, IniciarTareaUseCase>();
            services.AddScoped<IReiniciarTarea, ReiniciarTareaUseCase>();
            services.AddScoped<IFinalizarTarea, FinalizarTareaUseCase>();
            services.AddScoped<ICrearCategorias, CrearCategoriasUseCase>();
            services.AddScoped<IObtenerCategorias, ObtenerCategoriasUseCase>();
        }
    }
}