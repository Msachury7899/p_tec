using MongoDB.Driver;
using Personal.Soft.Domain.Database.Repositories;
using Personal.Soft.Domain.Entities.MongoDB;
using Personal.Soft.Presentation.Dtos.ClienteDtos;
using Personal.Soft.Presentation.Dtos.PlanDtos;
using Personal.Soft.Presentation.Dtos.PolizaDtos;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Soft.Infraestructure.Database.Repositories.MongoDB
{
    public class PlanRepository : InitRepository<Plan> , IPlanRepository
    {
        public PlanRepository(IMongoDatabase database) : base(database, "plan"){}

        public async Task<Guid> CreateAsync(PlanCreateDTO dto )
        {
            var plan = new Plan { 
                nombrePlan = dto.nombrePlan,
            };
            await this.dbCollection.InsertOneAsync(plan);
            return plan.id;
        }


        public async Task<Plan> GetAsync(Guid id)
        {
            FilterDefinition<Plan> filter = filterBuilder.Eq(entity => entity.id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<Plan>> GetAllAsync()
        {
            var dataAll = await dbCollection.Find(filterBuilder.Empty).ToListAsync();
            return dataAll;
        }

        public async Task UpdateAsync(PlanUpdateDTO planUpdateDTO)
        {

            var poliza = new Plan
            {
                id = planUpdateDTO.id,
                nombrePlan = planUpdateDTO.nombrePlan
            };
            FilterDefinition<Plan> filter = filterBuilder.Eq(existingEntity => existingEntity.id, planUpdateDTO.id);

            await dbCollection.ReplaceOneAsync(filter, poliza);

        }

        public async Task DeleteAsync(Guid id)
        {
            FilterDefinition<Plan> filter = filterBuilder.Eq(entity => entity.id, id);
            await dbCollection.DeleteOneAsync(filter);
        }
    }
}
