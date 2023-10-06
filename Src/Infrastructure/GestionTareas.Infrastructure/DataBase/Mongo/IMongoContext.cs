using GestionTareas.Domain.TareasContext;
using MongoDB.Driver;

namespace GestionTareas.Infrastructure.DataBase.Mongo
{
    public interface IMongoContext
    {
        IMongoCollection<Categoria> Categoria { get; }
        IMongoCollection<Tarea> Tarea { get; }
    }
}