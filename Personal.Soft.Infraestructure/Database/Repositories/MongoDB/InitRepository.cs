using MongoDB.Driver;
using Personal.Soft.Domain.Database.Repositories;
using Personal.Soft.Domain.Entities.MongoDB;
using System.Linq.Expressions;

namespace Personal.Soft.Infraestructure.Database.Repositories.MongoDB
{
 

  
    public class InitRepository<K> : IRepository where K : IEntity
    {

        protected readonly IMongoCollection<K> dbCollection;
        protected readonly FilterDefinitionBuilder<K> filterBuilder = Builders<K>.Filter;

        public InitRepository(
            IMongoDatabase database,
            string collectionName
        )
        {
            dbCollection = database.GetCollection<K>(collectionName);            
        }
     
    }
}
