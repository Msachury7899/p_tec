using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Soft.Presentation.Request
{
    public record ClientePolizaCreateRequest
    (
        Guid idCliente,
        Guid idPoliza,
        string placaAutomotor,
        int modeloAutomotor,
        DateTime fechaAdquision,
        bool vehiculoTieneInspeccion
    );


    public class SearchPolizaRequest
    {             
        public string? placaAutomotor {get;set;}        
        public Guid? numeroPoliza {get;set;}        
    
    }
}
