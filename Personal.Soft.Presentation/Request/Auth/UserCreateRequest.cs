using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Soft.Presentation.Request.Auth
{
    public record UserCreateRequest(
        string names,
        string email,
        string password
    );
}
