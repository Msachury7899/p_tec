using Personal.Soft.Domain.Database;
using Personal.Soft.Domain.Database.Repositories;

namespace Personal.Soft.Infraestructure.Database.DbProvider
{
    public interface IDbProvider
    {        
        public T InstanceRepository<T, K>(params object[] constructorArgs)  where T : IRepository where K : DataSourceCustom;
        public T InstanceRepository<T>(DataSourceCustom dataSource) where T : IRepository;
    }

    public class DbProvider : IDbProvider
    {
        public T InstanceRepository<T,K>(params object[] constructorArgs) where T : IRepository where K : DataSourceCustom
        {
            try
            {
                var dataSource = (K) Activator.CreateInstance(typeof(K), constructorArgs);    
                return dataSource!.BuildRepository<T>();
            }catch
            {
                throw new NotImplementedException();
            }
        }

        public T InstanceRepository<T>(DataSourceCustom dataSource) where T : IRepository
        {

            return dataSource.BuildRepository<T>();
            
        }
    }
}
