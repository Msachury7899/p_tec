using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Soft.Presentation.Dtos.PlanDtos
{
    public record PlanCreateDTO(
        string nombrePlan
    );

    public record PlanUpdateDTO(
        Guid id,
        string nombrePlan
    );
}
