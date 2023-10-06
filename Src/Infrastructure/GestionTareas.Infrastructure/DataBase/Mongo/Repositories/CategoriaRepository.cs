using GestionTareas.Application.TareasContext.Repositories;
using GestionTareas.Domain.TareasContext;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace GestionTareas.Infrastructure.DataBase.Mongo.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly IMongoContext _mongoContext;

        public CategoriaRepository(IConfiguration configuration)
        {
            _mongoContext = MongoContext.GetMongoDatabase(configuration);
        }

        public async Task<Categoria> AddAsync(Categoria categoria)
        {
            await _mongoContext.Categoria.InsertOneAsync(categoria);
            return categoria;
        }

        public async Task<ICollection<Categoria>> GetAllAsync(Expression<Func<Categoria, bool>> predicate, int offset = 0, int limit = 10)
        {
            return await _mongoContext.Categoria
                .AsQueryable()
                .Where(predicate)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }

        public Task<Categoria> GetAsync(Guid categoriaId)
        {
            return _mongoContext.Categoria
                .AsQueryable()
                .FirstOrDefaultAsync(_ => _.Id == categoriaId);
        }

        public Task<Categoria> GetAsync(Expression<Func<Categoria, bool>> predicate)
        {
            return _mongoContext.Categoria
                .AsQueryable()
                .Where(predicate)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(Categoria categoria)
        {
            var result = await _mongoContext.Categoria.ReplaceOneAsync(c => c.Id == categoria.Id,
                categoria);

            return result.IsAcknowledged;
        }
    }
}