using gymAPI.Infraestructura.Database.Entidades;
using gymAPI.Infraestructura.Repositorios.General;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace gymAPI.Infraestructura.Repositorios.UnidadRepository
{
    public class UnidadRepository : ICrudRepository<UnidadPesoEntity>, IUnidadRepository
    {
        private readonly IMongoCollection<UnidadPesoEntity> _collection;
        public UnidadRepository(IConfiguration configuration)
        {
            var settings = configuration.GetSection("GymDataBase").Get<GymDBSettings>();
            var client = new MongoClient(settings.connectionString);
            var database = client.GetDatabase(settings.databaseName);
            _collection= database.GetCollection<UnidadPesoEntity>("unidadpeso");
        }

        public async Task<UnidadPesoEntity> CreateAsync(UnidadPesoEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<List<UnidadPesoEntity>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public Task<UnidadPesoEntity> GetUnidadByName(string name)
        {
            return _collection.Find(uP => uP.unidad == name).FirstOrDefaultAsync();
        }

        public Task<UnidadPesoEntity> GetUnidadByNumero(int numero)
        {
            return _collection.Find(uP => uP.numero == numero).FirstOrDefaultAsync();
        }

        public async Task<UnidadPesoEntity> GetUserByID(string id)
        {
            return await _collection.Find(uP => uP.Id == id).FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(UnidadPesoEntity entity)
        {
            await _collection.DeleteOneAsync(uP => uP.Id == entity.Id);
        }

        public async Task<UnidadPesoEntity> UpdateAsync(UnidadPesoEntity entity)
        {
            await _collection.ReplaceOneAsync(uP => uP.Id == entity.Id, entity);
            return entity;
        }
    }
}