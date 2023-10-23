using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Personal.Soft.Domain.Database.Repositories;
using Personal.Soft.Infraestructure.Database.Datasource;
using Personal.Soft.Infraestructure.Database.DbProvider;
using Personal.Soft.Presentation.Dtos.ClienteDtos;
using Personal.Soft.Presentation.Dtos.ClientPolizaDtos;
using Personal.Soft.Presentation.Request;

namespace Personal.Soft.API.Controllers
{
    [Authorize]
    [Route("api/cliente/poliza")]
    [ApiController]
    public class ClientePolizaController : ControllerBase
    {
        private IClienteRepository clienteRepository;
        private IPolizaRepository  polizaRepository;
        private IClientePolizaRepository clientePolizaRepository;
        private IPlanRepository planRepository;
        public ClientePolizaController(IDbProvider dbProvider, IMongoDatabase mongoDatabase)
        {
            MongoDBDataSource mongoDBDataSource = new MongoDBDataSource(mongoDatabase);
            clienteRepository = dbProvider.InstanceRepository<IClienteRepository>(mongoDBDataSource);
            polizaRepository  = dbProvider.InstanceRepository<IPolizaRepository>(mongoDBDataSource);
            clientePolizaRepository = dbProvider.InstanceRepository<IClientePolizaRepository>(mongoDBDataSource);
            planRepository = dbProvider.InstanceRepository<IPlanRepository>(mongoDBDataSource);

        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ClientePolizaCreateRequest clientePolizaCreateRequest)
        {
            var dataPoliza = await this.polizaRepository.GetAsync(clientePolizaCreateRequest.idPoliza);

            if (dataPoliza.fechaFin < clientePolizaCreateRequest.fechaAdquision)
            {
                return BadRequest(
                    new
                    {
                        message = "La poliza no tiene vigencia activa"
                    }
                );
            }


            var clientePolizaCreateDTO = new ClientePolizaCreateDTO
            {
                idCliente = clientePolizaCreateRequest.idCliente,
                idPoliza = clientePolizaCreateRequest.idPoliza,
                vehiculoTieneInspeccion = clientePolizaCreateRequest.vehiculoTieneInspeccion,
                modeloAutomotor = clientePolizaCreateRequest.modeloAutomotor,
                placaAutomotor = clientePolizaCreateRequest.placaAutomotor,
                fechaAdquision = clientePolizaCreateRequest.fechaAdquision,
                numeroPoliza = Guid.NewGuid()
            };
            await this.clientePolizaRepository.CreateAsync(clientePolizaCreateDTO);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetByRequestAsync([FromQuery] SearchPolizaRequest request)
        {
            if (request.placaAutomotor is null && request.numeroPoliza is null)
            {
                return BadRequest();
            }

            var dataClientePoliza = await this.clientePolizaRepository.SearchPolizaByPlacaOrNumero(request);
            var dataCliente = await this.clienteRepository.GetAsync(dataClientePoliza.idCliente);
            var dataPoliza = await this.polizaRepository.GetAsync(dataClientePoliza.idPoliza);
            var dataPlan = await this.planRepository.GetAsync(dataPoliza.idPlan);

            var response = new
            {
                cliente = dataCliente.nombreCliente,
                poliza  = dataPoliza.nombrePoliza,
                placaAutomotor = dataClientePoliza.placaAutomotor,
                fechaAdquision = dataClientePoliza.fechaAdquision,
                nombrePlan = dataPlan.nombrePlan,
                fechaVigenciaMaxima = dataPoliza.fechaFin

            };

            return Ok(response);
        }
    }
}
