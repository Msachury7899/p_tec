using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Personal.Soft.Domain.Database.Repositories;
using Personal.Soft.Domain.Entities.MongoDB;
using Personal.Soft.Infraestructure.Database.Datasource;
using Personal.Soft.Infraestructure.Database.DbProvider;
using Personal.Soft.Presentation.Dtos.ClienteDtos;
using Personal.Soft.Presentation.Dtos.PlanDtos;
using Personal.Soft.Presentation.Dtos.PolizaDtos;

namespace Personal.Soft.API.Controllers
{
    [Authorize]
    [Route("api/cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
    {

        private IClienteRepository clienteRepository;
        public ClienteController(IDbProvider dbProvider, IMongoDatabase mongoDatabase)
        {            
            clienteRepository = dbProvider.InstanceRepository<IClienteRepository,MongoDBDataSource>(mongoDatabase);           
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var cliente = await this.clienteRepository.GetAsync(id);

            if (cliente is null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllAsync()
        //{
        //    var clientes = await this.clienteRepository.GetAllAsync();

        //    return Ok(clientes);
        //}

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ClienteCreateDTO clienteCreateDTO)
        {

            var id = await this.clienteRepository.CreateAsync(clienteCreateDTO);

            return Ok(new
            {
                id = id,
                message = "Cliente creado Correctamente"
            });
        }

        //[HttpPut]
        //public async Task<IActionResult> UpdateAsync([FromBody] ClienteUpdateDTO clienteUpdateDTO)
        //{
        //    var poliza = await this.clienteRepository.GetAsync(clienteUpdateDTO.id);

        //    if (poliza is null)
        //    {
        //        return NotFound();
        //    }
        //    await this.clienteRepository.UpdateAsync(clienteUpdateDTO);

        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync(Guid id)
        //{
        //    var cliente = await this.clienteRepository.GetAsync(id);

        //    if (cliente is null)
        //    {
        //        return NotFound();
        //    }
        //    await this.clienteRepository.DeleteAsync(id);

        //    return NoContent();
        //}
    }
}
