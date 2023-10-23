using Personal.Soft.Domain.Entities.MongoDB;
using Personal.Soft.Presentation.Request.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Soft.Domain.Database.Repositories
{
    public interface IUserAuthRepository: IRepository
    {
        Task<bool> ExistUser(string email);
        Task<Guid> RegisterUser(UserCreateRequest userCreateRequest);
        Task<bool> LoginUser(UserAuthRequest userAuthRequest);
    }
}
