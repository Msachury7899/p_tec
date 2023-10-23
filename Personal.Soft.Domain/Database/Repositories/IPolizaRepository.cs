using Personal.Soft.Domain.Entities.MongoDB;
using Personal.Soft.Presentation.Dtos.PolizaDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Soft.Domain.Database.Repositories
{
    public interface IPolizaRepository:IRepository
    {
        Task<Guid> CreateAsync(PolizaCreateDTO polizaCreateDTO);
        Task UpdateAsync(PolizaUpdateDTO entity);
        Task DeleteAsync(Guid id);
        Task<Poliza> GetAsync(Guid id);        
        Task<List<Poliza>> GetAllAsync();

    }
}
