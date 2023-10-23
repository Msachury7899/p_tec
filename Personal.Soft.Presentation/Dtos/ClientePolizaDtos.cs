using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Soft.Presentation.Dtos.ClientPolizaDtos
{
    public class ClientePolizaCreateDTO
    {
        public Guid numeroPoliza { get; set; }
        public Guid idCliente{get;set;}
        public Guid idPoliza{get;set;}
        public string placaAutomotor{get;set;}
        public int modeloAutomotor{get;set;}

        public DateTime fechaAdquision { get; set; }
        public bool vehiculoTieneInspeccion{get;set;}
    
    }
}
