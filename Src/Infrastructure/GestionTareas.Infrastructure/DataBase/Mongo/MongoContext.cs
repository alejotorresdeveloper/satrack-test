using GestionTareas.Domain.TareasContext;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Core.Compression;
using MongoDB.Driver.Core.Configuration;

namespace GestionTareas.Infrastructure.DataBase.Mongo
{
    public class MongoContext : IMongoContext
    {
        private static volatile MongoContext _instance;
        private static readonly object SyncLock = new();
        private readonly IMongoDatabase _database;
        private readonly string _databaseName;

        private MongoContext(IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var connectionString = configuration.GetSection("ConnectionStrings:MongoConnectionString").Value;
            var settings = MongoClientSettings.FromConnectionString(connectionString);

            settings.Compressors = new List<CompressorConfiguration>
            {
                new CompressorConfiguration(CompressorType.Snappy),
                new CompressorConfiguration(CompressorType.Zlib)
            };
            settings.ConnectTimeout = TimeSpan.FromSeconds(double.Parse(configuration.GetSection("ConnectionStrings:ConnectTimeout").Value)); // Reduce el tiempo de espera de conexión
            settings.SocketTimeout = TimeSpan.FromSeconds(double.Parse(configuration.GetSection("ConnectionStrings:SocketTimeout").Value)); // Aumenta el tiempo de espera del socket si es necesario
            settings.MaxConnectionPoolSize = int.Parse(configuration.GetSection("ConnectionStrings:MaxConnectionPoolSize").Value); // Aumenta el tamaño máximo de la agrupación de conexiones según la cantidad de recursos disponibles y la carga de trabajo
            settings.MinConnectionPoolSize = int.Parse(configuration.GetSection("ConnectionStrings:MinConnectionPoolSize").Value); // Aumenta el tamaño mínimo de la agrupación de conexiones para mantener más conexiones activas
            settings.WaitQueueTimeout = TimeSpan.FromSeconds(double.Parse(configuration.GetSection("ConnectionStrings:WaitQueueTimeout").Value)); // Ajusta el tiempo de espera de la cola de espera según las necesidades
            settings.ServerSelectionTimeout = TimeSpan.FromSeconds(double.Parse(configuration.GetSection("ConnectionStrings:ServerSelectionTimeout").Value)); // Ajusta el tiempo de espera de selección del servidor según las necesidades
            settings.HeartbeatInterval = TimeSpan.FromSeconds(double.Parse(configuration.GetSection("ConnectionStrings:HeartbeatInterval").Value));
            settings.HeartbeatTimeout = TimeSpan.FromSeconds(double.Parse(configuration.GetSection("ConnectionStrings:HeartbeatTimeout").Value));
            settings.LocalThreshold = TimeSpan.FromMilliseconds(double.Parse(configuration.GetSection("ConnectionStrings:LocalThreshold").Value));
            settings.RetryReads = bool.Parse(configuration.GetSection("ConnectionStrings:RetryReads").Value); // Habilita el reintento automático de operaciones de lectura
            settings.RetryWrites = bool.Parse(configuration.GetSection("ConnectionStrings:RetryWrites").Value); // Habilita el reintento automático de operaciones de escritura
            settings.LinqProvider = MongoDB.Driver.Linq.LinqProvider.V3;

            _databaseName = configuration.GetSection("ConnectionStrings:MongoDataBaseName").Value;
            _database = new MongoClient(settings).GetDatabase(_databaseName);
        }

        public IMongoCollection<Categoria> Categoria => _database.GetCollection<Categoria>(nameof(Categoria));
        public IMongoCollection<Tarea> Tarea => _database.GetCollection<Tarea>(nameof(Tarea));

        public static MongoContext GetMongoDatabase(IConfiguration configuration)
        {
            if (_instance != null)
            {
                return _instance;
            }

            lock (SyncLock)
            {
                _instance ??= new MongoContext(configuration);
            }

            return _instance;
        }
    }
}