using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Soft.Presentation.Dtos.ClienteDtos
{
    public record ClienteCreateDTO(
       string nombreCliente,
       string identificacion,
       string ciudad,
       string direccion,
       DateTime fechaNacimiento
    );

    public record ClienteUpdateDTO(
       Guid id,
       string nombreCliente,
       string identificacion,
       string ciudad,
       string direccion,
       DateTime fechaNacimiento
    );

}
