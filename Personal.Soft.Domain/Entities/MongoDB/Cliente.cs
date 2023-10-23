using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Soft.Domain.Entities.MongoDB
{
    public partial class Cliente: IEntity
    {        
        public Guid id { get; set; }        
        public string identificacion { get; set; } = string.Empty;
        public string nombreCliente { get; set; } = string.Empty;
        public string ciudad { get; set; } = string.Empty;
        public string direccion { get; set; } = string.Empty;
        public DateTime fechaNacimiento { get; set; }

    }
}
