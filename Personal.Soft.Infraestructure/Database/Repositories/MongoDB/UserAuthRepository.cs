using MongoDB.Driver;
using Personal.Soft.Domain.Database.Repositories;
using Personal.Soft.Domain.Entities.MongoDB;
using Personal.Soft.Presentation.Dtos.ClienteDtos;
using Personal.Soft.Presentation.Dtos.ClientPolizaDtos;
using Personal.Soft.Presentation.Request.Auth;

namespace Personal.Soft.Infraestructure.Database.Repositories.MongoDB
{
    public class UserAuthRepository : InitRepository<UserAuth> , IUserAuthRepository
    {
        public UserAuthRepository(IMongoDatabase database) : base(database, "user_auth"){}

        public async Task<bool> ExistUser(string email)
        {
            FilterDefinition<UserAuth> filter = filterBuilder.Where(e => e.email == email);
            var exist = await dbCollection.Find(filter).CountAsync();

            return exist > 0;
        }

        public async Task<bool> LoginUser(UserAuthRequest userAuth)
        {
            FilterDefinition<UserAuth> filter = filterBuilder.Where(e => e.email == userAuth.email && e.password == userAuth.password);
            var exist =  await dbCollection.Find(filter).CountAsync();

            return exist > 0;
        }

        public async Task<Guid> RegisterUser(UserCreateRequest userCreateRequest)
        {
            var userAuth = new UserAuth
            {
                names = userCreateRequest.names,
                password = userCreateRequest.password,
                email = userCreateRequest.email
            };

            await this.dbCollection.InsertOneAsync(userAuth);

            return userAuth.id;
        }
    }
}
