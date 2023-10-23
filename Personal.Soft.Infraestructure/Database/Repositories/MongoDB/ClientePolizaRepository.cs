using MongoDB.Driver;
using Personal.Soft.Domain.Database.Repositories;
using Personal.Soft.Domain.Entities.MongoDB;
using Personal.Soft.Presentation.Dtos.ClienteDtos;
using Personal.Soft.Presentation.Dtos.ClientPolizaDtos;
using Personal.Soft.Presentation.Request;

namespace Personal.Soft.Infraestructure.Database.Repositories.MongoDB
{
    public class ClientePolizaRepository : InitRepository<ClientePoliza> , IClientePolizaRepository
    {
        public ClientePolizaRepository(IMongoDatabase database) : base(database, "cliente_poliza"){}


        

        public async Task CreateAsync(ClientePolizaCreateDTO clientePolizaCreateDTO)
        {
            var clientePoliza = new ClientePoliza
            {
                idCliente = clientePolizaCreateDTO.idCliente,
                idPoliza = clientePolizaCreateDTO.idPoliza,
                modeloAutomotor = clientePolizaCreateDTO.modeloAutomotor,
                placaAutomotor = clientePolizaCreateDTO.placaAutomotor,
                fechaAdquision = clientePolizaCreateDTO.fechaAdquision,
                numeroPoliza = clientePolizaCreateDTO.numeroPoliza,
                vehiculoTieneInspeccion = clientePolizaCreateDTO.vehiculoTieneInspeccion
            };
            await this.dbCollection.InsertOneAsync(clientePoliza);
        }

        public async Task<List<ClientePoliza>> GetAllByCliente(Guid idCliente)
        {
            FilterDefinition<ClientePoliza> filter = filterBuilder.Eq(entity => entity.idCliente, idCliente);
            var dataAll = await dbCollection.Find(filter).ToListAsync();
            return dataAll;
            
        }

        public async Task<ClientePoliza> SearchPolizaByPlacaOrNumero(SearchPolizaRequest request)
        {
            FilterDefinition<ClientePoliza> filter = filterBuilder.Where( e => 
                e.placaAutomotor == request.placaAutomotor || 
                e.numeroPoliza == request.numeroPoliza
            );
            var data = await dbCollection.Find(filter).FirstOrDefaultAsync();
            return data;
        }
    }
}
