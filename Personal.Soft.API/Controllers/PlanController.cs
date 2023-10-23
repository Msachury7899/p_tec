using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Personal.Soft.Domain.Database.Repositories;
using Personal.Soft.Domain.Entities.MongoDB;
using Personal.Soft.Infraestructure.Database.Datasource;
using Personal.Soft.Infraestructure.Database.DbProvider;
using Personal.Soft.Presentation.Dtos.PlanDtos;
using Personal.Soft.Presentation.Dtos.PolizaDtos;
using Personal.Soft.Presentation.Shared;

namespace Personal.Soft.API.Controllers
{
    [Authorize]
    [Route("api/plan")]
    [ApiController]
    public class PlanController : ControllerBase
    {

        private IPlanRepository planRepository;
        public PlanController(IDbProvider dbProvider, IMongoDatabase mongoDatabase)
        {
            planRepository = dbProvider.InstanceRepository<IPlanRepository>(new MongoDBDataSource(mongoDatabase));           
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var plan = await this.planRepository.GetAsync(id);

            if (plan is null)
            {
                return NotFound();
            }
                          
            return Ok(plan);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] PlanCreateDTO planCreateDTO)
        {

            var newPlanGuid = await this.planRepository.CreateAsync(planCreateDTO);
            var interactionResponse = new InteractionResponse<dynamic>
            {
                operation = true,
                response = new
                {
                    message = "Plan creado Correctamente",
                    id = newPlanGuid,                                        
                },
            };

            return Ok(interactionResponse.response);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllAsync()
        //{
        //    var polizas = await this.planRepository.GetAllAsync();

        //    return Ok(polizas);
        //}

        //[HttpPut]
        //public async Task<IActionResult> UpdateAsync([FromBody] PlanUpdateDTO planUpdateDTO)
        //{
        //    var plan = await this.planRepository.GetAsync(planUpdateDTO.id);

        //    if (plan is null)
        //    {
        //        return NotFound();
        //    }
        //    await this.planRepository.UpdateAsync(planUpdateDTO);

        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync(Guid id)
        //{
        //    var poliza = await this.planRepository.GetAsync(id);

        //    if (poliza is null)
        //    {
        //        return NotFound();
        //    }
        //    await this.planRepository.DeleteAsync(id);

        //    return NoContent();
        //}
    }
}
