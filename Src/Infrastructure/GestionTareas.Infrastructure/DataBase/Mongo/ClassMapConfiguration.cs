using GestionTareas.Domain.SharedKernel;
using GestionTareas.Domain.TareasContext;
using GestionTareas.Domain.TareasContext.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace GestionTareas.Infrastructure.DataBase.Mongo
{
    public static class ClassMapConfiguration
    {
        public static void RegisterMaps()
        {
            BsonSerializer.RegisterIdGenerator(
                typeof(Guid),
                CombGuidGenerator.Instance
            );
            BsonSerializer.RegisterSerializer(DateTimeSerializer.UtcInstance);
            BsonSerializer.RegisterSerializer(typeof(EstadoEnum), new EnumSerializer<EstadoEnum>(BsonType.String));
            BsonSerializer.RegisterSerializer(typeof(EstadoTareaEnum), new EnumSerializer<EstadoTareaEnum>(BsonType.String));

            var packEnumString = new ConventionPack { new EnumRepresentationConvention(BsonType.String) };
            ConventionRegistry.Register("EnumStringConvention", packEnumString, t => true);

            ConventionRegistry.Remove("__defaults__");
            var pack = new ConventionPack();
            var defaultConventions = DefaultConventionPack.Instance.Conventions;
            pack.AddRange(defaultConventions.Except(defaultConventions.OfType<ImmutableTypeClassMapConvention>()));
            ConventionRegistry.Register("__defaults__", pack, t => true);

            RegisterTareasContextMaps();
        }

        private static void RegisterTareasContextMaps()
        {
            BsonClassMap.RegisterClassMap<Categoria>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapCreator(c => Categoria.Load(c.Id, c.Nombre, c.Estado, c.FechaCreacion, c.FechaActualizacion));
                map.MapIdMember(c => c.Id);
                map.MapMember(c => c.Nombre).SetIsRequired(true);
                map.MapMember(c => c.Estado).SetIsRequired(true).SetSerializer(new EnumSerializer<EstadoEnum>(BsonType.String));
                map.MapMember(c => c.FechaCreacion).SetIsRequired(true);
                map.MapMember(c => c.FechaActualizacion).SetIsRequired(true);
            });

            BsonClassMap.RegisterClassMap<Tarea>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapCreator(c => Tarea.Load(c.Id, c.Estado, c.EstadoTarea, c.Descripcion, c.FechaLimite, c.FechaFinalizacion, c.Cumplida, c.Categoria, c.FechaCreacion, c.FechaActualizacion));
                map.MapIdMember(c => c.Id);
                map.MapMember(c => c.Estado).SetIsRequired(true).SetSerializer(new EnumSerializer<EstadoEnum>(BsonType.String));
                map.MapMember(c => c.EstadoTarea).SetIsRequired(true).SetSerializer(new EnumSerializer<EstadoTareaEnum>(BsonType.String)); ;
                map.MapMember(c => c.Descripcion).SetIsRequired(true);
                map.MapMember(c => c.FechaLimite).SetIsRequired(true);
                map.MapMember(c => c.Cumplida).SetIsRequired(true);
                map.MapMember(c => c.Categoria).SetIsRequired(true);
                map.MapMember(c => c.FechaFinalizacion).SetIsRequired(false);
                map.MapMember(c => c.FechaCreacion).SetIsRequired(true);
                map.MapMember(c => c.FechaActualizacion).SetIsRequired(true);
            });
        }
    }
}