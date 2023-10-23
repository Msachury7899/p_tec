using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Soft.Presentation.Dtos.PolizaDtos
{

    public record PolizaCreateDTO(        
        int valorMaximo,
        string coberturtasCubiertas,
        string nombrePoliza,
        Guid idPlan,
        DateTime fechaInicio,
        DateTime fechaFin
    );

    public record PolizaUpdateDTO(        
        Guid id,        
        int valorMaximo,
        string coberturtasCubiertas,
        String nombrePoliza,
        Guid idPlan,
        DateTime fechaInicio,
        DateTime fechaFin
    );
}
