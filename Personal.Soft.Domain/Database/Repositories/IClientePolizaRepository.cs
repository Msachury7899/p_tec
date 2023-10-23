using Personal.Soft.Domain.Entities.MongoDB;
using Personal.Soft.Presentation.Dtos.ClienteDtos;
using Personal.Soft.Presentation.Dtos.ClientPolizaDtos;
using Personal.Soft.Presentation.Request;

namespace Personal.Soft.Domain.Database.Repositories
{
    public interface IClientePolizaRepository:IRepository
    { 
    
        Task<ClientePoliza> SearchPolizaByPlacaOrNumero (SearchPolizaRequest request);
        Task CreateAsync(ClientePolizaCreateDTO clientePolizaCreateDTO);



    }
}
