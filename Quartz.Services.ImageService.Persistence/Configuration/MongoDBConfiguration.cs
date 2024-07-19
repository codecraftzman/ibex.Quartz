using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using Quartz.Services.ImageService.Domain.Entities;
using Quartz.Shared.Contracts;

namespace Quartz.Services.ImageService.Persistence.Configurations
{
    public static class MongoDBConfigurations
    {
        public static void RegisterClassMaps()
        {
            BsonClassMap.RegisterClassMap<QuartzEntity>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
                // Assuming QuartzEntity has an Id property you want to use as the MongoDB document identifier
                cm.MapIdMember(c => c.Id).SetIdGenerator(ObjectIdGenerator.Instance);
                // Additional configurations for other properties can be added here
                cm.GetMemberMap(c => c.Id).SetSerializer(new ObjectIdSerializer(BsonType.String)); // Optional: Store ObjectId as string
            });
        }
    }
}
