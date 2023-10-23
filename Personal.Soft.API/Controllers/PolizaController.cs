using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Personal.Soft.Domain.Database.Repositories;
using Personal.Soft.Domain.Entities.MongoDB;
using Personal.Soft.Infraestructure.Database.Datasource;
using Personal.Soft.Infraestructure.Database.DbProvider;
using Personal.Soft.Presentation.Dtos.PolizaDtos;

namespace Personal.Soft.API.Controllers
{
    [Authorize]
    [Route("api/poliza")]
    [ApiController]
    public class PolizaController : ControllerBase
    {

        private IPolizaRepository polizaRepository;
        public PolizaController(IDbProvider dbProvider, IMongoDatabase mongoDatabase)
        {            
            polizaRepository = dbProvider.InstanceRepository<IPolizaRepository,MongoDBDataSource>(mongoDatabase);           
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var poliza = await this.polizaRepository.GetAsync(id);

            if (poliza is null)
            {
                return NotFound();
            }
                          
            return Ok(poliza);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllAsync()
        //{
        //    var polizas = await this.polizaRepository.GetAllAsync();

        //    return Ok(polizas);
        //}

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] PolizaCreateDTO polizaCreateDTO)
        {
            
            var id = await this.polizaRepository.CreateAsync(polizaCreateDTO);

            return Ok(new
            {
                id = id,

            });
        }

        //[HttpPut]
        //public async Task<IActionResult> UpdateAsync([FromBody] PolizaUpdateDTO polizaUpdateDTO)
        //{
        //    var poliza = await this.polizaRepository.GetAsync(polizaUpdateDTO.id);

        //    if (poliza is null)
        //    {
        //        return NotFound();
        //    }
        //    await this.polizaRepository.UpdateAsync(polizaUpdateDTO);

        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync(Guid id)
        //{
        //    var poliza = await this.polizaRepository.GetAsync(id);

        //    if (poliza is null)
        //    {
        //        return NotFound();
        //    }
        //    await this.polizaRepository.DeleteAsync(id);

        //    return NoContent();
        //}
    }
}
