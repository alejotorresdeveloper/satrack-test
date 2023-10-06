using GestionTareas.Application.TareasContext.Repositories;
using GestionTareas.Infrastructure.DataBase.Mongo;
using GestionTareas.Infrastructure.DataBase.Mongo.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GestionTareas.Infrastructure.DataBase
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMongoContext(this IServiceCollection services)
        {
            ClassMapConfiguration.RegisterMaps();

            services.AddSingleton<ICategoriaRepository, CategoriaRepository>();
            services.AddSingleton<ITareaRepository, TareaRepository>();
        }
    }
}