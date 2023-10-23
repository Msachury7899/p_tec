using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using Personal.Soft.Infraestructure.Database.Repositories.MongoDB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Personal.Soft.Infraestructure.Settings;
using MongoDB.Driver;

namespace Personal.Soft.Infraestructure.Extentions
{
    public static class Extensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            services.AddSingleton(servicerProvider =>
            {
                BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
                BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
                var configuration = servicerProvider.GetService<IConfiguration>()!;
                ServiceSettings serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                var mongoDbSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
                var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);
                return mongoClient.GetDatabase(serviceSettings.ServiceName);
            });

            return services;
        }

    }
}
