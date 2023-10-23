using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Soft.Domain.Entities.MongoDB
{
    public partial class ClientePoliza : IEntity
    {        
        public Guid id { get; set; }
        public Guid numeroPoliza { get; set; }
        public Guid idCliente { get; set; }
        public Guid idPoliza { get; set; }
        public string placaAutomotor { get; set; } = string.Empty;
        public int modeloAutomotor { get; set; }
        public DateTime fechaAdquision { get; set; }
        public Boolean vehiculoTieneInspeccion { get; set; }        
    }

    
}
