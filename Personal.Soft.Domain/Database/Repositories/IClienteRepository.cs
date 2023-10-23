using Personal.Soft.Domain.Entities.MongoDB;
using Personal.Soft.Presentation.Dtos.ClienteDtos;


namespace Personal.Soft.Domain.Database.Repositories
{
    public interface IClienteRepository : IRepository
    {        
        Task<Guid> CreateAsync(ClienteCreateDTO dto);
        Task UpdateAsync(ClienteUpdateDTO dto);
        Task DeleteAsync(Guid id);
        Task<Cliente> GetAsync(Guid id);
        Task<List<Cliente>> GetAllAsync();
    }
}
