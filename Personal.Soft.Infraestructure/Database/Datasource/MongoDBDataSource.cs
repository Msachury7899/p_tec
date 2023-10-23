using MongoDB.Driver;
using Personal.Soft.Domain.Database;
using Personal.Soft.Domain.Database.Repositories;
using Personal.Soft.Domain.Entities.MongoDB;
using Personal.Soft.Infraestructure.Database.Repositories.MongoDB;

namespace Personal.Soft.Infraestructure.Database.Datasource
{
    public class MongoDBDataSource : DataSourceCustom
    {        
        private readonly IMongoDatabase mongoDatabase;
        public MongoDBDataSource(IMongoDatabase mongoDatabase):base()
        {            
          this.mongoDatabase = mongoDatabase;
          this.InitFactory();
        }
        
        override
        protected void InitFactory()
        {
            RegisterRepository<IPolizaRepository, PolizaRepository>(mongoDatabase);
            RegisterRepository<IClienteRepository, ClienteRepository>(mongoDatabase);
            RegisterRepository<IClientePolizaRepository, ClientePolizaRepository>(mongoDatabase);
            RegisterRepository<IPlanRepository, PlanRepository>(mongoDatabase);
            RegisterRepository<IUserAuthRepository, UserAuthRepository>(mongoDatabase);
        }
    }
}
