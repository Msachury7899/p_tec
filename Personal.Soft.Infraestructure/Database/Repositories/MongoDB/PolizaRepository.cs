using MongoDB.Driver;
using Personal.Soft.Domain.Database.Repositories;
using Personal.Soft.Domain.Entities.MongoDB;
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
    public class PolizaRepository : InitRepository<Poliza> , IPolizaRepository
    {
        public PolizaRepository(IMongoDatabase database) : base(database, "poliza"){}

        public async Task<Guid> CreateAsync( PolizaCreateDTO polizaCreateDTO )
        {
            var poliza = new Poliza {                 
                nombrePoliza = polizaCreateDTO.nombrePoliza,
                fechaInicio = polizaCreateDTO.fechaInicio,
                fechaFin = polizaCreateDTO.fechaFin,
                idPlan = polizaCreateDTO.idPlan,
                coberturasCubiertas = polizaCreateDTO.coberturtasCubiertas,
                valorMaximo = polizaCreateDTO.valorMaximo                
            };

            await this.dbCollection.InsertOneAsync(poliza);

            return poliza.id;
        }

        public async Task<Poliza> GetAsync(Guid id)
        {
            FilterDefinition<Poliza> filter = filterBuilder.Eq(entity => entity.id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<Poliza>> GetAllAsync()
        {
            var dataAll =  await dbCollection.Find(filterBuilder.Empty).ToListAsync();
            return dataAll;
        }

        public async Task UpdateAsync(PolizaUpdateDTO polizaUpdateDTO)
        {

            var poliza = new Poliza {
                id = polizaUpdateDTO.id,              
                nombrePoliza = polizaUpdateDTO.nombrePoliza,
                coberturasCubiertas = polizaUpdateDTO.coberturtasCubiertas,
                fechaInicio = polizaUpdateDTO.fechaInicio,
                idPlan = polizaUpdateDTO.idPlan,
                fechaFin = polizaUpdateDTO.fechaFin,
                valorMaximo = polizaUpdateDTO.valorMaximo
            };
            FilterDefinition<Poliza> filter = filterBuilder.Eq(existingEntity => existingEntity.id, polizaUpdateDTO.id);

            await dbCollection.ReplaceOneAsync(filter,poliza);
            
        }

        public async Task DeleteAsync(Guid id)
        {
            FilterDefinition<Poliza> filter = filterBuilder.Eq(entity => entity.id, id);
            await dbCollection.DeleteOneAsync(filter);
        }
    }
}
