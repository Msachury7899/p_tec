using MongoDB.Driver;
using Personal.Soft.Domain.Database.Repositories;
using Personal.Soft.Domain.Entities.MongoDB;
using Personal.Soft.Presentation.Dtos.ClienteDtos;


namespace Personal.Soft.Infraestructure.Database.Repositories.MongoDB
{
    public class ClienteRepository : InitRepository<Cliente> , IClienteRepository
    {
        public ClienteRepository(IMongoDatabase database) : base(database, "cliente"){}

        public async Task<Guid> CreateAsync( ClienteCreateDTO dto )
        {
            var cliente = new Cliente
            {
                ciudad = dto.ciudad,
                direccion = dto.direccion,
                fechaNacimiento = dto.fechaNacimiento,
                identificacion = dto.identificacion,
                nombreCliente = dto.nombreCliente
            };
            await this.dbCollection.InsertOneAsync(cliente);
            return cliente.id;
        }

        public async Task<Cliente> GetAsync(Guid id)
        {
            FilterDefinition<Cliente> filter = filterBuilder.Eq(entity => entity.id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<Cliente>> GetAllAsync()
        {
            var dataAll = await dbCollection.Find(filterBuilder.Empty).ToListAsync();
            return dataAll;
        }

        public async Task UpdateAsync(ClienteUpdateDTO dto)
        {

            var poliza = new Cliente
            {
                id = dto.id,
                ciudad = dto.ciudad,
                direccion = dto.direccion,
                fechaNacimiento = dto.fechaNacimiento,
                identificacion = dto.identificacion,
                nombreCliente = dto.nombreCliente,                
            };

            FilterDefinition<Cliente> filter = filterBuilder.Eq(existingEntity => existingEntity.id, dto.id);

            await dbCollection.ReplaceOneAsync(filter, poliza);

        }

        public async Task DeleteAsync(Guid id)
        {
            FilterDefinition<Cliente> filter = filterBuilder.Eq(entity => entity.id, id);
            await dbCollection.DeleteOneAsync(filter);
        }
    }
}
