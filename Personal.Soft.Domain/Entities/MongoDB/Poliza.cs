using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Soft.Domain.Entities.MongoDB
{
    public partial class Poliza: IEntity
    {        
        public Guid id { get; set; }
        public Guid idPlan{ get; set; }
        public int valorMaximo{ get; set; }        
        public string nombrePoliza { get; set; }
        public string coberturasCubiertas { get; set; } = string.Empty;
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }


    }
}
