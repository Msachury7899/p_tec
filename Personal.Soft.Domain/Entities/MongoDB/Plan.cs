using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Soft.Domain.Entities.MongoDB
{
    public partial class Plan: IEntity
    {        
        public Guid id { get; set; }
        public string nombrePlan { get; set; }

    }
}
