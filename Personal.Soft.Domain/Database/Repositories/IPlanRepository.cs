using Personal.Soft.Domain.Entities.MongoDB;
using Personal.Soft.Presentation.Dtos.PlanDtos;
using Personal.Soft.Presentation.Dtos.PolizaDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Soft.Domain.Database.Repositories
{
    public interface IPlanRepository :IRepository
    {
        Task<Guid> CreateAsync(PlanCreateDTO dto);        
        Task UpdateAsync(PlanUpdateDTO entity);
        Task DeleteAsync(Guid id);
        Task<Plan> GetAsync(Guid id);
        Task<List<Plan>> GetAllAsync();
    }
}
