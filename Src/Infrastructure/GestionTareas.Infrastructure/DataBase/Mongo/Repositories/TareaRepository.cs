using GestionTareas.Application.TareasContext.Repositories;
using GestionTareas.Domain.TareasContext;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace GestionTareas.Infrastructure.DataBase.Mongo.Repositories
{
    public class TareaRepository : ITareaRepository
    {
        private readonly IMongoContext _mongoContext;

        public TareaRepository(IConfiguration configuration)
        {
            _mongoContext = MongoContext.GetMongoDatabase(configuration);
        }

        public async Task<Tarea> AddAsync(Tarea tarea)
        {
            await _mongoContext.Tarea.InsertOneAsync(tarea);
            return tarea;
        }

        public async Task<bool> DeleteAsync(Tarea tarea)
        {
            var result = await _mongoContext.Tarea.DeleteOneAsync(c => c.Id == tarea.Id);

            return result.IsAcknowledged;
        }

        public async Task<ICollection<Tarea>> GetAllAsync(Expression<Func<Tarea, bool>> predicate, int offset = 0, int limit = 10)
        {
            return await _mongoContext.Tarea
                .AsQueryable()
                .Where(predicate)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }

        public Task<Tarea> GetAsync(Guid tareaId)
        {
            return _mongoContext.Tarea
                .AsQueryable()
                .FirstOrDefaultAsync(_ => _.Id == tareaId);
        }

        public Task<Tarea> GetAsync(Expression<Func<Tarea, bool>> predicate)
        {
            return _mongoContext.Tarea
                .AsQueryable()
                .Where(predicate)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(Tarea tarea)
        {
            var result = await _mongoContext.Tarea.ReplaceOneAsync(c => c.Id == tarea.Id,
                tarea);

            return result.IsAcknowledged;
        }
    }
}