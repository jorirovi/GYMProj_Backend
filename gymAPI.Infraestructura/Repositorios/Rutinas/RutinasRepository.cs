using gymAPI.Infraestructura.Database.Entidades;
using gymAPI.Infraestructura.Repositorios.General;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace gymAPI.Infraestructura.Repositorios.Rutinas
{
    public class RutinasRepository : ICrudRepository<RutinasEntity>
    {
        private readonly IMongoCollection<RutinasEntity> _collection;
        public RutinasRepository(IConfiguration configuration)
        {
            var settings = configuration.GetSection("GymDataBase").Get<GymDBSettings>();
            var client = new MongoClient(settings.connectionString);
            var database = client.GetDatabase(settings.databaseName);
            _collection= database.GetCollection<RutinasEntity>("Rutinas");
        }

        public async Task<RutinasEntity> CreateAsync(RutinasEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<List<RutinasEntity>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync(); 
        }

        public async Task<RutinasEntity> GetUserByID(string id)
        {
            return await _collection.Find(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(RutinasEntity entity)
        {
            await _collection.DeleteOneAsync(r => r.Id == entity.Id);
        }

        public async Task<RutinasEntity> UpdateAsync(RutinasEntity entity)
        {
            await _collection.ReplaceOneAsync(r => r.Id == entity.Id, entity);
            return entity;
        }
    }
}